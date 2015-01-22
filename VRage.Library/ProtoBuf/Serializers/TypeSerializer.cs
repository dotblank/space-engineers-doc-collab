// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.TypeSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace ProtoBuf.Serializers
{
  internal sealed class TypeSerializer : IProtoTypeSerializer, IProtoSerializer
  {
    private static readonly Type iextensible = typeof (IExtensible);
    private readonly Type forType;
    private readonly Type constructType;
    private readonly IProtoSerializer[] serializers;
    private readonly int[] fieldNumbers;
    private readonly bool isRootType;
    private readonly bool useConstructor;
    private readonly bool isExtensible;
    private readonly bool hasConstructor;
    private readonly CallbackSet callbacks;
    private readonly MethodInfo[] baseCtorCallbacks;
    private readonly MethodInfo factory;

    public Type ExpectedType
    {
      get
      {
        return this.forType;
      }
    }

    private bool CanHaveInheritance
    {
      get
      {
        if (this.forType.IsClass || this.forType.IsInterface)
          return !this.forType.IsSealed;
        else
          return false;
      }
    }

    bool IProtoSerializer.RequiresOldValue
    {
      get
      {
        return true;
      }
    }

    bool IProtoSerializer.ReturnsValue
    {
      get
      {
        return false;
      }
    }

    public TypeSerializer(TypeModel model, Type forType, int[] fieldNumbers, IProtoSerializer[] serializers, MethodInfo[] baseCtorCallbacks, bool isRootType, bool useConstructor, CallbackSet callbacks, Type constructType, MethodInfo factory)
    {
      Helpers.Sort(fieldNumbers, (object[]) serializers);
      bool flag = false;
      for (int index = 1; index < fieldNumbers.Length; ++index)
      {
        if (fieldNumbers[index] == fieldNumbers[index - 1])
          throw new InvalidOperationException("Duplicate field-number detected; " + fieldNumbers[index].ToString() + " on: " + forType.FullName);
        if (!flag && serializers[index].ExpectedType != forType)
          flag = true;
      }
      this.forType = forType;
      this.factory = factory;
      if (constructType == (Type) null)
        constructType = forType;
      else if (!forType.IsAssignableFrom(constructType))
        throw new InvalidOperationException(forType.FullName + " cannot be assigned from " + constructType.FullName);
      this.constructType = constructType;
      this.serializers = serializers;
      this.fieldNumbers = fieldNumbers;
      this.callbacks = callbacks;
      this.isRootType = isRootType;
      this.useConstructor = useConstructor;
      if (baseCtorCallbacks != null && baseCtorCallbacks.Length == 0)
        baseCtorCallbacks = (MethodInfo[]) null;
      this.baseCtorCallbacks = baseCtorCallbacks;
      if (Helpers.GetUnderlyingType(forType) != (Type) null)
        throw new ArgumentException("Cannot create a TypeSerializer for nullable types", "forType");
      if (model.MapType(TypeSerializer.iextensible).IsAssignableFrom(forType))
      {
        if (forType.IsValueType || !isRootType || flag)
          throw new NotSupportedException("IExtensible is not supported in structs or classes with inheritance");
        this.isExtensible = true;
      }
      this.hasConstructor = !constructType.IsAbstract && Helpers.GetConstructor(constructType, Helpers.EmptyTypes, true) != (ConstructorInfo) null;
      if (constructType != forType && useConstructor && !this.hasConstructor)
        throw new ArgumentException("The supplied default implementation cannot be created: " + constructType.FullName, "constructType");
    }

    public bool HasCallbacks(TypeModel.CallbackType callbackType)
    {
      if (this.callbacks != null && this.callbacks[callbackType] != (MethodInfo) null)
        return true;
      for (int index = 0; index < this.serializers.Length; ++index)
      {
        if (this.serializers[index].ExpectedType != this.forType && ((IProtoTypeSerializer) this.serializers[index]).HasCallbacks(callbackType))
          return true;
      }
      return false;
    }

    bool IProtoTypeSerializer.CanCreateInstance()
    {
      return true;
    }

    object IProtoTypeSerializer.CreateInstance(ProtoReader source)
    {
      return this.CreateInstance(source, false);
    }

    public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
    {
      if (this.callbacks != null)
        this.InvokeCallback(this.callbacks[callbackType], value, context);
      IProtoTypeSerializer protoTypeSerializer = (IProtoTypeSerializer) this.GetMoreSpecificSerializer(value);
      if (protoTypeSerializer == null)
        return;
      protoTypeSerializer.Callback(value, callbackType, context);
    }

    private IProtoSerializer GetMoreSpecificSerializer(object value)
    {
      if (!this.CanHaveInheritance)
        return (IProtoSerializer) null;
      Type type = value.GetType();
      if (type == this.forType)
        return (IProtoSerializer) null;
      for (int index = 0; index < this.serializers.Length; ++index)
      {
        IProtoSerializer protoSerializer = this.serializers[index];
        if (protoSerializer.ExpectedType != this.forType && Helpers.IsAssignableFrom(protoSerializer.ExpectedType, type))
          return protoSerializer;
      }
      if (type == this.constructType)
        return (IProtoSerializer) null;
      TypeModel.ThrowUnexpectedSubtype(this.forType, type);
      return (IProtoSerializer) null;
    }

    public void Write(object value, ProtoWriter dest)
    {
      if (this.isRootType)
        this.Callback(value, TypeModel.CallbackType.BeforeSerialize, dest.Context);
      IProtoSerializer specificSerializer = this.GetMoreSpecificSerializer(value);
      if (specificSerializer != null)
        specificSerializer.Write(value, dest);
      for (int index = 0; index < this.serializers.Length; ++index)
      {
        IProtoSerializer protoSerializer = this.serializers[index];
        if (protoSerializer.ExpectedType == this.forType)
          protoSerializer.Write(value, dest);
      }
      if (this.isExtensible)
        ProtoWriter.AppendExtensionData((IExtensible) value, dest);
      if (!this.isRootType)
        return;
      this.Callback(value, TypeModel.CallbackType.AfterSerialize, dest.Context);
    }

    public object Read(object value, ProtoReader source)
    {
      if (this.isRootType && value != null)
        this.Callback(value, TypeModel.CallbackType.BeforeDeserialize, source.Context);
      int num1 = 0;
      int num2 = 0;
      int num3;
      while ((num3 = source.ReadFieldHeader()) > 0)
      {
        bool flag = false;
        if (num3 < num1)
          num1 = num2 = 0;
        for (int index = num2; index < this.fieldNumbers.Length; ++index)
        {
          if (this.fieldNumbers[index] == num3)
          {
            IProtoSerializer protoSerializer = this.serializers[index];
            if (value == null)
            {
              if (protoSerializer.ExpectedType == this.forType)
                value = this.CreateInstance(source, true);
            }
            else if (protoSerializer.ExpectedType != this.forType && ((IProtoTypeSerializer) protoSerializer).CanCreateInstance() && protoSerializer.ExpectedType.IsSubclassOf(value.GetType()))
              value = ProtoReader.Merge(source, value, ((IProtoTypeSerializer) protoSerializer).CreateInstance(source));
            if (protoSerializer.ReturnsValue)
              value = protoSerializer.Read(value, source);
            else
              protoSerializer.Read(value, source);
            num2 = index;
            num1 = num3;
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          if (value == null)
            value = this.CreateInstance(source, true);
          if (this.isExtensible)
            source.AppendExtensionData((IExtensible) value);
          else
            source.SkipField();
        }
      }
      if (value == null)
        value = this.CreateInstance(source, true);
      if (this.isRootType)
        this.Callback(value, TypeModel.CallbackType.AfterDeserialize, source.Context);
      return value;
    }

    private object InvokeCallback(MethodInfo method, object obj, SerializationContext context)
    {
      object obj1 = (object) null;
      if (method != (MethodInfo) null)
      {
        ParameterInfo[] parameters1 = method.GetParameters();
        object[] parameters2;
        bool flag;
        if (parameters1.Length == 0)
        {
          parameters2 = (object[]) null;
          flag = true;
        }
        else
        {
          parameters2 = new object[parameters1.Length];
          flag = true;
          for (int index = 0; index < parameters2.Length; ++index)
          {
            Type parameterType = parameters1[index].ParameterType;
            object obj2;
            if (parameterType == typeof (SerializationContext))
              obj2 = (object) context;
            else if (parameterType == typeof (Type))
              obj2 = (object) this.constructType;
            else if (parameterType == typeof (StreamingContext))
            {
              obj2 = (object) (StreamingContext) context;
            }
            else
            {
              obj2 = (object) null;
              flag = false;
            }
            parameters2[index] = obj2;
          }
        }
        if (!flag)
          throw CallbackSet.CreateInvalidCallbackSignature(method);
        obj1 = method.Invoke(obj, parameters2);
      }
      return obj1;
    }

    private object CreateInstance(ProtoReader source, bool includeLocalCallback)
    {
      object obj;
      if (this.factory != (MethodInfo) null)
        obj = this.InvokeCallback(this.factory, (object) null, source.Context);
      else if (this.useConstructor)
      {
        if (!this.hasConstructor)
          TypeModel.ThrowCannotCreateInstance(this.constructType);
        obj = Activator.CreateInstance(this.constructType, true);
      }
      else
        obj = BclHelpers.GetUninitializedObject(this.constructType);
      ProtoReader.NoteObject(obj, source);
      if (this.baseCtorCallbacks != null)
      {
        for (int index = 0; index < this.baseCtorCallbacks.Length; ++index)
          this.InvokeCallback(this.baseCtorCallbacks[index], obj, source.Context);
      }
      if (includeLocalCallback && this.callbacks != null)
        this.InvokeCallback(this.callbacks.BeforeDeserialize, obj, source.Context);
      return obj;
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      Type expectedType = this.ExpectedType;
      using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
      {
        this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeSerialize);
        CodeLabel label1 = ctx.DefineLabel();
        if (this.CanHaveInheritance)
        {
          for (int index = 0; index < this.serializers.Length; ++index)
          {
            IProtoSerializer protoSerializer = this.serializers[index];
            if (protoSerializer.ExpectedType != this.forType)
            {
              CodeLabel label2 = ctx.DefineLabel();
              CodeLabel label3 = ctx.DefineLabel();
              ctx.LoadValue(localWithValue);
              ctx.TryCast(protoSerializer.ExpectedType);
              ctx.CopyValue();
              ctx.BranchIfTrue(label2, true);
              ctx.DiscardValue();
              ctx.Branch(label3, true);
              ctx.MarkLabel(label2);
              protoSerializer.EmitWrite(ctx, (Local) null);
              ctx.Branch(label1, false);
              ctx.MarkLabel(label3);
            }
          }
          if (this.constructType != (Type) null && this.constructType != this.forType)
          {
            using (Local local = new Local(ctx, ctx.MapType(typeof (Type))))
            {
              ctx.LoadValue(localWithValue);
              ctx.EmitCall(ctx.MapType(typeof (object)).GetMethod("GetType"));
              ctx.CopyValue();
              ctx.StoreValue(local);
              ctx.LoadValue(this.forType);
              ctx.BranchIfEqual(label1, true);
              ctx.LoadValue(local);
              ctx.LoadValue(this.constructType);
              ctx.BranchIfEqual(label1, true);
            }
          }
          else
          {
            ctx.LoadValue(localWithValue);
            ctx.EmitCall(ctx.MapType(typeof (object)).GetMethod("GetType"));
            ctx.LoadValue(this.forType);
            ctx.BranchIfEqual(label1, true);
          }
          ctx.LoadValue(this.forType);
          ctx.LoadValue(localWithValue);
          ctx.EmitCall(ctx.MapType(typeof (object)).GetMethod("GetType"));
          ctx.EmitCall(ctx.MapType(typeof (TypeModel)).GetMethod("ThrowUnexpectedSubtype", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
        }
        ctx.MarkLabel(label1);
        for (int index = 0; index < this.serializers.Length; ++index)
        {
          IProtoSerializer protoSerializer = this.serializers[index];
          if (protoSerializer.ExpectedType == this.forType)
            protoSerializer.EmitWrite(ctx, localWithValue);
        }
        if (this.isExtensible)
        {
          ctx.LoadValue(localWithValue);
          ctx.LoadReaderWriter();
          ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("AppendExtensionData"));
        }
        this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.AfterSerialize);
      }
    }

    private static void EmitInvokeCallback(CompilerContext ctx, MethodInfo method, bool copyValue, Type constructType, Type type)
    {
      if (!(method != (MethodInfo) null))
        return;
      if (copyValue)
        ctx.CopyValue();
      ParameterInfo[] parameters = method.GetParameters();
      bool flag = true;
      for (int index = 0; index < parameters.Length; ++index)
      {
        Type parameterType = parameters[0].ParameterType;
        if (parameterType == ctx.MapType(typeof (SerializationContext)))
          ctx.LoadSerializationContext();
        else if (parameterType == ctx.MapType(typeof (Type)))
          ctx.LoadValue(constructType ?? type);
        else if (parameterType == ctx.MapType(typeof (StreamingContext)))
        {
          ctx.LoadSerializationContext();
          MethodInfo method1 = ctx.MapType(typeof (SerializationContext)).GetMethod("op_Implicit", new Type[1]
          {
            ctx.MapType(typeof (SerializationContext))
          });
          if (method1 != (MethodInfo) null)
          {
            ctx.EmitCall(method1);
            flag = true;
          }
        }
        else
          flag = false;
      }
      if (!flag)
        throw CallbackSet.CreateInvalidCallbackSignature(method);
      ctx.EmitCall(method);
      if (!(constructType != (Type) null) || !(method.ReturnType == ctx.MapType(typeof (object))))
        return;
      ctx.CastFromObject(type);
    }

    private void EmitCallbackIfNeeded(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
    {
      if (!this.isRootType || !this.HasCallbacks(callbackType))
        return;
      ((IProtoTypeSerializer) this).EmitCallback(ctx, valueFrom, callbackType);
    }

    void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
    {
      bool copyValue = false;
      if (this.CanHaveInheritance)
      {
        for (int index = 0; index < this.serializers.Length; ++index)
        {
          IProtoSerializer protoSerializer = this.serializers[index];
          if (protoSerializer.ExpectedType != this.forType && ((IProtoTypeSerializer) protoSerializer).HasCallbacks(callbackType))
            copyValue = true;
        }
      }
      MethodInfo method = this.callbacks == null ? (MethodInfo) null : this.callbacks[callbackType];
      if (method == (MethodInfo) null && !copyValue)
        return;
      ctx.LoadAddress(valueFrom, this.ExpectedType);
      TypeSerializer.EmitInvokeCallback(ctx, method, copyValue, (Type) null, this.forType);
      if (!copyValue)
        return;
      CodeLabel label1 = ctx.DefineLabel();
      for (int index = 0; index < this.serializers.Length; ++index)
      {
        IProtoSerializer protoSerializer = this.serializers[index];
        IProtoTypeSerializer protoTypeSerializer;
        if (protoSerializer.ExpectedType != this.forType && (protoTypeSerializer = (IProtoTypeSerializer) protoSerializer).HasCallbacks(callbackType))
        {
          CodeLabel label2 = ctx.DefineLabel();
          CodeLabel label3 = ctx.DefineLabel();
          ctx.CopyValue();
          ctx.TryCast(protoSerializer.ExpectedType);
          ctx.CopyValue();
          ctx.BranchIfTrue(label2, true);
          ctx.DiscardValue();
          ctx.Branch(label3, false);
          ctx.MarkLabel(label2);
          protoTypeSerializer.EmitCallback(ctx, (Local) null, callbackType);
          ctx.Branch(label1, false);
          ctx.MarkLabel(label3);
        }
      }
      ctx.MarkLabel(label1);
      ctx.DiscardValue();
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      Type expectedType = this.ExpectedType;
      using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
      {
        using (Local local = new Local(ctx, ctx.MapType(typeof (int))))
        {
          if (this.HasCallbacks(TypeModel.CallbackType.BeforeDeserialize))
          {
            if (this.ExpectedType.IsValueType)
            {
              this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeDeserialize);
            }
            else
            {
              CodeLabel label = ctx.DefineLabel();
              ctx.LoadValue(localWithValue);
              ctx.BranchIfFalse(label, false);
              this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeDeserialize);
              ctx.MarkLabel(label);
            }
          }
          CodeLabel codeLabel1 = ctx.DefineLabel();
          CodeLabel label1 = ctx.DefineLabel();
          ctx.Branch(codeLabel1, false);
          ctx.MarkLabel(label1);
          foreach (BasicList.Group group in BasicList.GetContiguousGroups(this.fieldNumbers, (object[]) this.serializers))
          {
            CodeLabel label2 = ctx.DefineLabel();
            int count = group.Items.Count;
            if (count == 1)
            {
              ctx.LoadValue(local);
              ctx.LoadValue(group.First);
              CodeLabel codeLabel2 = ctx.DefineLabel();
              ctx.BranchIfEqual(codeLabel2, true);
              ctx.Branch(label2, false);
              this.WriteFieldHandler(ctx, expectedType, localWithValue, codeLabel2, codeLabel1, (IProtoSerializer) group.Items[0]);
            }
            else
            {
              ctx.LoadValue(local);
              ctx.LoadValue(group.First);
              ctx.Subtract();
              CodeLabel[] jumpTable = new CodeLabel[count];
              for (int index = 0; index < count; ++index)
                jumpTable[index] = ctx.DefineLabel();
              ctx.Switch(jumpTable);
              ctx.Branch(label2, false);
              for (int index = 0; index < count; ++index)
                this.WriteFieldHandler(ctx, expectedType, localWithValue, jumpTable[index], codeLabel1, (IProtoSerializer) group.Items[index]);
            }
            ctx.MarkLabel(label2);
          }
          this.EmitCreateIfNull(ctx, localWithValue);
          ctx.LoadReaderWriter();
          if (this.isExtensible)
          {
            ctx.LoadValue(localWithValue);
            ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("AppendExtensionData"));
          }
          else
            ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("SkipField"));
          ctx.MarkLabel(codeLabel1);
          ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof (int)));
          ctx.CopyValue();
          ctx.StoreValue(local);
          ctx.LoadValue(0);
          ctx.BranchIfGreater(label1, false);
          this.EmitCreateIfNull(ctx, localWithValue);
          this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.AfterDeserialize);
        }
      }
    }

    private void WriteFieldHandler(CompilerContext ctx, Type expected, Local loc, CodeLabel handler, CodeLabel @continue, IProtoSerializer serializer)
    {
      ctx.MarkLabel(handler);
      if (serializer.ExpectedType == this.forType)
      {
        this.EmitCreateIfNull(ctx, loc);
        serializer.EmitRead(ctx, loc);
      }
      else
      {
        RuntimeTypeModel runtimeTypeModel = (RuntimeTypeModel) ctx.Model;
        if (((IProtoTypeSerializer) serializer).CanCreateInstance())
        {
          CodeLabel label = ctx.DefineLabel();
          ctx.LoadValue(loc);
          ctx.BranchIfFalse(label, false);
          ctx.LoadValue(loc);
          ctx.TryCast(serializer.ExpectedType);
          ctx.BranchIfTrue(label, false);
          ctx.LoadReaderWriter();
          ctx.LoadValue(loc);
          ((IProtoTypeSerializer) serializer).EmitCreateInstance(ctx);
          ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("Merge"));
          ctx.Cast(expected);
          ctx.StoreValue(loc);
          ctx.MarkLabel(label);
        }
        ctx.LoadValue(loc);
        ctx.Cast(serializer.ExpectedType);
        serializer.EmitRead(ctx, (Local) null);
      }
      if (serializer.ReturnsValue)
        ctx.StoreValue(loc);
      ctx.Branch(@continue, false);
    }

    void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
    {
      bool flag = true;
      if (this.factory != (MethodInfo) null)
        TypeSerializer.EmitInvokeCallback(ctx, this.factory, false, this.constructType, this.forType);
      else if (!this.useConstructor)
      {
        ctx.LoadValue(this.constructType);
        ctx.EmitCall(ctx.MapType(typeof (BclHelpers)).GetMethod("GetUninitializedObject"));
        ctx.Cast(this.forType);
      }
      else if (this.constructType.IsClass && this.hasConstructor)
      {
        ctx.EmitCtor(this.constructType);
      }
      else
      {
        ctx.LoadValue(this.ExpectedType);
        ctx.EmitCall(ctx.MapType(typeof (TypeModel)).GetMethod("ThrowCannotCreateInstance", BindingFlags.Static | BindingFlags.Public));
        ctx.LoadNullRef();
        flag = false;
      }
      if (flag)
      {
        ctx.CopyValue();
        ctx.LoadReaderWriter();
        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("NoteObject", BindingFlags.Static | BindingFlags.Public));
      }
      if (this.baseCtorCallbacks == null)
        return;
      for (int index = 0; index < this.baseCtorCallbacks.Length; ++index)
        TypeSerializer.EmitInvokeCallback(ctx, this.baseCtorCallbacks[index], true, (Type) null, this.forType);
    }

    private void EmitCreateIfNull(CompilerContext ctx, Local storage)
    {
      if (this.ExpectedType.IsValueType)
        return;
      CodeLabel label = ctx.DefineLabel();
      ctx.LoadValue(storage);
      ctx.BranchIfTrue(label, false);
      ((IProtoTypeSerializer) this).EmitCreateInstance(ctx);
      if (this.callbacks != null)
        TypeSerializer.EmitInvokeCallback(ctx, this.callbacks.BeforeDeserialize, true, (Type) null, this.forType);
      ctx.StoreValue(storage);
      ctx.MarkLabel(label);
    }
  }
}
