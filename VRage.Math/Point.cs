// Decompiled with JetBrains decompiler
// Type: VRageMath.Point
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;

namespace VRageMath
{
    [Serializable]
    public struct Point : IEquatable<Point>
    {
        private static Point _zero = new Point();
        public int X;
        public int Y;

        public static Point Zero
        {
            get { return Point._zero; }
        }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            if (a.X == b.X)
                return a.Y != b.Y;
            else
                return true;
        }

        public bool Equals(Point other)
        {
            if (this.X == other.X)
                return this.Y == other.Y;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Point)
                flag = this.Equals((Point) obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode();
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider) currentCulture, "{{X:{0} Y:{1}}}", new object[2]
            {
                (object) this.X.ToString((IFormatProvider) currentCulture),
                (object) this.Y.ToString((IFormatProvider) currentCulture)
            });
        }
    }
}