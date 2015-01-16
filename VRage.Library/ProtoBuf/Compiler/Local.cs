// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Compiler.Local
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Reflection.Emit;

namespace ProtoBuf.Compiler
{
    internal sealed class Local : IDisposable
    {
        public static readonly Local InputValue = new Local((CompilerContext) null, (Type) null);
        private LocalBuilder value;
        private CompilerContext ctx;

        public Type Type
        {
            get
            {
                if (this.value != null)
                    return this.value.LocalType;
                else
                    return (Type) null;
            }
        }

        internal LocalBuilder Value
        {
            get
            {
                if (this.value == null)
                    throw new ObjectDisposedException(this.GetType().Name);
                else
                    return this.value;
            }
        }

        private Local(LocalBuilder value)
        {
            this.value = value;
        }

        internal Local(CompilerContext ctx, Type type)
        {
            this.ctx = ctx;
            if (ctx == null)
                return;
            this.value = ctx.GetFromPool(type);
        }

        public Local AsCopy()
        {
            if (this.ctx == null)
                return this;
            else
                return new Local(this.value);
        }

        public void Dispose()
        {
            if (this.ctx == null)
                return;
            this.ctx.ReleaseToPool(this.value);
            this.value = (LocalBuilder) null;
            this.ctx = (CompilerContext) null;
        }
    }
}