// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyEncounterId
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public struct MyEncounterId
    {
        [ProtoMember(1)] public BoundingBoxD BoundingBox;
        [ProtoMember(2)] public int Seed;

        public MyEncounterId(BoundingBoxD box, int seed)
        {
            this.BoundingBox = box;
            this.Seed = seed;
        }

        public static bool operator ==(MyEncounterId x, MyEncounterId y)
        {
            if (x.BoundingBox == y.BoundingBox)
                return x.Seed == y.Seed;
            else
                return false;
        }

        public static bool operator !=(MyEncounterId x, MyEncounterId y)
        {
            return !(x == y);
        }

        public override bool Equals(object o)
        {
            try
            {
                return this == (MyEncounterId) o;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.BoundingBox.GetHashCode() << 16 ^ this.Seed;
        }
    }
}