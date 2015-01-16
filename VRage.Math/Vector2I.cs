// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector2I
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Collections.Generic;

namespace VRageMath
{
    [ProtoContract]
    public struct Vector2I
    {
        public static readonly Vector2I.ComparerClass Comparer = new Vector2I.ComparerClass();
        public static Vector2I Zero = new Vector2I();
        public static Vector2I One = new Vector2I(1, 1);
        public static Vector2I UnitX = new Vector2I(1, 0);
        public static Vector2I UnitY = new Vector2I(0, 1);
        [ProtoMember(1)] public int X;
        [ProtoMember(2)] public int Y;

        public Vector2I(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2I(Vector2 vec)
        {
            this.X = (int) vec.X;
            this.Y = (int) vec.Y;
        }

        public static implicit operator Vector2(Vector2I intVector)
        {
            return new Vector2((float) intVector.X, (float) intVector.Y);
        }

        public static Vector2I operator +(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2I operator -(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2I operator *(Vector2I value1, int multiplier)
        {
            return new Vector2I(value1.X*multiplier, value1.Y*multiplier);
        }

        public static Vector2I operator /(Vector2I value1, int divider)
        {
            return new Vector2I(value1.X/divider, value1.Y/divider);
        }

        public override string ToString()
        {
            return (string) (object) this.X + (object) ", " + (string) (object) this.Y;
        }

        public int Size()
        {
            return Math.Abs(this.X*this.Y);
        }

        public class ComparerClass : IEqualityComparer<Vector2I>
        {
            public bool Equals(Vector2I x, Vector2I y)
            {
                return x.X == y.X & x.Y == y.Y;
            }

            public int GetHashCode(Vector2I obj)
            {
                return obj.X*397 ^ obj.Y;
            }
        }
    }
}