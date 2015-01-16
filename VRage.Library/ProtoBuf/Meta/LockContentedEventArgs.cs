// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.LockContentedEventArgs
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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