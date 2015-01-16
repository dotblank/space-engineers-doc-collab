// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.NetObjectSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class NetObjectSerializer : IProtoSerializer
    {
        private readonly int key;
        private readonly Type type;
        private readonly BclHelpers.NetObjectOptions options;

        public Type ExpectedType
        {
            get { return this.type; }
        }

        public bool ReturnsValue
        {
            get { return true; }
        }

        public bool RequiresOldValue
        {
            get { return true; }
        }

        public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
        {
            bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) != BclHelpers.NetObjectOptions.None;
            this.key = flag ? -1 : key;
            this.type = flag ? model.MapType(typeof (object)) : type;
            this.options = options;
        }

        public object Read(object value, ProtoReader source)
        {
            return BclHelpers.ReadNetObject(value, source, this.key,
                this.type == typeof (object) ? (Type) null : this.type, this.options);
        }

        public void Write(object value, ProtoWriter dest)
        {
            BclHelpers.WriteNetObject(value, dest, this.key, this.options);
        }

        public void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.LoadValue(valueFrom);
            ctx.CastToObject(this.type);
            ctx.LoadReaderWriter();
            ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
            if (this.type == ctx.MapType(typeof (object)))
                ctx.LoadNullRef();
            else
                ctx.LoadValue(this.type);
            ctx.LoadValue((int) this.options);
            ctx.EmitCall(ctx.MapType(typeof (BclHelpers)).GetMethod("ReadNetObject"));
            ctx.CastFromObject(this.type);
        }

        public void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.LoadValue(valueFrom);
            ctx.CastToObject(this.type);
            ctx.LoadReaderWriter();
            ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
            ctx.LoadValue((int) this.options);
            ctx.EmitCall(ctx.MapType(typeof (BclHelpers)).GetMethod("WriteNetObject"));
        }
    }
}