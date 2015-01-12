// Decompiled with JetBrains decompiler
// Type: VRage.EnumComparer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VRage
{
    public sealed class EnumComparer<TEnum> : IEqualityComparer<TEnum>
        where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        private static readonly Func<TEnum, int> getHashCodeFunct = EnumComparer<TEnum>.GenerateGetHashCodeFunct();
        private static readonly Func<TEnum, TEnum, bool> equalsFunct = EnumComparer<TEnum>.GenerateEqualsFunct();
        private static readonly EnumComparer<TEnum> instance = new EnumComparer<TEnum>();

        public static EnumComparer<TEnum> Instance
        {
            get { return EnumComparer<TEnum>.instance; }
        }

        private EnumComparer()
        {
            EnumComparer<TEnum>.AssertTypeIsEnum();
            EnumComparer<TEnum>.AssertUnderlyingTypeIsSupported();
        }

        public bool Equals(TEnum x, TEnum y)
        {
            return EnumComparer<TEnum>.equalsFunct(x, y);
        }

        public int GetHashCode(TEnum obj)
        {
            return EnumComparer<TEnum>.getHashCodeFunct(obj);
        }

        private static void AssertTypeIsEnum()
        {
            if (!typeof (TEnum).IsEnum)
                throw new NotSupportedException(
                    string.Format("The type parameter {0} is not an Enum. LcgEnumComparer supports Enums only.",
                        (object) typeof (TEnum)));
        }

        private static void AssertUnderlyingTypeIsSupported()
        {
            Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
            if (!((ICollection<Type>) new Type[8]
            {
                typeof (byte),
                typeof (sbyte),
                typeof (short),
                typeof (ushort),
                typeof (int),
                typeof (uint),
                typeof (long),
                typeof (ulong)
            }).Contains(underlyingType))
                throw new NotSupportedException(
                    string.Format(
                        "The underlying type of the type parameter {0} is {1}. LcgEnumComparer only supports Enums with underlying type of byte, sbyte, short, ushort, int, uint, long, or ulong.",
                        (object) typeof (TEnum), (object) underlyingType));
        }

        private static Func<TEnum, TEnum, bool> GenerateEqualsFunct()
        {
            return ((Expression<Func<TEnum, TEnum, bool>>) ((x, y) => x.Equals(y))).Compile();
        }

        private static Func<TEnum, int> GenerateGetHashCodeFunct()
        {
            // removed because trying to fix the errors is pointless
            return default(Func<TEnum, int>);
        }
    }
}