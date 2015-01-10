// Decompiled with JetBrains decompiler
// Type: VRageMath.MyShort4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public struct MyShort4
  {
    public short X;
    public short Y;
    public short Z;
    public short W;

    public static unsafe explicit operator ulong(MyShort4 val)
    {
      return (ulong) *(long*) &val;
    }

    public static unsafe explicit operator MyShort4(ulong val)
    {
      return *(MyShort4*) &val;
    }
  }
}
