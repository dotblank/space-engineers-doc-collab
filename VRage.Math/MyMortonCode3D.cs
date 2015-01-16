// Decompiled with JetBrains decompiler
// Type: VRageMath.MyMortonCode3D
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System.Diagnostics;

namespace VRageMath
{
    public static class MyMortonCode3D
    {
        public static int Encode(ref Vector3I value)
        {
            return MyMortonCode3D.splitBits(value.X) | MyMortonCode3D.splitBits(value.Y) << 1 |
                   MyMortonCode3D.splitBits(value.Z) << 2;
        }

        public static void Decode(int code, out Vector3I value)
        {
            value.X = MyMortonCode3D.joinBits(code);
            value.Y = MyMortonCode3D.joinBits(code >> 1);
            value.Z = MyMortonCode3D.joinBits(code >> 2);
        }

        private static int splitBits(int x)
        {
            x = (x | x << 16) & 50331903;
            x = (x | x << 8) & 50393103;
            x = (x | x << 4) & 51130563;
            x = (x | x << 2) & 153391689;
            return x;
        }

        private static int joinBits(int x)
        {
            x &= 153391689;
            x = (x | x >> 2) & 51130563;
            x = (x | x >> 4) & 50393103;
            x = (x | x >> 8) & 50331903;
            x = (x | x >> 16) & 1023;
            return x;
        }

        [Conditional("DEBUG")]
        private static void AssertRange(Vector3I value)
        {
        }
    }
}