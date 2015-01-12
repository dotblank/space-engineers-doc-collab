// Decompiled with JetBrains decompiler
// Type: VRage.Native.NativeCallHelper`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace VRage.Native
{
    public class NativeCallHelper<TDelegate> where TDelegate : class
    {
        public static readonly TDelegate Invoke = NativeCallHelper<TDelegate>.Create();

        private static TDelegate Create()
        {
            MethodInfo method = typeof (TDelegate).GetMethod("Invoke");
            Type[] parameterTypes1 =
                Enumerable.ToArray<Type>(
                    Enumerable.Select<ParameterInfo, Type>((IEnumerable<ParameterInfo>) method.GetParameters(),
                        (Func<ParameterInfo, Type>) (s => s.ParameterType)));
            if (parameterTypes1.Length == 0 || parameterTypes1[0] != typeof (IntPtr))
                throw new InvalidOperationException("First parameter must be function pointer");
            Type[] parameterTypes2 =
                Enumerable.ToArray<Type>(
                    Enumerable.Select<Type, Type>(Enumerable.Skip<Type>((IEnumerable<Type>) parameterTypes1, 1),
                        (Func<Type, Type>) (s =>
                        {
                            if (!(s == typeof (IntPtr)))
                                return s;
                            else
                                return typeof (void*);
                        })));
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, method.ReturnType, parameterTypes1,
                Assembly.GetExecutingAssembly().ManifestModule);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            for (int index = 1; index < parameterTypes1.Length; ++index)
                ilGenerator.Emit(OpCodes.Ldarg, index);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, method.ReturnType, parameterTypes2);
            ilGenerator.Emit(OpCodes.Ret);
            return MethodInfoExtensions.CreateDelegate<TDelegate>((MethodInfo) dynamicMethod);
        }
    }
}