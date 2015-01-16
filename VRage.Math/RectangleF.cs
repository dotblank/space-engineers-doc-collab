// Decompiled with JetBrains decompiler
// Type: VRageMath.RectangleF
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
    [Serializable]
    public struct RectangleF : IEquatable<RectangleF>
    {
        public Vector2 Position;
        public Vector2 Size;

        public float X
        {
            get { return this.Position.X; }
            set { this.Position.X = value; }
        }

        public float Y
        {
            get { return this.Position.Y; }
            set { this.Position.Y = value; }
        }

        public float Width
        {
            get { return this.Size.X; }
            set { this.Size.X = value; }
        }

        public float Height
        {
            get { return this.Size.Y; }
            set { this.Size.Y = value; }
        }

        public RectangleF(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        public RectangleF(float x, float y, float width, float height)
        {
            this.Position = new Vector2(x, y);
            this.Size = new Vector2(width, height);
        }

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !left.Equals(right);
        }

        public bool Contains(int x, int y)
        {
            return (double) x >= (double) this.X && (double) x <= (double) this.X + (double) this.Width &&
                   ((double) y >= (double) this.Y && (double) y <= (double) this.Y + (double) this.Height);
        }

        public bool Contains(float x, float y)
        {
            return (double) x >= (double) this.X && (double) x <= (double) this.X + (double) this.Width &&
                   ((double) y >= (double) this.Y && (double) y <= (double) this.Y + (double) this.Height);
        }

        public bool Contains(Vector2 vector2D)
        {
            return (double) vector2D.X >= (double) this.X &&
                   (double) vector2D.X <= (double) this.X + (double) this.Width &&
                   ((double) vector2D.Y >= (double) this.Y &&
                    (double) vector2D.Y <= (double) this.Y + (double) this.Height);
        }

        public bool Contains(Point point)
        {
            return (double) point.X >= (double) this.X && (double) point.X <= (double) this.X + (double) this.Width &&
                   ((double) point.Y >= (double) this.Y && (double) point.Y <= (double) this.Y + (double) this.Height);
        }

        public bool Equals(RectangleF other)
        {
            if (other.X.Equals(this.X) && other.Y.Equals(this.Y) && other.Width.Equals(this.Width))
                return other.Height.Equals(this.Height);
            else
                return false;
        }

        public static void Intersect(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
        {
            float num1 = value1.X + value1.Width;
            float num2 = value2.X + value2.Width;
            float num3 = value1.Y + value1.Height;
            float num4 = value2.Y + value2.Height;
            float x = (double) value1.X > (double) value2.X ? value1.X : value2.X;
            float y = (double) value1.Y > (double) value2.Y ? value1.Y : value2.Y;
            float num5 = (double) num1 < (double) num2 ? num1 : num2;
            float num6 = (double) num3 < (double) num4 ? num3 : num4;
            if ((double) num5 > (double) x && (double) num6 > (double) y)
                result = new RectangleF(x, y, num5 - x, num6 - y);
            else
                result = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals((object) null, obj) || obj.GetType() != typeof (RectangleF))
                return false;
            else
                return this.Equals((RectangleF) obj);
        }

        public override int GetHashCode()
        {
            return ((this.X.GetHashCode()*397 ^ this.Y.GetHashCode())*397 ^ this.Width.GetHashCode())*397 ^
                   this.Height.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("(X: {0} Y: {1} W: {2} H: {3})", (object) this.X, (object) this.Y, (object) this.Width,
                (object) this.Height);
        }
    }
}