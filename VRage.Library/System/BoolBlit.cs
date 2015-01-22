// Decompiled with JetBrains decompiler
// Type: System.BoolBlit
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
