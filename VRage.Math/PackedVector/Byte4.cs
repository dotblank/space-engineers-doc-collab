// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.Byte4
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;
using VRageMath;

namespace VRageMath.PackedVector
{
    public struct Byte4 : IPackedVector<uint>, IPackedVector, IEquatable<Byte4>
    {
        private uint packedValue;

        public uint PackedValue
        {
            get { return this.packedValue; }
            set { this.packedValue = value; }
        }

        public Byte4(float x, float y, float z, float w)
        {
            this.packedValue = Byte4.PackHelper(x, y, z, w);
        }

        public Byte4(Vector4 vector)
        {
            this.packedValue = Byte4.PackHelper(vector.X, vector.Y, vector.Z, vector.W);
        }

        public Byte4(uint packedValue)
        {
            this.packedValue = packedValue;
        }

        public static bool operator ==(Byte4 a, Byte4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Byte4 a, Byte4 b)
        {
            return !a.Equals(b);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = Byte4.PackHelper(vector.X, vector.Y, vector.Z, vector.W);
        }

        private static uint PackHelper(float vectorX, float vectorY, float vectorZ, float vectorW)
        {
            return
                (uint)
                    ((int) PackUtils.PackUnsigned((float) byte.MaxValue, vectorX) |
                     (int) PackUtils.PackUnsigned((float) byte.MaxValue, vectorY) << 8 |
                     (int) PackUtils.PackUnsigned((float) byte.MaxValue, vectorZ) << 16 |
                     (int) PackUtils.PackUnsigned((float) byte.MaxValue, vectorW) << 24);
        }

        public Vector4 ToVector4()
        {
            Vector4 vector4;
            vector4.X = (float) (this.packedValue & (uint) byte.MaxValue);
            vector4.Y = (float) (this.packedValue >> 8 & (uint) byte.MaxValue);
            vector4.Z = (float) (this.packedValue >> 16 & (uint) byte.MaxValue);
            vector4.W = (float) (this.packedValue >> 24 & (uint) byte.MaxValue);
            return vector4;
        }

        public override string ToString()
        {
            return this.packedValue.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture);
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Byte4)
                return this.Equals((Byte4) obj);
            else
                return false;
        }

        public bool Equals(Byte4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }
    }
}