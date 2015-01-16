// Decompiled with JetBrains decompiler
// Type: VRageMath.MyShort4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
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