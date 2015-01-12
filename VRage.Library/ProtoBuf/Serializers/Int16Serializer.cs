// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.Int16Serializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class Int16Serializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (short);

        public Type ExpectedType
        {
            get { return Int16Serializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public Int16Serializer(TypeModel model)
        {
        }

        public object Read(object value, ProtoReader source)
        {
            return (object) source.ReadInt16();
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteInt16((short) value, dest);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteInt16", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadInt16", this.ExpectedType);
        }
    }
}