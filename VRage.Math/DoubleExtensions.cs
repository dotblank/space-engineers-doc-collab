// Decompiled with JetBrains decompiler
// Type: VRageMath.DoubleExtensions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
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