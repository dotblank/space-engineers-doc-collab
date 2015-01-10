// Decompiled with JetBrains decompiler
// Type: VRageMath.MyBounds
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public struct MyBounds
  {
    public float Min;
    public float Max;
    public float Default;

    public MyBounds(float min, float max, float def)
    {
      this.Min = min;
      this.Max = max;
      this.Default = def;
    }

    public float Normalize(float value)
    {
      return (float) (((double) value - (double) this.Min) / ((double) this.Max - (double) this.Min));
    }

    public float Clamp(float value)
    {
      return MathHelper.Clamp(value, this.Min, this.Max);
    }

    public override string ToString()
    {
      return string.Format("Min={0}, Max={1}, Default={2}", (object) this.Min, (object) this.Max, (object) this.Default);
    }
  }
}
