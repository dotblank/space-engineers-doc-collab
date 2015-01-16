// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.BooleanSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class BooleanSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (bool);

        public Type ExpectedType
        {
            get { return BooleanSerializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public BooleanSerializer(TypeModel model)
        {
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteBoolean((bool) value, dest);
        }

        public object Read(object value, ProtoReader source)
        {
            return (object) (source.ReadBoolean() ? 1 : 0);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteBoolean", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadBoolean", this.ExpectedType);
        }
    }
}