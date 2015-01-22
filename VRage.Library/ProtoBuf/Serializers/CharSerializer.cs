// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.CharSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class CharSerializer : UInt16Serializer
  {
    private static readonly Type expectedType = typeof (char);

    public override Type ExpectedType
    {
      get
      {
        return CharSerializer.expectedType;
      }
    }

    public CharSerializer(TypeModel model)
      : base(model)
    {
    }

    public override void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteUInt16((ushort) (char) value, dest);
    }

    public override object Read(object value, ProtoReader source)
    {
      return (object) (char) source.ReadUInt16();
    }
  }
}
