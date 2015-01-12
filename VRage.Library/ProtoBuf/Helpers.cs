// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Helpers
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ProtoBuf
{
    internal class Helpers
    {
        public static readonly Type[] EmptyTypes = new Type[0];

        private Helpers()
        {
        }

        public static StringBuilder AppendLine(StringBuilder builder)
        {
            return builder.AppendLine();
        }

        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }

        [Conditional("DEBUG")]
        public static void DebugWriteLine(string message, object obj)
        {
            try
            {
                if (obj == null)
                    return;
                obj.ToString();
            }
            catch
            {
            }
        }

        [Conditional("DEBUG")]
        public static void DebugWriteLine(string message)
        {
        }

        [Conditional("TRACE")]
        public static void TraceWriteLine(string message)
        {
            Trace.WriteLine(message);
        }

        [Conditional("DEBUG")]
        public static void DebugAssert(bool condition, string message)
        {
            int num = condition ? 1 : 0;
        }

        [Conditional("DEBUG")]
        public static void DebugAssert(bool condition, string message, params object[] args)
        {
            int num = condition ? 1 : 0;
        }

        [Conditional("DEBUG")]
        public static void DebugAssert(bool condition)
        {
            if (condition || !Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        public static void Sort(int[] keys, object[] values)
        {
            bool flag;
            do
            {
                flag = false;
                for (int index = 1; index < keys.Length; ++index)
                {
                    if (keys[index - 1] > keys[index])
                    {
                        int num = keys[index];
                        keys[index] = keys[index - 1];
                        keys[index - 1] = num;
                        object obj = values[index];
                        values[index] = values[index - 1];
                        values[index - 1] = obj;
                        flag = true;
                    }
                }
            } while (flag);
        }

        public static void BlockCopy(byte[] from, int fromIndex, byte[] to, int toIndex, int count)
        {
            Buffer.BlockCopy((Array) from, fromIndex, (Array) to, toIndex, count);
        }

        public static bool IsInfinity(float value)
        {
            return float.IsInfinity(value);
        }

        internal static MethodInfo GetInstanceMethod(Type declaringType, string name)
        {
            return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        internal static MethodInfo GetStaticMethod(Type declaringType, string name)
        {
            return declaringType.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }

        internal static MethodInfo GetInstanceMethod(Type declaringType, string name, Type[] types)
        {
            if (types == null)
                types = Helpers.EmptyTypes;
            return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                (Binder) null, types, (ParameterModifier[]) null);
        }

        internal static bool IsSubclassOf(Type type, Type baseClass)
        {
            return type.IsSubclassOf(baseClass);
        }

        public static bool IsInfinity(double value)
        {
            return double.IsInfinity(value);
        }

        public static ProtoTypeCode GetTypeCode(Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Empty:
                case TypeCode.Boolean:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.DateTime:
                case TypeCode.String:
                    return (ProtoTypeCode) typeCode;
                default:
                    if (type == typeof (TimeSpan))
                        return ProtoTypeCode.TimeSpan;
                    if (type == typeof (Guid))
                        return ProtoTypeCode.Guid;
                    if (type == typeof (Uri))
                        return ProtoTypeCode.Uri;
                    if (type == typeof (byte[]))
                        return ProtoTypeCode.ByteArray;
                    return type == typeof (Type) ? ProtoTypeCode.Type : ProtoTypeCode.Unknown;
            }
        }

        internal static Type GetUnderlyingType(Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        internal static bool IsValueType(Type type)
        {
            return type.IsValueType;
        }

        internal static bool IsEnum(Type type)
        {
            return type.IsEnum;
        }

        internal static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
        {
            if (property == (PropertyInfo) null)
                return (MethodInfo) null;
            MethodInfo methodInfo = property.GetGetMethod(nonPublic);
            if (methodInfo == (MethodInfo) null && !nonPublic && allowInternal)
            {
                methodInfo = property.GetGetMethod(true);
                if (methodInfo == (MethodInfo) null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
                    methodInfo = (MethodInfo) null;
            }
            return methodInfo;
        }

        internal static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
        {
            if (property == (PropertyInfo) null)
                return (MethodInfo) null;
            MethodInfo methodInfo = property.GetSetMethod(nonPublic);
            if (methodInfo == (MethodInfo) null && !nonPublic && allowInternal)
            {
                methodInfo = property.GetGetMethod(true);
                if (methodInfo == (MethodInfo) null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
                    methodInfo = (MethodInfo) null;
            }
            return methodInfo;
        }

        internal static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes, bool nonPublic)
        {
            return
                type.GetConstructor(
                    nonPublic
                        ? BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                        : BindingFlags.Instance | BindingFlags.Public, (Binder) null, parameterTypes,
                    (ParameterModifier[]) null);
        }

        internal static ConstructorInfo[] GetConstructors(Type type, bool nonPublic)
        {
            return
                type.GetConstructors(nonPublic
                    ? BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                    : BindingFlags.Instance | BindingFlags.Public);
        }

        internal static PropertyInfo GetProperty(Type type, string name, bool nonPublic)
        {
            return type.GetProperty(name,
                nonPublic
                    ? BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                    : BindingFlags.Instance | BindingFlags.Public);
        }

        internal static object ParseEnum(Type type, string value)
        {
            return Enum.Parse(type, value, true);
        }

        internal static MemberInfo[] GetInstanceFieldsAndProperties(Type type, bool publicOnly)
        {
            BindingFlags bindingAttr = publicOnly
                ? BindingFlags.Instance | BindingFlags.Public
                : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo[] properties = type.GetProperties(bindingAttr);
            FieldInfo[] fields = type.GetFields(bindingAttr);
            MemberInfo[] memberInfoArray = new MemberInfo[fields.Length + properties.Length];
            properties.CopyTo((Array) memberInfoArray, 0);
            fields.CopyTo((Array) memberInfoArray, properties.Length);
            return memberInfoArray;
        }

        internal static Type GetMemberType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo) member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo) member).PropertyType;
                default:
                    return (Type) null;
            }
        }

        internal static bool IsAssignableFrom(Type target, Type type)
        {
            return target.IsAssignableFrom(type);
        }
    }
}