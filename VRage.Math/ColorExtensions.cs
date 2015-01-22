// Decompiled with JetBrains decompiler
// Type: VRageMath.ColorExtensions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  public static class ColorExtensions
  {
    public static Vector3 ColorToHSV(this Color rgb)
    {
      System.Drawing.Color color = System.Drawing.Color.FromArgb((int) rgb.R, (int) rgb.G, (int) rgb.B);
      int num1 = (int) Math.Max(color.R, Math.Max(color.G, color.B));
      int num2 = (int) Math.Min(color.R, Math.Min(color.G, color.B));
      return new Vector3(color.GetHue() / 360f, num1 == 0 ? 0.0f : (float) (1.0 - 1.0 * (double) num2 / (double) num1), (float) num1 / (float) byte.MaxValue);
    }

    private static Vector3 Hue(float H)
    {
      return new Vector3(MathHelper.Clamp(Math.Abs((float) ((double) H * 6.0 - 3.0)) - 1f, 0.0f, 1f), MathHelper.Clamp(2f - Math.Abs((float) ((double) H * 6.0 - 2.0)), 0.0f, 1f), MathHelper.Clamp(2f - Math.Abs((float) ((double) H * 6.0 - 4.0)), 0.0f, 1f));
    }

    public static Color HSVtoColor(this Vector3 HSV)
    {
      return new Color(((ColorExtensions.Hue(HSV.X) - 1f) * HSV.Y + 1f) * HSV.Z);
    }

    public static uint PackHSVToUint(this Vector3 HSV)
    {
      int num1 = (int) Math.Round((double) HSV.X * 360.0);
      int num2 = (int) Math.Round((double) HSV.Y * 100.0 + 100.0);
      int num3 = (int) Math.Round((double) HSV.Z * 100.0 + 100.0);
      int num4 = num2 << 16;
      int num5 = num3 << 24;
      return (uint) (num1 | num4 | num5);
    }

    public static Vector3 UnpackHSVFromUint(uint packed)
    {
      return new Vector3((float) (ushort) packed / 360f, (float) ((int) (byte) (packed >> 16) - 100) / 100f, (float) ((int) (byte) (packed >> 24) - 100) / 100f);
    }

    public static float HueDistance(this Color color, float hue)
    {
      float val1 = Math.Abs(ColorExtensions.ColorToHSV(color).X - hue);
      return Math.Min(val1, 1f - val1);
    }

    public static float HueDistance(this Color color, Color otherColor)
    {
      return ColorExtensions.HueDistance(color, ColorExtensions.ColorToHSV(otherColor).X);
    }
  }
}
