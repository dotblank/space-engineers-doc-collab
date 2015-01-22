// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.PropertyDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
  internal sealed class PropertyDecorator : ProtoDecoratorBase
  {
    private readonly PropertyInfo property;
    private readonly Type forType;
    private readonly bool readOptionsWriteValue;
    private readonly MethodInfo shadowSetter;

    public override Type ExpectedType
    {
      get
      {
        return this.forType;
      }
    }

    public override bool RequiresOldValue
    {
      get
      {
        return true;
      }
    }

    public override bool ReturnsValue
    {
      get
      {
        return false;
      }
    }

    public PropertyDecorator(TypeModel model, Type forType, PropertyInfo property, IProtoSerializer tail)
      : base(tail)
    {
      this.forType = forType;
      this.property = property;
      PropertyDecorator.SanityCheck(model, property, tail, out this.readOptionsWriteValue, true, true);
      this.shadowSetter = PropertyDecorator.GetShadowSetter(model, property);
    }

    private static void SanityCheck(TypeModel model, PropertyInfo property, IProtoSerializer tail, out bool writeValue, bool nonPublic, bool allowInternal)
    {
      if (property == (PropertyInfo) null)
        throw new ArgumentNullException("property");
      writeValue = tail.ReturnsValue && (PropertyDecorator.GetShadowSetter(model, property) != (MethodInfo) null || property.CanWrite && Helpers.GetSetMethod(property, nonPublic, allowInternal) != (MethodInfo) null);
      if (!property.CanRead || Helpers.GetGetMethod(property, nonPublic, allowInternal) == (MethodInfo) null)
        throw new InvalidOperationException("Cannot serialize property without a get accessor");
      if (!writeValue && (!tail.RequiresOldValue || Helpers.IsValueType(tail.ExpectedType)))
        throw new InvalidOperationException("Cannot apply changes to property " + property.DeclaringType.FullName + "." + property.Name);
    }

    private static MethodInfo GetShadowSetter(TypeModel model, PropertyInfo property)
    {
      MethodInfo instanceMethod = Helpers.GetInstanceMethod(property.ReflectedType, "Set" + property.Name, new Type[1]
      {
        property.PropertyType
      });
      if (instanceMethod == (MethodInfo) null || !instanceMethod.IsPublic || instanceMethod.ReturnType != model.MapType(typeof (void)))
        return (MethodInfo) null;
      else
        return instanceMethod;
    }

    public override void Write(object value, ProtoWriter dest)
    {
      value = this.property.GetValue(value, (object[]) null);
      if (value == null)
        return;
      this.Tail.Write(value, dest);
    }

    public override object Read(object value, ProtoReader source)
    {
      object obj = this.Tail.Read(this.Tail.RequiresOldValue ? this.property.GetValue(value, (object[]) null) : (object) null, source);
      if (this.readOptionsWriteValue && obj != null)
      {
        if (this.shadowSetter == (MethodInfo) null)
          this.property.SetValue(value, obj, (object[]) null);
        else
          this.shadowSetter.Invoke(value, new object[1]
          {
            obj
          });
      }
      return (object) null;
    }

    protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.LoadAddress(valueFrom, this.ExpectedType);
      ctx.LoadValue(this.property);
      ctx.WriteNullCheckedTail(this.property.PropertyType, this.Tail, (Local) null);
    }

    protected override void EmitRead(CompilerContext ctx, Local valueFrom)
    {
      bool writeValue;
      PropertyDecorator.SanityCheck(ctx.Model, this.property, this.Tail, out writeValue, ctx.NonPublic, ctx.AllowInternal(this.property));
      if (this.ExpectedType.IsValueType && valueFrom == null)
        throw new InvalidOperationException("Attempt to mutate struct on the head of the stack; changes would be lost");
      ctx.LoadAddress(valueFrom, this.ExpectedType);
      if (writeValue && this.Tail.RequiresOldValue)
        ctx.CopyValue();
      if (this.Tail.RequiresOldValue)
        ctx.LoadValue(this.property);
      ctx.ReadNullCheckedTail(this.property.PropertyType, this.Tail, (Local) null);
      if (writeValue)
      {
        CodeLabel label1 = new CodeLabel();
        CodeLabel label2 = new CodeLabel();
        if (!this.property.PropertyType.IsValueType)
        {
          ctx.CopyValue();
          label1 = ctx.DefineLabel();
          label2 = ctx.DefineLabel();
          ctx.BranchIfFalse(label1, true);
        }
        if (this.shadowSetter == (MethodInfo) null)
          ctx.StoreValue(this.property);
        else
          ctx.EmitCall(this.shadowSetter);
        if (this.property.PropertyType.IsValueType)
          return;
        ctx.Branch(label2, true);
        ctx.MarkLabel(label1);
        ctx.DiscardValue();
        ctx.DiscardValue();
        ctx.MarkLabel(label2);
      }
      else
      {
        if (!this.Tail.ReturnsValue)
          return;
        ctx.DiscardValue();
      }
    }

    internal static bool CanWrite(TypeModel model, MemberInfo member)
    {
      if (member == (MemberInfo) null)
        throw new ArgumentNullException("member");
      PropertyInfo property = member as PropertyInfo;
      if (!(property != (PropertyInfo) null))
        return member is FieldInfo;
      if (!property.CanWrite)
        return PropertyDecorator.GetShadowSetter(model, property) != (MethodInfo) null;
      else
        return true;
    }
  }
}
