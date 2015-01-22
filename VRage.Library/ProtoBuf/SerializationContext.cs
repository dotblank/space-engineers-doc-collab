// Decompiled with JetBrains decompiler
// Type: ProtoBuf.SerializationContext
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.Serialization;

namespace ProtoBuf
{
  public sealed class SerializationContext
  {
    private StreamingContextStates state = StreamingContextStates.Persistence;
    private static readonly SerializationContext @default = new SerializationContext();
    private bool frozen;
    private object context;

    public object Context
    {
      get
      {
        return this.context;
      }
      set
      {
        if (this.context == value)
          return;
        this.ThrowIfFrozen();
        this.context = value;
      }
    }

    internal static SerializationContext Default
    {
      get
      {
        return SerializationContext.@default;
      }
    }

    public StreamingContextStates State
    {
      get
      {
        return this.state;
      }
      set
      {
        if (this.state == value)
          return;
        this.ThrowIfFrozen();
        this.state = value;
      }
    }

    static SerializationContext()
    {
      SerializationContext.@default.Freeze();
    }

    public static implicit operator StreamingContext(SerializationContext ctx)
    {
      if (ctx == null)
        return new StreamingContext(StreamingContextStates.Persistence);
      else
        return new StreamingContext(ctx.state, ctx.context);
    }

    public static implicit operator SerializationContext(StreamingContext ctx)
    {
      return new SerializationContext()
      {
        Context = ctx.Context,
        State = ctx.State
      };
    }

    internal void Freeze()
    {
      this.frozen = true;
    }

    private void ThrowIfFrozen()
    {
      if (this.frozen)
        throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
    }
  }
}
