// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.UriDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class UriDecorator : ProtoDecoratorBase
  {
    private static readonly Type expectedType = typeof (Uri);

    public override Type ExpectedType
    {
      get
      {
        return UriDecorator.expectedType;
      }
    }

    public override bool RequiresOldValue
    {
      get
      {
        return false;
      }
    }

    public override bool ReturnsValue
    {
      get
      {
        return true;
      }
    }

    public UriDecorator(TypeModel model, IProtoSerializer tail)
      : base(tail)
    {
    }

    public override void Write(object value, ProtoWriter dest)
    {
      this.Tail.Write((object) ((Uri) value).AbsoluteUri, dest);
    }

    public override object Read(object value, ProtoReader source)
    {
      string uriString = (string) this.Tail.Read((object) null, source);
      if (uriString.Length != 0)
        return (object) new Uri(uriString);
      else
        return (object) null;
    }

    protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.LoadValue(valueFrom);
      ctx.LoadValue(typeof (Uri).GetProperty("AbsoluteUri"));
      this.Tail.EmitWrite(ctx, (Local) null);
    }

    protected override void EmitRead(CompilerContext ctx, Local valueFrom)
    {
      this.Tail.EmitRead(ctx, valueFrom);
      ctx.CopyValue();
      CodeLabel label1 = ctx.DefineLabel();
      CodeLabel label2 = ctx.DefineLabel();
      ctx.LoadValue(typeof (string).GetProperty("Length"));
      ctx.BranchIfTrue(label1, true);
      ctx.DiscardValue();
      ctx.LoadNullRef();
      ctx.Branch(label2, true);
      ctx.MarkLabel(label1);
      ctx.EmitCtor(ctx.MapType(typeof (Uri)), ctx.MapType(typeof (string)));
      ctx.MarkLabel(label2);
    }
  }
}
