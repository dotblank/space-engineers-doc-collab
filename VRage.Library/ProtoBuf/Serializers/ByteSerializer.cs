// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.ByteSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class ByteSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (byte);

        public Type ExpectedType
        {
            get { return ByteSerializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public ByteSerializer(TypeModel model)
        {
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteByte((byte) value, dest);
        }

        public object Read(object value, ProtoReader source)
        {
            return (object) source.ReadByte();
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteByte", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadByte", this.ExpectedType);
        }
    }
}