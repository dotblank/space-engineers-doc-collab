// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.DateTimeSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class DateTimeSerializer : IProtoSerializer
    {
        private static readonly Type expectedType = typeof (DateTime);

        public Type ExpectedType
        {
            get { return DateTimeSerializer.expectedType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public DateTimeSerializer(TypeModel model)
        {
        }

        public object Read(object value, ProtoReader source)
        {
            return (object) BclHelpers.ReadDateTime(source);
        }

        public void Write(object value, ProtoWriter dest)
        {
            BclHelpers.WriteDateTime((DateTime) value, dest);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitWrite(ctx.MapType(typeof (BclHelpers)), "WriteDateTime", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead(ctx.MapType(typeof (BclHelpers)), "ReadDateTime", this.ExpectedType);
        }
    }
}