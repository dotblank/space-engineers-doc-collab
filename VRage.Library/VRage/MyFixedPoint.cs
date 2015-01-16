// Decompiled with JetBrains decompiler
// Type: VRage.MyFixedPoint
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VRage
{
    [ProtoContract]
    public struct MyFixedPoint : IXmlSerializable
    {
        private static readonly string FormatSpecifier = "D" + (object) 7;

        private static readonly char[] TrimChars = new char[1]
        {
            '0'
        };

        public static readonly MyFixedPoint MinValue = new MyFixedPoint(long.MinValue);
        public static readonly MyFixedPoint MaxValue = new MyFixedPoint(long.MaxValue);
        public static readonly MyFixedPoint SmallestPossibleValue = new MyFixedPoint(1L);
        private const int Places = 6;
        private const int Divider = 1000000;
        [ProtoMember(1)] public long RawValue;

        private MyFixedPoint(long rawValue)
        {
            this.RawValue = rawValue;
        }

        public static explicit operator MyFixedPoint(float d)
        {
            return new MyFixedPoint((long) ((double) d*1000000.0 + 0.5));
        }

        public static explicit operator MyFixedPoint(double d)
        {
            return new MyFixedPoint((long) (d*1000000.0 + 0.5));
        }

        public static explicit operator MyFixedPoint(Decimal d)
        {
            return new MyFixedPoint((long) (d*new Decimal(1000000) + new Decimal(5, 0, 0, false, (byte) 1)));
        }

        public static implicit operator MyFixedPoint(int i)
        {
            return new MyFixedPoint((long) i*1000000L);
        }

        public static explicit operator Decimal(MyFixedPoint fp)
        {
            return (Decimal) fp.RawValue/new Decimal(1000000);
        }

        public static explicit operator float(MyFixedPoint fp)
        {
            return (float) fp.RawValue/1000000f;
        }

        public static explicit operator double(MyFixedPoint fp)
        {
            return (double) fp.RawValue/1000000.0;
        }

        public static explicit operator int(MyFixedPoint fp)
        {
            return (int) (fp.RawValue/1000000L);
        }

        public static bool operator <(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue < b.RawValue;
        }

        public static bool operator >(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue > b.RawValue;
        }

        public static bool operator <=(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue <= b.RawValue;
        }

        public static bool operator >=(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue >= b.RawValue;
        }

        public static bool operator ==(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue == b.RawValue;
        }

        public static bool operator !=(MyFixedPoint a, MyFixedPoint b)
        {
            return a.RawValue != b.RawValue;
        }

        public static MyFixedPoint operator +(MyFixedPoint a, MyFixedPoint b)
        {
            a.RawValue += b.RawValue;
            return a;
        }

        public static MyFixedPoint operator -(MyFixedPoint a, MyFixedPoint b)
        {
            a.RawValue -= b.RawValue;
            return a;
        }

        public static MyFixedPoint operator *(MyFixedPoint a, MyFixedPoint b)
        {
            long num1 = a.RawValue/1000000L;
            long num2 = b.RawValue/1000000L;
            long num3 = a.RawValue%1000000L;
            long num4 = b.RawValue%1000000L;
            return new MyFixedPoint(num1*num2*1000000L + num3*num4/1000000L + num1*num4 + num2*num3);
        }

        public static MyFixedPoint operator *(MyFixedPoint a, float b)
        {
            return a*(MyFixedPoint) b;
        }

        public static MyFixedPoint operator *(float a, MyFixedPoint b)
        {
            return (MyFixedPoint) a*b;
        }

        public static MyFixedPoint operator *(MyFixedPoint a, int b)
        {
            return a*(MyFixedPoint) b;
        }

        public static MyFixedPoint operator *(int a, MyFixedPoint b)
        {
            return (MyFixedPoint) a*b;
        }

        public string SerializeString()
        {
            string str1 = this.RawValue.ToString(MyFixedPoint.FormatSpecifier);
            string str2 = str1.Substring(0, str1.Length - 6);
            string str3 = str1.Substring(str1.Length - 6).TrimEnd(MyFixedPoint.TrimChars);
            if (str3.Length > 0)
                return str2 + "." + str3;
            else
                return str2;
        }

        public static MyFixedPoint DeserializeStringSafe(string text)
        {
            for (int index = 0; index < text.Length; ++index)
            {
                char ch = text[index];
                if (((int) ch < 48 || (int) ch > 57) && (int) ch != 46 && ((int) ch != 45 || index != 0))
                    return (MyFixedPoint) double.Parse(text);
            }
            try
            {
                return MyFixedPoint.DeserializeString(text);
            }
            catch
            {
                return (MyFixedPoint) double.Parse(text);
            }
        }

        public static MyFixedPoint DeserializeString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new MyFixedPoint();
            int num = text.IndexOf('.');
            if (num == -1)
                return new MyFixedPoint(long.Parse(text)*1000000L);
            text = text.Replace(".", "");
            text = text.PadRight(num + 1 + 6, '0');
            text = text.Substring(0, num + 6);
            return new MyFixedPoint(long.Parse(text));
        }

        public static MyFixedPoint Ceiling(MyFixedPoint a)
        {
            a.RawValue = (a.RawValue + 1000000L - 1L)/1000000L*1000000L;
            return a;
        }

        public static MyFixedPoint Floor(MyFixedPoint a)
        {
            a.RawValue = a.RawValue/1000000L*1000000L;
            return a;
        }

        public static MyFixedPoint Min(MyFixedPoint a, MyFixedPoint b)
        {
            if (!(a < b))
                return b;
            else
                return a;
        }

        public static MyFixedPoint Max(MyFixedPoint a, MyFixedPoint b)
        {
            if (!(a > b))
                return b;
            else
                return a;
        }

        public static MyFixedPoint Round(MyFixedPoint a)
        {
            a.RawValue = (a.RawValue + 500000L)/1000000L;
            return a;
        }

        public override string ToString()
        {
            return this.SerializeString();
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return (XmlSchema) null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            this.RawValue = MyFixedPoint.DeserializeStringSafe(reader.ReadInnerXml()).RawValue;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.SerializeString());
        }
    }
}