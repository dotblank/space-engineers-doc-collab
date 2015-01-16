// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3Ushort
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;

namespace VRageMath
{
    [ProtoContract]
    public struct Vector3Ushort
    {
        [ProtoMember(1)] public ushort X;
        [ProtoMember(2)] public ushort Y;
        [ProtoMember(3)] public ushort Z;

        public Vector3Ushort(ushort x, ushort y, ushort z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static explicit operator Vector3(Vector3Ushort v)
        {
            return new Vector3((float) v.X, (float) v.Y, (float) v.Z);
        }

        public static Vector3Ushort operator *(Vector3Ushort v, ushort t)
        {
            return new Vector3Ushort((ushort) ((uint) t*(uint) v.X), (ushort) ((uint) t*(uint) v.Y),
                (ushort) ((uint) t*(uint) v.Z));
        }

        public static Vector3 operator *(Vector3 vector, Vector3Ushort ushortVector)
        {
            return ushortVector*vector;
        }

        public static Vector3 operator *(Vector3Ushort ushortVector, Vector3 vector)
        {
            return new Vector3((float) ushortVector.X*vector.X, (float) ushortVector.Y*vector.Y,
                (float) ushortVector.Z*vector.Z);
        }

        public override string ToString()
        {
            return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z;
        }
    }
}