// Decompiled with JetBrains decompiler
// Type: VRage.MyEnum`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VRage.Compiler;

namespace VRage
{
    public static class MyEnum<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public static readonly T[] Values = (T[]) Enum.GetValues(typeof (T));
        public static readonly Type UnderlyingType = typeof (T).UnderlyingSystemType;
        private static readonly Dictionary<int, string> m_names = new Dictionary<int, string>();

        public static string Name
        {
            get { return TypeNameHelper<T>.Name; }
        }

        private static T FindMaxValue()
        {
            T[] objArray = MyEnum<T>.Values;
            Comparer<T> @default = Comparer<T>.Default;
            if (objArray.Length <= 0)
                return default (T);
            T x = objArray[0];
            for (int index = 1; index < objArray.Length; ++index)
            {
                if (@default.Compare(x, objArray[index]) < 0)
                    x = objArray[index];
            }
            return x;
        }

        public static string GetName(T value)
        {
            int key = Array.IndexOf<T>(MyEnum<T>.Values, value);
            string str;
            if (!MyEnum<T>.m_names.TryGetValue(key, out str))
            {
                str = value.ToString();
                MyEnum<T>.m_names[key] = str;
            }
            return str;
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct MaxValue
        {
            public static readonly T Value = MyEnum<T>.FindMaxValue();
        }
    }
}