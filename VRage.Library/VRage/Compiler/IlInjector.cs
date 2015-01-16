// Decompiled with JetBrains decompiler
// Type: VRage.Compiler.IlInjector
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace VRage.Compiler
{
    public class IlInjector
    {
        private static int m_numInstructions = 0;
        private static int m_numMaxInstructions = 0;
        private static IlReader m_reader = new IlReader();

        public static void RestartCountingInstructions(int maxInstructions)
        {
            IlInjector.m_numInstructions = 0;
            IlInjector.m_numMaxInstructions = maxInstructions;
        }

        public static void CountInstructions()
        {
            ++IlInjector.m_numInstructions;
            if (IlInjector.m_numInstructions > IlInjector.m_numMaxInstructions)
                throw new ScriptOutOfRangeException();
        }

        public static Assembly InjectCodeToAssembly(string newAssemblyName, Assembly inputAssembly, MethodInfo method)
        {
            bool flag = false;
            AssemblyName name = new AssemblyName(newAssemblyName);
            AssemblyBuilder assemblyBuilder = Thread.GetDomain()
                .DefineDynamicAssembly(name, flag ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
            ModuleBuilder newModule = assemblyBuilder.DefineDynamicModule(name.Name);
            IlInjector.InjectTypes(inputAssembly.GetTypes(), newModule, method);
            if (flag)
                assemblyBuilder.Save(name.Name + ".dll");
            return (Assembly) assemblyBuilder;
        }

        private static void InjectTypes(Type[] sourceTypes, ModuleBuilder newModule, MethodInfo methodToInject)
        {
            Dictionary<TypeBuilder, Type> dictionary1 = new Dictionary<TypeBuilder, Type>();
            Dictionary<MethodBuilder, MethodInfo> dictionary2 = new Dictionary<MethodBuilder, MethodInfo>();
            Dictionary<ConstructorBuilder, ConstructorInfo> dictionary3 =
                new Dictionary<ConstructorBuilder, ConstructorInfo>();
            List<FieldBuilder> list = new List<FieldBuilder>();
            foreach (Type type1 in sourceTypes)
            {
                TypeBuilder type2 = IlInjector.CreateType(newModule, dictionary1, type1);
                IlInjector.CopyFields(list, type1, type2);
                IlInjector.CopyProperties(type1, type2);
                IlInjector.CopyConstructors(dictionary3, type1, type2);
                IlInjector.CopyMethods(dictionary2, type1, type2);
            }
            foreach (KeyValuePair<TypeBuilder, Type> keyValuePair1 in dictionary1)
            {
                foreach (KeyValuePair<MethodBuilder, MethodInfo> keyValuePair2 in dictionary2)
                {
                    if (keyValuePair2.Key.DeclaringType == (Type) keyValuePair1.Key)
                        IlInjector.InjectMethod((MethodBase) keyValuePair2.Value, keyValuePair2.Key.GetILGenerator(),
                            list, dictionary2, dictionary3, dictionary1, methodToInject);
                }
                foreach (KeyValuePair<ConstructorBuilder, ConstructorInfo> keyValuePair2 in dictionary3)
                {
                    if (keyValuePair2.Key.DeclaringType == (Type) keyValuePair1.Key)
                        IlInjector.InjectMethod((MethodBase) keyValuePair2.Value, keyValuePair2.Key.GetILGenerator(),
                            list, dictionary2, dictionary3, dictionary1, methodToInject);
                }
                keyValuePair1.Key.CreateType();
            }
        }

        private static TypeBuilder CreateType(ModuleBuilder newModule, Dictionary<TypeBuilder, Type> createdTypes,
            Type sourceType)
        {
            TypeAttributes attr = sourceType.Attributes;
            if ((attr & TypeAttributes.NestedPublic) == TypeAttributes.NestedPublic)
                attr = attr & ~TypeAttributes.NestedPublic | TypeAttributes.Public;
            if ((attr & TypeAttributes.NestedPrivate) == TypeAttributes.NestedPrivate)
                attr &= ~TypeAttributes.NestedPrivate;
            TypeBuilder key = newModule.DefineType(sourceType.Name, attr, sourceType.BaseType,
                sourceType.GetInterfaces());
            createdTypes.Add(key, sourceType);
            return key;
        }

        private static void CopyFields(List<FieldBuilder> createdFields, Type sourceType, TypeBuilder newType)
        {
            foreach (
                FieldInfo fieldInfo in
                    sourceType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                         BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField))
                createdFields.Add(newType.DefineField(fieldInfo.Name, fieldInfo.FieldType, fieldInfo.Attributes));
        }

        private static void CopyProperties(Type sourceType, TypeBuilder newType)
        {
            foreach (
                PropertyInfo propertyInfo in
                    sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                             BindingFlags.GetProperty | BindingFlags.SetProperty))
                newType.DefineProperty(propertyInfo.Name, PropertyAttributes.HasDefault, propertyInfo.PropertyType,
                    Type.EmptyTypes);
        }

        private static void CopyMethods(Dictionary<MethodBuilder, MethodInfo> createdMethods, Type type,
            TypeBuilder newType)
        {
            foreach (
                MethodInfo methodInfo in
                    type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                    BindingFlags.NonPublic))
            {
                if (!(methodInfo.DeclaringType != type))
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    Type[] parameterTypes = new Type[parameters.Length];
                    int num = 0;
                    foreach (ParameterInfo parameterInfo in parameters)
                        parameterTypes[num++] = parameterInfo.ParameterType;
                    createdMethods.Add(
                        newType.DefineMethod(methodInfo.Name, methodInfo.Attributes, methodInfo.CallingConvention,
                            methodInfo.ReturnType, parameterTypes), methodInfo);
                }
            }
        }

        private static void CopyConstructors(Dictionary<ConstructorBuilder, ConstructorInfo> createdConstructors,
            Type type, TypeBuilder newType)
        {
            foreach (
                ConstructorInfo constructorInfo in
                    type.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                         BindingFlags.NonPublic))
            {
                if (!(constructorInfo.DeclaringType != type))
                {
                    ParameterInfo[] parameters = constructorInfo.GetParameters();
                    Type[] parameterTypes = new Type[parameters.Length];
                    int num = 0;
                    foreach (ParameterInfo parameterInfo in parameters)
                        parameterTypes[num++] = parameterInfo.ParameterType;
                    createdConstructors.Add(
                        newType.DefineConstructor(constructorInfo.Attributes, constructorInfo.CallingConvention,
                            parameterTypes), constructorInfo);
                }
            }
        }

        private static void InjectMethod(MethodBase sourceMethod, ILGenerator methodGenerator, List<FieldBuilder> fields,
            Dictionary<MethodBuilder, MethodInfo> methods, Dictionary<ConstructorBuilder, ConstructorInfo> constructors,
            Dictionary<TypeBuilder, Type> types, MethodInfo methodToInject)
        {
            IlInjector.ConstructInstructions(sourceMethod, methodGenerator, fields, methods, constructors, types,
                methodToInject);
        }

        private static void ConstructInstructions(MethodBase sourceMethod, ILGenerator methodGenerator,
            List<FieldBuilder> createdFields, Dictionary<MethodBuilder, MethodInfo> createdMethods,
            Dictionary<ConstructorBuilder, ConstructorInfo> createdConstructors,
            Dictionary<TypeBuilder, Type> createdTypes, MethodInfo methodToInject)
        {
            List<IlReader.IlInstruction> list = IlInjector.m_reader.ReadInstructions(sourceMethod);
            IlInjector.ResolveTypes(methodGenerator, createdTypes);
            Dictionary<long, Label> dictionary = new Dictionary<long, Label>();
            foreach (IlReader.IlInstruction ilInstruction in list)
                dictionary[ilInstruction.Offset] = methodGenerator.DefineLabel();
            methodGenerator.Emit(OpCodes.Call, methodToInject);
            foreach (IlReader.IlInstruction instruction in list)
            {
                methodGenerator.MarkLabel(dictionary[instruction.Offset]);
                OpCode opCode = instruction.OpCode;
                if (opCode.FlowControl == FlowControl.Branch || opCode.FlowControl == FlowControl.Cond_Branch)
                {
                    opCode = IlInjector.SwitchShortOpCodes(opCode);
                    methodGenerator.Emit(OpCodes.Call, methodToInject);
                    methodGenerator.Emit(opCode, dictionary[(long) Convert.ToInt32(instruction.Operand)]);
                }
                else
                {
                    switch (instruction.OpCode.OperandType)
                    {
                        case OperandType.InlineBrTarget:
                        case OperandType.InlineI:
                        case OperandType.InlineSig:
                            methodGenerator.Emit(opCode, Convert.ToInt32(instruction.Operand));
                            continue;
                        case OperandType.InlineField:
                            IlInjector.ResolveField(instruction.Operand as FieldInfo, createdFields, methodGenerator,
                                opCode);
                            continue;
                        case OperandType.InlineI8:
                            methodGenerator.Emit(opCode, Convert.ToInt64(instruction.Operand));
                            continue;
                        case OperandType.InlineMethod:
                            try
                            {
                                IlInjector.ResolveMethod(methodGenerator, createdMethods, createdConstructors,
                                    instruction, opCode);
                                continue;
                            }
                            catch
                            {
                                IlInjector.ResolveField(instruction.Operand as FieldInfo, createdFields, methodGenerator,
                                    opCode);
                                continue;
                            }
                        case OperandType.InlineNone:
                            methodGenerator.Emit(opCode);
                            continue;
                        case OperandType.InlineR:
                            methodGenerator.Emit(opCode, Convert.ToDouble(instruction.Operand));
                            continue;
                        case OperandType.InlineString:
                            methodGenerator.Emit(opCode, instruction.Operand as string);
                            continue;
                        case OperandType.InlineSwitch:
                            continue;
                        case OperandType.InlineTok:
                        case OperandType.InlineType:
                            try
                            {
                                methodGenerator.Emit(opCode, instruction.Operand as Type);
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        case OperandType.InlineVar:
                            methodGenerator.Emit(opCode, (int) Convert.ToUInt16(instruction.LocalVariableIndex));
                            continue;
                        case OperandType.ShortInlineBrTarget:
                        case OperandType.ShortInlineI:
                            methodGenerator.Emit(opCode, Convert.ToSByte(instruction.Operand));
                            continue;
                        case OperandType.ShortInlineR:
                            methodGenerator.Emit(opCode, Convert.ToSingle(instruction.Operand));
                            continue;
                        case OperandType.ShortInlineVar:
                            methodGenerator.Emit(opCode, Convert.ToByte(instruction.LocalVariableIndex));
                            continue;
                        default:
                            throw new Exception("Unknown operand type.");
                    }
                }
            }
        }

        private static OpCode SwitchShortOpCodes(OpCode code)
        {
            if (code == OpCodes.Bge_Un_S)
                code = OpCodes.Bge_Un;
            if (code == OpCodes.Bne_Un_S)
                code = OpCodes.Bne_Un;
            if (code == OpCodes.Ble_Un_S)
                code = OpCodes.Ble_Un;
            if (code == OpCodes.Ble_S)
                code = OpCodes.Ble;
            if (code == OpCodes.Blt_S)
                code = OpCodes.Blt;
            if (code == OpCodes.Blt_Un_S)
                code = OpCodes.Blt_Un;
            if (code == OpCodes.Beq_S)
                code = OpCodes.Beq;
            if (code == OpCodes.Br_S)
                code = OpCodes.Br;
            if (code == OpCodes.Brtrue_S)
                code = OpCodes.Brtrue;
            if (code == OpCodes.Brfalse_S)
                code = OpCodes.Brfalse;
            if (code == OpCodes.Leave_S)
                code = OpCodes.Leave;
            if (code == OpCodes.Bge_S)
                code = OpCodes.Bge;
            return code;
        }

        private static void ResolveMethod(ILGenerator generator, Dictionary<MethodBuilder, MethodInfo> methods,
            Dictionary<ConstructorBuilder, ConstructorInfo> constructors, IlReader.IlInstruction instruction,
            OpCode code)
        {
            bool flag = false;
            MethodBase methodBase = instruction.Operand as MethodBase;
            if (instruction.Operand is MethodInfo)
            {
                MethodInfo methodInfo = instruction.Operand as MethodInfo;
                foreach (KeyValuePair<MethodBuilder, MethodInfo> keyValuePair in methods)
                {
                    if (keyValuePair.Value == methodInfo)
                    {
                        generator.Emit(code, (MethodInfo) keyValuePair.Key);
                        flag = true;
                        break;
                    }
                }
            }
            if (instruction.Operand is ConstructorInfo)
            {
                ConstructorInfo constructorInfo = instruction.Operand as ConstructorInfo;
                foreach (KeyValuePair<ConstructorBuilder, ConstructorInfo> keyValuePair in constructors)
                {
                    if (keyValuePair.Value == constructorInfo)
                    {
                        generator.Emit(code, (ConstructorInfo) keyValuePair.Key);
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
                return;
            if (methodBase is MethodInfo)
            {
                generator.Emit(code, methodBase as MethodInfo);
            }
            else
            {
                if (!(methodBase is ConstructorInfo))
                    return;
                generator.Emit(code, methodBase as ConstructorInfo);
            }
        }

        private static void ResolveTypes(ILGenerator generator, Dictionary<TypeBuilder, Type> types)
        {
            foreach (LocalVariableInfo localVariableInfo in (IEnumerable<LocalVariableInfo>) IlInjector.m_reader.Locals)
            {
                bool flag = false;
                foreach (KeyValuePair<TypeBuilder, Type> keyValuePair in types)
                {
                    if (keyValuePair.Value == localVariableInfo.LocalType)
                    {
                        generator.DeclareLocal((Type) keyValuePair.Key);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    generator.DeclareLocal(localVariableInfo.LocalType);
            }
        }

        private static void ResolveField(FieldInfo field, List<FieldBuilder> fields, ILGenerator generator, OpCode code)
        {
            foreach (FieldBuilder fieldBuilder in fields)
            {
                if (fieldBuilder.DeclaringType.Name == field.DeclaringType.Name && fieldBuilder.Name == field.Name)
                {
                    generator.Emit(code, (FieldInfo) fieldBuilder);
                    break;
                }
            }
        }
    }
}