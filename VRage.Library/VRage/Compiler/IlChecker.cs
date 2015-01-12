// Decompiled with JetBrains decompiler
// Type: VRage.Compiler.IlChecker
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Xml.Serialization;

namespace VRage.Compiler
{
    public class IlChecker
    {
        public static Dictionary<Type, List<MemberInfo>> AllowedOperands = new Dictionary<Type, List<MemberInfo>>();

        public static Dictionary<Assembly, List<string>> AllowedNamespacesCommon =
            new Dictionary<Assembly, List<string>>();

        public static Dictionary<Assembly, List<string>> AllowedNamespacesModAPI =
            new Dictionary<Assembly, List<string>>();

        static IlChecker()
        {
            IlChecker.AllowedOperands.Add(typeof (object), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (IDisposable), (List<MemberInfo>) null);
            IlChecker.AllowNamespaceOfTypeCommon(typeof (IEnumerator));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (IEnumerable<>));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (HashSet<>));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (Queue<>));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (ListExtensions));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (Enumerable));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (StringBuilder));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (Regex));
            IlChecker.AllowNamespaceOfTypeModAPI(typeof (Timer));
            IlChecker.AllowNamespaceOfTypeCommon(typeof (Calendar));
            IlChecker.AllowedOperands.Add(typeof (StringBuilder), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (string), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Math), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Enum), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (int), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (short), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (long), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (uint), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (ushort), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (ulong), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (double), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (float), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (bool), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (char), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (byte), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (sbyte), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Decimal), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (DateTime), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (TimeSpan), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Array), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlElementAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAttributeAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlArrayAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlArrayItemAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAnyAttributeAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAnyElementAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAnyElementAttributes), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlArrayItemAttributes), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAttributeEventArgs), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAttributeOverrides), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlAttributes), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlChoiceIdentifierAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlElementAttributes), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlElementEventArgs), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlEnumAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlIgnoreAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlIncludeAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlRootAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlTextAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (XmlTypeAttribute), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (MemberInfo), new List<MemberInfo>()
            {
                (MemberInfo) typeof (MemberInfo).GetProperty("Name").GetGetMethod()
            });
            IlChecker.AllowedOperands.Add(typeof (RuntimeHelpers), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Stream), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (TextWriter), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (TextReader), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (BinaryReader), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (BinaryWriter), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (Type), new List<MemberInfo>()
            {
                (MemberInfo) typeof (Type).GetMethod("GetTypeFromHandle")
            });
            Type type1 = typeof (Type).Assembly.GetType("System.RuntimeType");
            IlChecker.AllowedOperands[type1] = new List<MemberInfo>()
            {
                (MemberInfo) type1.GetMethod("op_Inequality"),
                (MemberInfo) type1.GetMethod("GetFields", new Type[1]
                {
                    typeof (BindingFlags)
                })
            };
            IlChecker.AllowedOperands[typeof (Type)] = new List<MemberInfo>()
            {
                (MemberInfo) typeof (Type).GetMethod("GetFields", new Type[1]
                {
                    typeof (BindingFlags)
                }),
                (MemberInfo) typeof (Type).GetMethod("IsEquivalentTo"),
                (MemberInfo) typeof (Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public),
                (MemberInfo) typeof (Type).GetMethod("op_Equality")
            };
            Type type2 = typeof (Type).Assembly.GetType("System.Reflection.RtFieldInfo");
            IlChecker.AllowedOperands[type2] = new List<MemberInfo>()
            {
                (MemberInfo) type2.GetMethod("UnsafeGetValue", BindingFlags.Instance | BindingFlags.NonPublic)
            };
            IlChecker.AllowedOperands[typeof (NullReferenceException)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (ArgumentException)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (ArgumentNullException)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (InvalidOperationException)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (FormatException)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands.Add(typeof (Exception), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (DivideByZeroException), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (InvalidCastException), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (FileNotFoundException), (List<MemberInfo>) null);
            typeof (MethodInfo).Assembly.GetType("System.Reflection.RuntimeMethodInfo");
            IlChecker.AllowedOperands[typeof (ValueType)] = new List<MemberInfo>()
            {
                (MemberInfo) typeof (ValueType).GetMethod("Equals"),
                (MemberInfo) typeof (ValueType).GetMethod("GetHashCode"),
                (MemberInfo) typeof (ValueType).GetMethod("ToString"),
                (MemberInfo)
                    typeof (ValueType).GetMethod("CanCompareBits", BindingFlags.Static | BindingFlags.NonPublic),
                (MemberInfo)
                    typeof (ValueType).GetMethod("FastEqualsCheck", BindingFlags.Static | BindingFlags.NonPublic)
            };
            Type index = typeof (Environment);
            IlChecker.AllowedOperands[index] = new List<MemberInfo>()
            {
                (MemberInfo)
                    index.GetMethod("GetResourceString", BindingFlags.Static | BindingFlags.NonPublic, (Binder) null,
                        new Type[2]
                        {
                            typeof (string),
                            typeof (object[])
                        }, (ParameterModifier[]) null),
                (MemberInfo)
                    index.GetMethod("GetResourceString", BindingFlags.Static | BindingFlags.NonPublic, (Binder) null,
                        new Type[1]
                        {
                            typeof (string)
                        }, (ParameterModifier[]) null)
            };
            IlChecker.AllowedOperands[typeof (Path)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (Random)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands[typeof (Convert)] = (List<MemberInfo>) null;
            IlChecker.AllowedOperands.Add(typeof (Nullable<>), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (StringComparer), (List<MemberInfo>) null);
            IlChecker.AllowedOperands.Add(typeof (IComparable<>), (List<MemberInfo>) null);
        }

        public static void AllowNamespaceOfTypeModAPI(Type type)
        {
            if (!IlChecker.AllowedNamespacesModAPI.ContainsKey(type.Assembly))
                IlChecker.AllowedNamespacesModAPI.Add(type.Assembly, new List<string>());
            IlChecker.AllowedNamespacesModAPI[type.Assembly].Add(type.Namespace);
        }

        public static void AllowNamespaceOfTypeCommon(Type type)
        {
            if (!IlChecker.AllowedNamespacesCommon.ContainsKey(type.Assembly))
                IlChecker.AllowedNamespacesCommon.Add(type.Assembly, new List<string>());
            IlChecker.AllowedNamespacesCommon[type.Assembly].Add(type.Namespace);
        }

        public static bool CheckIl(List<IlReader.IlInstruction> instructions, out Type failed, bool isIngameScript,
            Dictionary<Type, List<MemberInfo>> allowedTypes = null)
        {
            failed = (Type) null;
            foreach (KeyValuePair<Type, List<MemberInfo>> keyValuePair in allowedTypes)
            {
                if (
                    !Enumerable.Contains<KeyValuePair<Type, List<MemberInfo>>>(
                        (IEnumerable<KeyValuePair<Type, List<MemberInfo>>>) IlChecker.AllowedOperands, keyValuePair))
                    IlChecker.AllowedOperands.Add(keyValuePair.Key, keyValuePair.Value);
            }
            foreach (IlReader.IlInstruction ilInstruction in instructions)
            {
                MethodInfo methodInfo = ilInstruction.Operand as MethodInfo;
                if (methodInfo != (MethodInfo) null && IlChecker.HasMethodInvalidAtrributes(methodInfo.Attributes))
                    return false;
                if (!IlChecker.CheckMember(ilInstruction.Operand as MemberInfo, isIngameScript) ||
                    ilInstruction.OpCode == OpCodes.Calli)
                {
                    failed = ((MemberInfo) ilInstruction.Operand).DeclaringType;
                    return false;
                }
            }
            return true;
        }

        private static bool CheckMember(MemberInfo memberInfo, bool isIngameScript)
        {
            if (memberInfo == (MemberInfo) null)
                return true;
            else
                return IlChecker.CheckTypeAndMember(memberInfo.DeclaringType, isIngameScript, memberInfo);
        }

        public static bool CheckTypeAndMember(Type type, bool isIngameScript, MemberInfo memberInfo = null)
        {
            return type == (Type) null || IlChecker.IsDelegate(type) ||
                   !type.IsGenericTypeDefinition && type.IsGenericType &&
                   IlChecker.CheckGenericType(type.GetGenericTypeDefinition(), memberInfo, isIngameScript) ||
                   (IlChecker.CheckNamespace(type, isIngameScript) ||
                    IlChecker.CheckOperand(type, memberInfo, IlChecker.AllowedOperands));
        }

        private static bool IsDelegate(Type type)
        {
            Type type1 = typeof (MulticastDelegate);
            if (!type1.IsAssignableFrom(type.BaseType) && !(type == type1))
                return type == type1.BaseType;
            else
                return true;
        }

        private static bool CheckNamespace(Type type, bool isIngameScript)
        {
            if (type == (Type) null)
                return false;
            bool flag = IlChecker.AllowedNamespacesCommon.ContainsKey(type.Assembly) &&
                        IlChecker.AllowedNamespacesCommon[type.Assembly].Contains(type.Namespace);
            if (!flag && !isIngameScript)
                flag = IlChecker.AllowedNamespacesModAPI.ContainsKey(type.Assembly) &&
                       IlChecker.AllowedNamespacesModAPI[type.Assembly].Contains(type.Namespace);
            return flag;
        }

        private static bool CheckOperand(Type type, MemberInfo memberInfo, Dictionary<Type, List<MemberInfo>> op)
        {
            if (op == null || !op.ContainsKey(type))
                return false;
            if (!(memberInfo == (MemberInfo) null) && op[type] != null)
                return op[type].Contains(memberInfo);
            else
                return true;
        }

        private static bool CheckGenericType(Type declType, MemberInfo memberInfo, bool isIngameScript)
        {
            if (!IlChecker.CheckTypeAndMember(declType, isIngameScript, memberInfo))
                return false;
            if (memberInfo != (MemberInfo) null)
            {
                foreach (Type type in memberInfo.DeclaringType.GetGenericArguments())
                {
                    if (!type.IsGenericParameter &&
                        !IlChecker.CheckTypeAndMember(type, isIngameScript, (MemberInfo) null))
                        return false;
                }
            }
            return true;
        }

        public static bool HasMethodInvalidAtrributes(MethodAttributes Attributes)
        {
            return (Attributes & (MethodAttributes.PinvokeImpl | MethodAttributes.UnmanagedExport)) !=
                   MethodAttributes.PrivateScope;
        }

        public static bool IsMethodFromParent(Type classType, MethodBase method)
        {
            return classType.IsSubclassOf(method.DeclaringType);
        }
    }
}