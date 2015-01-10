// Decompiled with JetBrains decompiler
// Type: VRageMath.MyUShort4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public struct MyUShort4
  {
    public ushort X;
    public ushort Y;
    public ushort Z;
    public ushort W;

    public MyUShort4(uint x, uint y, uint z, uint w)
    {
      this.X = (ushort) x;
      this.Y = (ushort) y;
      this.Z = (ushort) z;
      this.W = (ushort) w;
    }

    public static unsafe explicit operator ulong(MyUShort4 val)
    {
      return (ulong) *(long*) &val;
    }

    public static unsafe explicit operator MyUShort4(ulong val)
    {
      return *(MyUShort4*) &val;
    }
  }
}
