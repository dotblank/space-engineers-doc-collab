// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.IProtoSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using System;

namespace ProtoBuf.Serializers
{
  internal interface IProtoSerializer
  {
    Type ExpectedType { get; }

    bool RequiresOldValue { get; }

    bool ReturnsValue { get; }

    void Write(object value, ProtoWriter dest);

    object Read(object value, ProtoReader source);

    void EmitWrite(CompilerContext ctx, Local valueFrom);

    void EmitRead(CompilerContext ctx, Local entity);
  }
}
