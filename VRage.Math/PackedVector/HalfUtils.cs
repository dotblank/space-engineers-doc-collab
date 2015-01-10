// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.HalfUtils
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath.PackedVector
{
  internal static class HalfUtils
  {
    private const int cFracBits = 10;
    private const int cExpBits = 5;
    private const int cSignBit = 15;
    private const uint cSignMask = 32768U;
    private const uint cFracMask = 1023U;
    private const int cExpBias = 15;
    private const uint cRoundBit = 4096U;
    private const uint eMax = 16U;
    private const int eMin = -14;
    private const uint wMaxNormal = 1207955455U;
    private const uint wMinNormal = 947912704U;
    private const uint BiasDiffo = 3355443200U;
    private const int cFracBitsDiff = 13;

    public static unsafe ushort Pack(float value)
    {
        return 0;
    }

    public static unsafe float Unpack(ushort value)
    {
      uint num1;
      if (((int) value & -33792) == 0)
      {
        if (((int) value & 1023) != 0)
        {
          uint num2 = 4294967282U;
          uint num3 = (uint) value & 1023U;
          while (((int) num3 & 1024) == 0)
          {
            --num2;
            num3 <<= 1;
          }
          uint num4 = num3 & 4294966271U;
          num1 = (uint) (((int) value & 32768) << 16 | (int) num2 + (int) sbyte.MaxValue << 23 | (int) num4 << 13);
        }
        else
          num1 = (uint) (((int) value & 32768) << 16);
      }
      else
        num1 = (uint) (((int) value & 32768) << 16 | ((int) value >> 10 & 31) - 15 + (int) sbyte.MaxValue << 23 | ((int) value & 1023) << 13);
      return *(float*) &num1;
    }
  }
}
