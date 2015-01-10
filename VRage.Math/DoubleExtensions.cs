// Decompiled with JetBrains decompiler
// Type: VRageMath.DoubleExtensions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
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
