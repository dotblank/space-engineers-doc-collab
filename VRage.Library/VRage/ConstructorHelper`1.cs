// Decompiled with JetBrains decompiler
// Type: VRage.ConstructorHelper`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace VRage
{
    public static class ConstructorHelper<T>
    {
        public static Delegate CreateInPlaceConstructor(Type constructorType)
        {
            Type type = typeof (T);
            Type[] parameterTypes =
                Array.ConvertAll<ParameterInfo, Type>(constructorType.GetMethod("Invoke").GetParameters(),
                    (Converter<ParameterInfo, Type>) (p => p.ParameterType));
            Type[] types;
            if (parameterTypes.Length > 1)
            {
                types = new Type[parameterTypes.Length - 1];
                Array.ConstrainedCopy((Array) parameterTypes, 1, (Array) types, 0, parameterTypes.Length - 1);
            }
            else
                types = Type.EmptyTypes;
            ConstructorInfo constructor = type.GetConstructor(types);
            if (constructor == (ConstructorInfo) null)
                throw new InvalidOperationException(string.Format("No matching constructor for object {0} was found!",
                    (object) type.Name));
            DynamicMethod dynamicMethod =
                new DynamicMethod(string.Format("Pool<T>__{0}", (object) Guid.NewGuid().ToString().Replace("-", "")),
                    typeof (void), parameterTypes, typeof (ConstructorHelper<T>), false);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            for (int index = 0; index < parameterTypes.Length; ++index)
            {
                if (index < 4)
                {
                    switch (index)
                    {
                        case 0:
                            ilGenerator.Emit(OpCodes.Ldarg_0);
                            continue;
                        case 1:
                            ilGenerator.Emit(OpCodes.Ldarg_1);
                            continue;
                        case 2:
                            ilGenerator.Emit(OpCodes.Ldarg_2);
                            continue;
                        case 3:
                            ilGenerator.Emit(OpCodes.Ldarg_3);
                            continue;
                        default:
                            continue;
                    }
                }
                else
                    ilGenerator.Emit(OpCodes.Ldarg_S, index);
            }
            ilGenerator.Emit(OpCodes.Callvirt, constructor);
            ilGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(constructorType);
        }
    }
}