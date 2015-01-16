// Decompiled with JetBrains decompiler
// Type: System.BoolBlit
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace System
{
    public struct BoolBlit
    {
        private byte m_value;

        internal BoolBlit(byte value)
        {
            this.m_value = value;
        }

        public static implicit operator bool(BoolBlit b)
        {
            return (int) b.m_value != 0;
        }

        public static implicit operator BoolBlit(bool b)
        {
            return new BoolBlit(b ? byte.MaxValue : (byte) 0);
        }
    }
}