// Decompiled with JetBrains decompiler
// Type: VRageMath.FloatExtensions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
    public static class FloatExtensions
    {
        public static bool IsValid(this float f)
        {
            if (!float.IsNaN(f))
                return !float.IsInfinity(f);
            else
                return false;
        }

        public static bool IsEqual(this float f, float other, float epsilon = 0.0001f)
        {
            return FloatExtensions.IsZero(f - other, epsilon);
        }

        public static bool IsZero(this float f, float epsilon = 0.0001f)
        {
            return (double) Math.Abs(f) < (double) epsilon;
        }
    }
}