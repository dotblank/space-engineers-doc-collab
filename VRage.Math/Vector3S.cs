// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3S
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;

namespace VRageMath
{
    [ProtoContract]
    public struct Vector3S
    {
        [ProtoMember(1)] public short X;
        [ProtoMember(2)] public short Y;
        [ProtoMember(3)] public short Z;

        public Vector3S(Vector3I vec)
        {
            this = new Vector3S(ref vec);
        }

        public Vector3S(ref Vector3I vec)
        {
            this.X = (short) vec.X;
            this.Y = (short) vec.Y;
            this.Z = (short) vec.Z;
        }

        public Vector3S(short x, short y, short z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3S operator *(Vector3S v, short t)
        {
            return new Vector3S((short) ((int) t*(int) v.X), (short) ((int) t*(int) v.Y), (short) ((int) t*(int) v.Z));
        }

        public static Vector3 operator *(Vector3S v, float t)
        {
            return new Vector3(t*(float) v.X, t*(float) v.Y, t*(float) v.Z);
        }

        public static Vector3 operator *(Vector3 vector, Vector3S shortVector)
        {
            return shortVector*vector;
        }

        public static Vector3 operator *(Vector3S shortVector, Vector3 vector)
        {
            return new Vector3((float) shortVector.X*vector.X, (float) shortVector.Y*vector.Y,
                (float) shortVector.Z*vector.Z);
        }

        public static bool operator ==(Vector3S v1, Vector3S v2)
        {
            if ((int) v1.X == (int) v2.X && (int) v1.Y == (int) v2.Y)
                return (int) v1.Z == (int) v2.Z;
            else
                return false;
        }

        public static bool operator !=(Vector3S v1, Vector3S v2)
        {
            if ((int) v1.X == (int) v2.X && (int) v1.Y == (int) v2.Y)
                return (int) v1.Z != (int) v2.Z;
            else
                return true;
        }

        public override string ToString()
        {
            return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z;
        }

        public static Vector3S Round(Vector3 v)
        {
            return new Vector3S((short) Math.Round((double) v.X), (short) Math.Round((double) v.Y),
                (short) Math.Round((double) v.Z));
        }
    }
}