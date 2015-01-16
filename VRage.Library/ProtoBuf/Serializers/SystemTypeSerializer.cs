// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.SystemTypeSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal class SystemTypeSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (Type);

        public Type ExpectedType
        {
            get { return SystemTypeSerializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public SystemTypeSerializer(TypeModel model)
        {
        }

        void IProtoSerializer.Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteType((Type) value, dest);
        }

        object IProtoSerializer.Read(object value, ProtoReader source)
        {
            return (object) source.ReadType();
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteType", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadType", this.ExpectedType);
        }
    }
}