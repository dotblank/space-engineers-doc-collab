// Decompiled with JetBrains decompiler
// Type: VRageMath.MyUShort4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
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
