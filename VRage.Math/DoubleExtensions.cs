// Decompiled with JetBrains decompiler
// Type: VRageMath.DoubleExtensions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public static class DoubleExtensions
  {
    public static bool IsValid(this double f)
    {
      if (!double.IsNaN(f))
        return !double.IsInfinity(f);
      else
        return false;
    }
  }
}
