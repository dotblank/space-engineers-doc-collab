// Decompiled with JetBrains decompiler
// Type: System.Text.StringBuilderExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using VRage;

namespace System.Text
{
    public static class StringBuilderExtensions
    {
        private static readonly char[] ms_digits = new char[16]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        };

        private static readonly uint ms_default_decimal_places = 5U;
        private static readonly char ms_default_pad_char = '0';
        private static NumberFormatInfo m_numberFormatInfoHelper;

        static StringBuilderExtensions()
        {
            if (StringBuilderExtensions.m_numberFormatInfoHelper != null)
                return;
            StringBuilderExtensions.m_numberFormatInfoHelper =
                CultureInfo.InvariantCulture.NumberFormat.Clone() as NumberFormatInfo;
        }

        public static StringBuilder AppendStringBuilder(this StringBuilder stringBuilder,
            StringBuilder otherStringBuilder)
        {
            stringBuilder.EnsureCapacity(stringBuilder.Length + otherStringBuilder.Length);
            for (int index = 0; index < otherStringBuilder.Length; ++index)
                stringBuilder.Append(otherStringBuilder[index]);
            return stringBuilder;
        }

        public static StringBuilder AppendSubstring(this StringBuilder stringBuilder, StringBuilder append, int start,
            int count)
        {
            stringBuilder.EnsureCapacity(stringBuilder.Length + count);
            for (int index = 0; index < count; ++index)
                stringBuilder.Append(append[start + index]);
            return stringBuilder;
        }

        public static StringBuilder ConcatFormat<A>(this StringBuilder string_builder, string format_string, A arg1,
            NumberFormatInfo numberFormat = null) where A : IConvertible
        {
            return StringBuilderExtensions.ConcatFormat<A, int, int, int>(string_builder, format_string, arg1, 0, 0, 0,
                numberFormat);
        }

        public static StringBuilder ConcatFormat<A, B>(this StringBuilder string_builder, string format_string, A arg1,
            B arg2, NumberFormatInfo numberFormat = null) where A : IConvertible where B : IConvertible
        {
            return StringBuilderExtensions.ConcatFormat<A, B, int, int>(string_builder, format_string, arg1, arg2, 0, 0,
                numberFormat);
        }

        public static StringBuilder ConcatFormat<A, B, C>(this StringBuilder string_builder, string format_string,
            A arg1, B arg2, C arg3, NumberFormatInfo numberFormat = null) where A : IConvertible where B : IConvertible
            where C : IConvertible
        {
            return StringBuilderExtensions.ConcatFormat<A, B, C, int>(string_builder, format_string, arg1, arg2, arg3, 0,
                numberFormat);
        }

        public static StringBuilder ConcatFormat<A, B, C, D>(this StringBuilder string_builder, string format_string,
            A arg1, B arg2, C arg3, D arg4, NumberFormatInfo numberFormat = null) where A : IConvertible
            where B : IConvertible where C : IConvertible where D : IConvertible
        {
            int startIndex = 0;
            numberFormat = numberFormat ?? CultureInfo.InvariantCulture.NumberFormat;
            for (int index1 = 0; index1 < format_string.Length; ++index1)
            {
                if ((int) format_string[index1] == 123)
                {
                    if (startIndex < index1)
                        string_builder.Append(format_string, startIndex, index1 - startIndex);
                    uint base_value = 10U;
                    uint padding = 0U;
                    uint decimal_places = (uint) numberFormat.NumberDecimalDigits;
                    bool thousandSeparation = !string.IsNullOrEmpty(numberFormat.NumberGroupSeparator);
                    int index2 = index1 + 1;
                    char ch = format_string[index2];
                    if ((int) ch == 123)
                    {
                        string_builder.Append('{');
                        index1 = index2 + 1;
                    }
                    else
                    {
                        index1 = index2 + 1;
                        if ((int) format_string[index1] == 58)
                        {
                            ++index1;
                            while ((int) format_string[index1] == 48)
                            {
                                ++index1;
                                ++padding;
                            }
                            if ((int) format_string[index1] == 88)
                            {
                                ++index1;
                                base_value = 16U;
                                if ((int) format_string[index1] >= 48 && (int) format_string[index1] <= 57)
                                {
                                    padding = (uint) format_string[index1] - 48U;
                                    ++index1;
                                }
                            }
                            else if ((int) format_string[index1] == 46)
                            {
                                ++index1;
                                decimal_places = 0U;
                                while ((int) format_string[index1] == 48)
                                {
                                    ++index1;
                                    ++decimal_places;
                                }
                            }
                        }
                        while ((int) format_string[index1] != 125)
                            ++index1;
                        switch (ch)
                        {
                            case '0':
                                StringBuilderExtensions.ConcatFormatValue<A>(string_builder, arg1, padding, base_value,
                                    decimal_places, thousandSeparation);
                                break;
                            case '1':
                                StringBuilderExtensions.ConcatFormatValue<B>(string_builder, arg2, padding, base_value,
                                    decimal_places, thousandSeparation);
                                break;
                            case '2':
                                StringBuilderExtensions.ConcatFormatValue<C>(string_builder, arg3, padding, base_value,
                                    decimal_places, thousandSeparation);
                                break;
                            case '3':
                                StringBuilderExtensions.ConcatFormatValue<D>(string_builder, arg4, padding, base_value,
                                    decimal_places, thousandSeparation);
                                break;
                        }
                    }
                    startIndex = index1 + 1;
                }
            }
            if (startIndex < format_string.Length)
                string_builder.Append(format_string, startIndex, format_string.Length - startIndex);
            return string_builder;
        }

        private static void ConcatFormatValue<T>(this StringBuilder string_builder, T arg, uint padding, uint base_value,
            uint decimal_places, bool thousandSeparation) where T : IConvertible
        {
            switch (arg.GetTypeCode())
            {
                case TypeCode.Boolean:
                    if (arg.ToBoolean((IFormatProvider) CultureInfo.InvariantCulture))
                    {
                        string_builder.Append("true");
                        break;
                    }
                    else
                    {
                        string_builder.Append("false");
                        break;
                    }
                case TypeCode.Int32:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToInt32((IFormatProvider) NumberFormatInfo.InvariantInfo), padding, '0', base_value,
                        thousandSeparation);
                    break;
                case TypeCode.UInt32:
                    StringBuilderExtensions.Concat(string_builder,
                        (long) arg.ToUInt32((IFormatProvider) NumberFormatInfo.InvariantInfo), padding, '0', base_value,
                        thousandSeparation);
                    break;
                case TypeCode.Int64:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToInt64((IFormatProvider) NumberFormatInfo.InvariantInfo), padding, '0', base_value,
                        thousandSeparation);
                    break;
                case TypeCode.UInt64:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToInt32((IFormatProvider) NumberFormatInfo.InvariantInfo), padding, '0', base_value,
                        thousandSeparation);
                    break;
                case TypeCode.Single:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToSingle((IFormatProvider) NumberFormatInfo.InvariantInfo), decimal_places, padding, '0',
                        false);
                    break;
                case TypeCode.Double:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToDouble((IFormatProvider) NumberFormatInfo.InvariantInfo), decimal_places, padding, '0',
                        false);
                    break;
                case TypeCode.Decimal:
                    StringBuilderExtensions.Concat(string_builder,
                        arg.ToSingle((IFormatProvider) NumberFormatInfo.InvariantInfo), decimal_places, padding, '0',
                        false);
                    break;
                case TypeCode.String:
                    string_builder.Append(arg.ToString());
                    break;
            }
        }

        public static StringBuilder ToUpper(this StringBuilder self)
        {
            for (int index = 0; index < self.Length; ++index)
                self[index] = char.ToUpper(self[index]);
            return self;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, ulong uint_val, uint pad_amount,
            char pad_char, uint base_val, bool thousandSeparation)
        {
            uint val2 = 0U;
            ulong num1 = uint_val;
            int num2 = 0;
            do
            {
                ++num2;
                if (thousandSeparation && num2%4 == 0)
                {
                    ++val2;
                }
                else
                {
                    num1 /= (ulong) base_val;
                    ++val2;
                }
            } while (num1 > 0UL);
            string_builder.Append(pad_char, (int) Math.Max(pad_amount, val2));
            int length = string_builder.Length;
            int num3 = 0;
            while (val2 > 0U)
            {
                --length;
                ++num3;
                if (thousandSeparation && num3%4 == 0)
                {
                    --val2;
                    string_builder[length] = NumberFormatInfo.InvariantInfo.NumberGroupSeparator[0];
                }
                else
                {
                    string_builder[length] = StringBuilderExtensions.ms_digits[uint_val%(ulong) base_val];
                    uint_val /= (ulong) base_val;
                    --val2;
                }
            }
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val)
        {
            StringBuilderExtensions.Concat(string_builder, (long) uint_val, 0U,
                StringBuilderExtensions.ms_default_pad_char, 10U, true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount)
        {
            StringBuilderExtensions.Concat(string_builder, (long) uint_val, pad_amount,
                StringBuilderExtensions.ms_default_pad_char, 10U, true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount,
            char pad_char)
        {
            StringBuilderExtensions.Concat(string_builder, (long) uint_val, pad_amount, pad_char, 10U, true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount,
            char pad_char, uint base_val, bool thousandSeparation)
        {
            if (int_val < 0)
            {
                string_builder.Append('-');
                uint num = (uint) (-1 - int_val + 1);
                StringBuilderExtensions.Concat(string_builder, (long) num, pad_amount, pad_char, base_val,
                    thousandSeparation);
            }
            else
                StringBuilderExtensions.Concat(string_builder, (long) (uint) int_val, pad_amount, pad_char, base_val,
                    thousandSeparation);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, long long_val, uint pad_amount,
            char pad_char, uint base_val, bool thousandSeparation)
        {
            if (long_val < 0L)
            {
                string_builder.Append('-');
                ulong uint_val = (ulong) (-1L - long_val) + 1UL;
                StringBuilderExtensions.Concat(string_builder, uint_val, pad_amount, pad_char, base_val,
                    thousandSeparation);
            }
            else
                StringBuilderExtensions.Concat(string_builder, (ulong) long_val, pad_amount, pad_char, base_val,
                    thousandSeparation);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, int int_val)
        {
            StringBuilderExtensions.Concat(string_builder, int_val, 0U, StringBuilderExtensions.ms_default_pad_char, 10U,
                true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount)
        {
            StringBuilderExtensions.Concat(string_builder, int_val, pad_amount,
                StringBuilderExtensions.ms_default_pad_char, 10U, true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount,
            char pad_char)
        {
            StringBuilderExtensions.Concat(string_builder, int_val, pad_amount, pad_char, 10U, true);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places,
            uint pad_amount, char pad_char, bool thousandSeparator)
        {
            if ((int) decimal_places == 0)
            {
                long long_val = (double) float_val < 0.0
                    ? (long) ((double) float_val - 0.5)
                    : (long) ((double) float_val + 0.5);
                StringBuilderExtensions.Concat(string_builder, long_val, pad_amount, pad_char, 10U, thousandSeparator);
            }
            else
            {
                float num1 = 0.5f;
                for (int index = 0; (long) index < (long) decimal_places; ++index)
                    num1 *= 0.1f;
                float_val += (double) float_val >= 0.0 ? num1 : -num1;
                int int_val = (int) float_val;
                if (int_val == 0 && (double) float_val < 0.0)
                    string_builder.Append('-');
                StringBuilderExtensions.Concat(string_builder, int_val, pad_amount, pad_char, 10U, thousandSeparator);
                string_builder.Append('.');
                float num2 = Math.Abs(float_val - (float) int_val);
                uint num3 = decimal_places;
                do
                {
                    num2 *= 10f;
                    --num3;
                } while (num3 > 0U);
                StringBuilderExtensions.Concat(string_builder, (long) (uint) num2, decimal_places, '0', 10U,
                    thousandSeparator);
            }
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, float float_val)
        {
            StringBuilderExtensions.Concat(string_builder, float_val, StringBuilderExtensions.ms_default_decimal_places,
                0U, StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places)
        {
            StringBuilderExtensions.Concat(string_builder, float_val, decimal_places, 0U,
                StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places,
            uint pad_amount)
        {
            StringBuilderExtensions.Concat(string_builder, float_val, decimal_places, pad_amount,
                StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, double double_val, uint decimal_places,
            uint pad_amount, char pad_char, bool thousandSeparator)
        {
            if ((int) decimal_places == 0)
            {
                long long_val = double_val < 0.0 ? (long) (double_val - 0.5) : (long) (double_val + 0.5);
                StringBuilderExtensions.Concat(string_builder, long_val, pad_amount, pad_char, 10U, thousandSeparator);
            }
            else
            {
                double num1 = 0.5;
                for (int index = 0; (long) index < (long) decimal_places; ++index)
                    num1 *= 0.100000001490116;
                double_val += double_val >= 0.0 ? num1 : -num1;
                int int_val = (int) double_val;
                if (int_val == 0 && double_val < 0.0)
                    string_builder.Append('-');
                StringBuilderExtensions.Concat(string_builder, int_val, pad_amount, pad_char, 10U, thousandSeparator);
                string_builder.Append('.');
                double num2 = Math.Abs(double_val - (double) int_val);
                uint num3 = decimal_places;
                do
                {
                    num2 *= 10.0;
                    --num3;
                } while (num3 > 0U);
                StringBuilderExtensions.Concat(string_builder, (long) (uint) num2, decimal_places, '0', 10U,
                    thousandSeparator);
            }
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, double double_val)
        {
            StringBuilderExtensions.Concat(string_builder, double_val, StringBuilderExtensions.ms_default_decimal_places,
                0U, StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, double double_val, uint decimal_places)
        {
            StringBuilderExtensions.Concat(string_builder, double_val, decimal_places, 0U,
                StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static StringBuilder Concat(this StringBuilder string_builder, double double_val, uint decimal_places,
            uint pad_amount)
        {
            StringBuilderExtensions.Concat(string_builder, double_val, decimal_places, pad_amount,
                StringBuilderExtensions.ms_default_pad_char, false);
            return string_builder;
        }

        public static bool CompareUpdate(this StringBuilder sb, string text)
        {
            if (StringBuilderExtensions.CompareTo(sb, text) == 0)
                return false;
            sb.Clear();
            sb.Append(text);
            return true;
        }

        public static bool CompareUpdate(this StringBuilder sb, StringBuilder text)
        {
            if (StringBuilderExtensions.CompareTo(sb, text) == 0)
                return false;
            sb.Clear();
            StringBuilderExtensions.AppendStringBuilder(sb, text);
            return true;
        }

        public static void TrimEnd(this StringBuilder sb, int length)
        {
            Exceptions.ThrowIf<ArgumentException>(length > sb.Length,
                "String builder contains less characters then requested number!");
            sb.Length -= length;
        }

        public static StringBuilder GetFormatedLong(this StringBuilder sb, string before, long value, string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.ConcatFormat<string, long, string>(sb, "{0}{1: #,0}{2}", before, value, after,
                (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder GetFormatedInt(this StringBuilder sb, string before, int value, string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.ConcatFormat<string, int, string>(sb, "{0}{1: #,0}{2}", before, value, after,
                (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder GetFormatedFloat(this StringBuilder sb, string before, float value,
            string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.ConcatFormat<string, float, string>(sb, "{0}{1: #,0}{2}", before, value, after,
                (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder GetFormatedBool(this StringBuilder sb, string before, bool value, string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.ConcatFormat<string, bool, string>(sb, "{0}{1}{2}", before, value, after,
                (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder GetFormatedDateTimeOffset(this StringBuilder sb, string before, DateTimeOffset value,
            string after = "")
        {
            return StringBuilderExtensions.GetFormatedDateTimeOffset(sb, before, value.DateTime, after);
        }

        public static StringBuilder GetFormatedDateTimeOffset(this StringBuilder sb, string before, DateTime value,
            string after = "")
        {
            sb.Clear();
            sb.Append(before);
            StringBuilderExtensions.Concat(sb, value.Year, 4U, '0', 10U, false);
            sb.Append("-");
            StringBuilderExtensions.Concat(sb, value.Month, 2U);
            sb.Append("-");
            StringBuilderExtensions.Concat(sb, value.Day, 2U);
            sb.Append(" ");
            StringBuilderExtensions.Concat(sb, value.Hour, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Minute, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Second, 2U);
            sb.Append(".");
            StringBuilderExtensions.Concat(sb, value.Millisecond, 3U);
            sb.Append(after);
            return sb;
        }

        public static StringBuilder GetFormatedDateTime(this StringBuilder sb, DateTime value)
        {
            sb.Clear();
            StringBuilderExtensions.Concat(sb, value.Day, 2U);
            sb.Append("/");
            StringBuilderExtensions.Concat(sb, value.Month, 2U);
            sb.Append("/");
            StringBuilderExtensions.Concat(sb, value.Year, 0U, '0', 10U, false);
            sb.Append(" ");
            StringBuilderExtensions.Concat(sb, value.Hour, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Minute, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Second, 2U);
            return sb;
        }

        public static StringBuilder GetFormatedDateTimeForFilename(this StringBuilder sb, DateTime value)
        {
            sb.Clear();
            StringBuilderExtensions.Concat(sb, value.Year, 0U, '0', 10U, false);
            StringBuilderExtensions.Concat(sb, value.Month, 2U);
            StringBuilderExtensions.Concat(sb, value.Day, 2U);
            sb.Append("_");
            StringBuilderExtensions.Concat(sb, value.Hour, 2U);
            StringBuilderExtensions.Concat(sb, value.Minute, 2U);
            StringBuilderExtensions.Concat(sb, value.Second, 2U);
            return sb;
        }

        public static StringBuilder GetFormatedTimeSpan(this StringBuilder sb, string before, TimeSpan value,
            string after = "")
        {
            sb.Clear();
            sb.Clear();
            sb.Append(before);
            StringBuilderExtensions.Concat(sb, value.Hours, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Minutes, 2U);
            sb.Append(":");
            StringBuilderExtensions.Concat(sb, value.Seconds, 2U);
            sb.Append(".");
            StringBuilderExtensions.Concat(sb, value.Milliseconds, 3U);
            sb.Append(after);
            return sb;
        }

        public static StringBuilder GetStrings(this StringBuilder sb, string before, string value = "",
            string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.ConcatFormat<string, string, string>(sb, "{0}{1}{2}", before, value, after,
                (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder AppendFormatedDecimal(this StringBuilder sb, string before, float value,
            int decimalDigits, string after = "")
        {
            sb.Clear();
            StringBuilderExtensions.m_numberFormatInfoHelper.NumberDecimalDigits = decimalDigits;
            StringBuilderExtensions.ConcatFormat<string, float, string>(sb, "{0}{1 }{2}", before, value, after,
                StringBuilderExtensions.m_numberFormatInfoHelper);
            return sb;
        }

        public static StringBuilder AppendInt64(this StringBuilder sb, long number)
        {
            StringBuilderExtensions.ConcatFormat<long>(sb, "{0}", number, (NumberFormatInfo) null);
            return sb;
        }

        public static StringBuilder AppendInt32(this StringBuilder sb, int number)
        {
            StringBuilderExtensions.ConcatFormat<int>(sb, "{0}", number, (NumberFormatInfo) null);
            return sb;
        }

        public static int GetDecimalCount(float number, int validDigitCount)
        {
            int num;
            for (num = validDigitCount; (double) number >= 1.0 && num > 0; --num)
                number /= 10f;
            return num;
        }

        public static int GetDecimalCount(double number, int validDigitCount)
        {
            int num;
            for (num = validDigitCount; number >= 1.0 && num > 0; --num)
                number /= 10.0;
            return num;
        }

        public static int GetDecimalCount(Decimal number, int validDigitCount)
        {
            int num;
            for (num = validDigitCount; number >= new Decimal(1) && num > 0; --num)
                number /= new Decimal(10);
            return num;
        }

        public static StringBuilder AppendDecimalDigit(this StringBuilder sb, float number, int validDigitCount)
        {
            return StringBuilderExtensions.AppendDecimal(sb, number,
                StringBuilderExtensions.GetDecimalCount(number, validDigitCount));
        }

        public static StringBuilder AppendDecimalDigit(this StringBuilder sb, double number, int validDigitCount)
        {
            return StringBuilderExtensions.AppendDecimal(sb, number,
                StringBuilderExtensions.GetDecimalCount(number, validDigitCount));
        }

        public static StringBuilder AppendDecimal(this StringBuilder sb, float number, int decimals)
        {
            StringBuilderExtensions.m_numberFormatInfoHelper.NumberDecimalDigits = Math.Max(0, Math.Min(decimals, 99));
            StringBuilderExtensions.ConcatFormat<float>(sb, "{0}", number,
                StringBuilderExtensions.m_numberFormatInfoHelper);
            return sb;
        }

        public static StringBuilder AppendDecimal(this StringBuilder sb, double number, int decimals)
        {
            StringBuilderExtensions.m_numberFormatInfoHelper.NumberDecimalDigits = Math.Max(0, Math.Min(decimals, 99));
            StringBuilderExtensions.ConcatFormat<double>(sb, "{0}", number,
                StringBuilderExtensions.m_numberFormatInfoHelper);
            return sb;
        }

        public static List<StringBuilder> Split(this StringBuilder sb, char separator)
        {
            List<StringBuilder> list = new List<StringBuilder>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < sb.Length; ++index)
            {
                if ((int) sb[index] == (int) separator)
                {
                    list.Add(stringBuilder);
                    stringBuilder = new StringBuilder();
                }
                else
                    stringBuilder.Append(sb[index]);
            }
            if (stringBuilder.Length > 0)
                list.Add(stringBuilder);
            return list;
        }

        public static StringBuilder TrimTrailingWhitespace(this StringBuilder sb)
        {
            int length = sb.Length;
            while (length > 0 && (int) sb[length - 1] == 32 ||
                   ((int) sb[length - 1] == 13 || (int) sb[length - 1] == 10))
                --length;
            sb.Length = length;
            return sb;
        }

        public static int CompareTo(this StringBuilder self, StringBuilder other)
        {
            int index = 0;
            int num;
            while (true)
            {
                bool flag1 = index < self.Length;
                bool flag2 = index < other.Length;
                if (flag1 || flag2)
                {
                    if (flag1)
                    {
                        if (flag2)
                        {
                            num = self[index].CompareTo(other[index]);
                            if (num == 0)
                                ++index;
                            else
                                goto label_8;
                        }
                        else
                            goto label_6;
                    }
                    else
                        goto label_4;
                }
                else
                    break;
            }
            return 0;
            label_4:
            return -1;
            label_6:
            return 1;
            label_8:
            return num;
        }

        public static int CompareTo(this StringBuilder self, string other)
        {
            int index = 0;
            int num;
            while (true)
            {
                bool flag1 = index < self.Length;
                bool flag2 = index < other.Length;
                if (flag1 || flag2)
                {
                    if (flag1)
                    {
                        if (flag2)
                        {
                            num = self[index].CompareTo(other[index]);
                            if (num == 0)
                                ++index;
                            else
                                goto label_8;
                        }
                        else
                            goto label_6;
                    }
                    else
                        goto label_4;
                }
                else
                    break;
            }
            return 0;
            label_4:
            return -1;
            label_6:
            return 1;
            label_8:
            return num;
        }

        public static int CompareToIgnoreCase(this StringBuilder self, StringBuilder other)
        {
            int index = 0;
            int num;
            while (true)
            {
                bool flag1 = index < self.Length;
                bool flag2 = index < other.Length;
                if (flag1 || flag2)
                {
                    if (flag1)
                    {
                        if (flag2)
                        {
                            num = char.ToLowerInvariant(self[index]).CompareTo(char.ToLowerInvariant(other[index]));
                            if (num == 0)
                                ++index;
                            else
                                goto label_8;
                        }
                        else
                            goto label_6;
                    }
                    else
                        goto label_4;
                }
                else
                    break;
            }
            return 0;
            label_4:
            return -1;
            label_6:
            return 1;
            label_8:
            return num;
        }
    }
}