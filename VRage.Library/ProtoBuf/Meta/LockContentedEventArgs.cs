// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.LockContentedEventArgs
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf.Meta
{
    public sealed class LockContentedEventArgs : EventArgs
    {
        private readonly string ownerStackTrace;

        public string OwnerStackTrace
        {
            get { return this.ownerStackTrace; }
        }

        internal LockContentedEventArgs(string ownerStackTrace)
        {
            this.ownerStackTrace = ownerStackTrace;
        }
    }
}