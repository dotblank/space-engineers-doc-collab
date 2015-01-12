// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.ProtoDecoratorBase
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using System;

namespace ProtoBuf.Serializers
{
    internal abstract class ProtoDecoratorBase : IProtoSerializer
    {
        protected readonly IProtoSerializer Tail;

        public abstract Type ExpectedType { get; }

        public abstract bool ReturnsValue { get; }

        public abstract bool RequiresOldValue { get; }

        protected ProtoDecoratorBase(IProtoSerializer tail)
        {
            this.Tail = tail;
        }

        public abstract void Write(object value, ProtoWriter dest);

        public abstract object Read(object value, ProtoReader source);

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            this.EmitWrite(ctx, valueFrom);
        }

        protected abstract void EmitWrite(CompilerContext ctx, Local valueFrom);

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            this.EmitRead(ctx, valueFrom);
        }

        protected abstract void EmitRead(CompilerContext ctx, Local valueFrom);
    }
}