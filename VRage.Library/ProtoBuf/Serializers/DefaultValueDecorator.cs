// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.DefaultValueDecorator
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
  internal sealed class DefaultValueDecorator : ProtoDecoratorBase
  {
    private readonly object defaultValue;

    public override Type ExpectedType
    {
      get
      {
        return this.Tail.ExpectedType;
      }
    }

    public override bool RequiresOldValue
    {
      get
      {
        return this.Tail.RequiresOldValue;
      }
    }

    public override bool ReturnsValue
    {
      get
      {
        return this.Tail.ReturnsValue;
      }
    }

    public DefaultValueDecorator(TypeModel model, object defaultValue, IProtoSerializer tail)
      : base(tail)
    {
      if (defaultValue == null)
        throw new ArgumentNullException("defaultValue");
      if (model.MapType(defaultValue.GetType()) != tail.ExpectedType)
        throw new ArgumentException("Default value is of incorrect type", "defaultValue");
      this.defaultValue = defaultValue;
    }

    public override void Write(object value, ProtoWriter dest)
    {
      if (object.Equals(value, this.defaultValue))
        return;
      this.Tail.Write(value, dest);
    }

    public override object Read(object value, ProtoReader source)
    {
      return this.Tail.Read(value, source);
    }

    protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      CodeLabel label1 = ctx.DefineLabel();
      if (valueFrom == null)
      {
        ctx.CopyValue();
        CodeLabel label2 = ctx.DefineLabel();
        this.EmitBranchIfDefaultValue(ctx, label2);
        this.Tail.EmitWrite(ctx, (Local) null);
        ctx.Branch(label1, true);
        ctx.MarkLabel(label2);
        ctx.DiscardValue();
      }
      else
      {
        ctx.LoadValue(valueFrom);
        this.EmitBranchIfDefaultValue(ctx, label1);
        this.Tail.EmitWrite(ctx, valueFrom);
      }
      ctx.MarkLabel(label1);
    }

    private void EmitBeq(CompilerContext ctx, CodeLabel label, Type type)
    {
      switch (Helpers.GetTypeCode(type))
      {
        case ProtoTypeCode.Boolean:
        case ProtoTypeCode.Char:
        case ProtoTypeCode.SByte:
        case ProtoTypeCode.Byte:
        case ProtoTypeCode.Int16:
        case ProtoTypeCode.UInt16:
        case ProtoTypeCode.Int32:
        case ProtoTypeCode.UInt32:
        case ProtoTypeCode.Int64:
        case ProtoTypeCode.UInt64:
        case ProtoTypeCode.Single:
        case ProtoTypeCode.Double:
          ctx.BranchIfEqual(label, false);
          break;
        default:
          MethodInfo method = type.GetMethod("op_Equality", BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[2]
          {
            type,
            type
          }, (ParameterModifier[]) null);
          if (method == (MethodInfo) null || method.ReturnType != ctx.MapType(typeof (bool)))
            throw new InvalidOperationException("No suitable equality operator found for default-values of type: " + type.FullName);
          ctx.EmitCall(method);
          ctx.BranchIfTrue(label, false);
          break;
      }
    }

    private void EmitBranchIfDefaultValue(CompilerContext ctx, CodeLabel label)
    {
      switch (Helpers.GetTypeCode(this.ExpectedType))
      {
        case ProtoTypeCode.Boolean:
          if ((bool) this.defaultValue)
          {
            ctx.BranchIfTrue(label, false);
            break;
          }
          else
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
        case ProtoTypeCode.Char:
          if ((int) (char) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (char) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.SByte:
          if ((int) (sbyte) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (sbyte) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.Byte:
          if ((int) (byte) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (byte) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.Int16:
          if ((int) (short) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (short) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.UInt16:
          if ((int) (ushort) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (ushort) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.Int32:
          if ((int) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.UInt32:
          if ((int) (uint) this.defaultValue == 0)
          {
            ctx.BranchIfFalse(label, false);
            break;
          }
          else
          {
            ctx.LoadValue((int) (uint) this.defaultValue);
            this.EmitBeq(ctx, label, this.ExpectedType);
            break;
          }
        case ProtoTypeCode.Int64:
          ctx.LoadValue((long) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.UInt64:
          ctx.LoadValue((long) (ulong) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.Single:
          ctx.LoadValue((float) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.Double:
          ctx.LoadValue((double) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.Decimal:
          Decimal num = (Decimal) this.defaultValue;
          ctx.LoadValue(num);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.DateTime:
          ctx.LoadValue(((DateTime) this.defaultValue).ToBinary());
          ctx.EmitCall(ctx.MapType(typeof (DateTime)).GetMethod("FromBinary"));
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.String:
          ctx.LoadValue((string) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.TimeSpan:
          TimeSpan timeSpan = (TimeSpan) this.defaultValue;
          if (timeSpan == TimeSpan.Zero)
          {
            ctx.LoadValue(typeof (TimeSpan).GetField("Zero"));
          }
          else
          {
            ctx.LoadValue(timeSpan.Ticks);
            ctx.EmitCall(ctx.MapType(typeof (TimeSpan)).GetMethod("FromTicks"));
          }
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        case ProtoTypeCode.Guid:
          ctx.LoadValue((Guid) this.defaultValue);
          this.EmitBeq(ctx, label, this.ExpectedType);
          break;
        default:
          throw new NotSupportedException("Type cannot be represented as a default value: " + this.ExpectedType.FullName);
      }
    }

    protected override void EmitRead(CompilerContext ctx, Local valueFrom)
    {
      this.Tail.EmitRead(ctx, valueFrom);
    }
  }
}
