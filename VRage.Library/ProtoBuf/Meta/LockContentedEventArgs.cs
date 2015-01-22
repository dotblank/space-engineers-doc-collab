// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.LockContentedEventArgs
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf.Meta
{
  public sealed class LockContentedEventArgs : EventArgs
  {
    private readonly string ownerStackTrace;

    public string OwnerStackTrace
    {
      get
      {
        return this.ownerStackTrace;
      }
    }

    internal LockContentedEventArgs(string ownerStackTrace)
    {
      this.ownerStackTrace = ownerStackTrace;
    }
  }
}
