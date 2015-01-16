// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.HalfVector4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using VRageMath;

namespace VRageMath.PackedVector
{
    public struct HalfVector4 : IPackedVector<ulong>, IPackedVector, IEquatable<HalfVector4>
    {
        private ulong packedValue;

        public ulong PackedValue
        {
            get { return this.packedValue; }
            set { this.packedValue = value; }
        }

        public HalfVector4(float x, float y, float z, float w)
        {
            this.packedValue = HalfVector4.PackHelper(x, y, z, w);
        }

        public HalfVector4(Vector4 vector)
        {
            this.packedValue = HalfVector4.PackHelper(vector.X, vector.Y, vector.Z, vector.W);
        }

        public static bool operator ==(HalfVector4 a, HalfVector4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HalfVector4 a, HalfVector4 b)
        {
            return !a.Equals(b);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = HalfVector4.PackHelper(vector.X, vector.Y, vector.Z, vector.W);
        }

        private static ulong PackHelper(float vectorX, float vectorY, float vectorZ, float vectorW)
        {
            return
                (ulong)
                    ((long) HalfUtils.Pack(vectorX) | (long) HalfUtils.Pack(vectorY) << 16 |
                     (long) HalfUtils.Pack(vectorZ) << 32 | (long) HalfUtils.Pack(vectorW) << 48);
        }

        public Vector4 ToVector4()
        {
            Vector4 vector4;
            vector4.X = HalfUtils.Unpack((ushort) this.packedValue);
            vector4.Y = HalfUtils.Unpack((ushort) (this.packedValue >> 16));
            vector4.Z = HalfUtils.Unpack((ushort) (this.packedValue >> 32));
            vector4.W = HalfUtils.Unpack((ushort) (this.packedValue >> 48));
            return vector4;
        }

        public override string ToString()
        {
            return this.ToVector4().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is HalfVector4)
                return this.Equals((HalfVector4) obj);
            else
                return false;
        }

        public bool Equals(HalfVector4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }
    }
}