// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.BlobSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class BlobSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (byte[]);
        private readonly bool overwriteList;

        public Type ExpectedType
        {
            get { return BlobSerializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return !this.overwriteList; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public BlobSerializer(TypeModel model, bool overwriteList)
        {
            this.overwriteList = overwriteList;
        }

        public object Read(object value, ProtoReader source)
        {
            return (object) ProtoReader.AppendBytes(this.overwriteList ? (byte[]) null : (byte[]) value, source);
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteBytes((byte[]) value, dest);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteBytes", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            if (this.overwriteList)
                ctx.LoadNullRef();
            else
                ctx.LoadValue(valueFrom);
            ctx.LoadReaderWriter();
            ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("AppendBytes"));
        }
    }
}