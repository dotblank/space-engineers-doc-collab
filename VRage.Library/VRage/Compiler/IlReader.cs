// Decompiled with JetBrains decompiler
// Type: VRage.Compiler.IlReader
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using VRage;

namespace VRage.Compiler
{
    public class IlReader
    {
        private BinaryReader stream;
        private OpCode[] singleByteOpCode;
        private OpCode[] doubleByteOpCode;
        private byte[] instructions;
        private IList<LocalVariableInfo> locals;
        private ParameterInfo[] parameters;
        private Type[] typeArgs;
        private Type[] methodArgs;
        private MethodBase currentMethod;
        private List<IlReader.IlInstruction> ilInstructions;

        public IList<LocalVariableInfo> Locals
        {
            get { return this.locals; }
        }

        public IlReader()
        {
            this.CreateOpCodes();
        }

        private void CreateOpCodes()
        {
            this.singleByteOpCode = new OpCode[225];
            this.doubleByteOpCode = new OpCode[31];
            foreach (FieldInfo fieldInfo in this.GetOpCodeFields())
            {
                OpCode opCode = (OpCode) fieldInfo.GetValue((object) null);
                if (opCode.OpCodeType != OpCodeType.Nternal)
                {
                    if (opCode.Size == 1)
                        this.singleByteOpCode[(int) opCode.Value] = opCode;
                    else
                        this.doubleByteOpCode[(int) opCode.Value & (int) byte.MaxValue] = opCode;
                }
            }
        }

        public List<IlReader.IlInstruction> ReadInstructions(MethodBase method)
        {
            this.ilInstructions = new List<IlReader.IlInstruction>();
            this.currentMethod = method;
            MethodBody methodBody = method.GetMethodBody();
            this.parameters = method.GetParameters();
            if (methodBody == null)
                return this.ilInstructions;
            this.locals = methodBody.LocalVariables;
            this.instructions = method.GetMethodBody().GetILAsByteArray();
            this.stream = new BinaryReader((Stream) new ByteStream(this.instructions, this.instructions.Length));
            if (!typeof (ConstructorInfo).IsAssignableFrom(method.GetType()))
                this.methodArgs = method.GetGenericArguments();
            if (method.DeclaringType != (Type) null)
                this.typeArgs = method.DeclaringType.GetGenericArguments();
            while (this.stream.BaseStream.Position < this.stream.BaseStream.Length)
            {
                IlReader.IlInstruction ilInstruction = new IlReader.IlInstruction();
                OpCode code = this.ReadOpCode();
                ilInstruction.OpCode = code;
                ilInstruction.Offset = this.stream.BaseStream.Position - 1L;
                ilInstruction.Operand = this.ReadOperand(code, method.Module, ref ilInstruction.LocalVariableIndex);
                this.ilInstructions.Add(ilInstruction);
            }
            return this.ilInstructions;
        }

        private object ReadOperand(OpCode code, Module module, ref long localVariableIndex)
        {
            object obj = (object) null;
            switch (code.OperandType)
            {
                case OperandType.InlineBrTarget:
                    obj = (object) ((long) this.stream.ReadInt32() + this.stream.BaseStream.Position);
                    goto case 5;
                case OperandType.InlineField:
                case OperandType.InlineMethod:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                    obj = (object) module.ResolveMember(this.stream.ReadInt32(), this.typeArgs, this.methodArgs);
                    goto case 5;
                case OperandType.InlineI:
                    obj = (object) this.stream.ReadInt32();
                    goto case 5;
                case OperandType.InlineI8:
                    obj = (object) this.stream.ReadInt64();
                    goto case 5;
                case OperandType.InlineNone:
                    return obj;
                case OperandType.InlineR:
                    obj = (object) this.stream.ReadDouble();
                    goto case 5;
                case OperandType.InlineSig:
                    obj = (object) module.ResolveSignature(this.stream.ReadInt32());
                    goto case 5;
                case OperandType.InlineString:
                    obj = (object) module.ResolveString(this.stream.ReadInt32());
                    goto case 5;
                case OperandType.InlineSwitch:
                    int length = this.stream.ReadInt32();
                    int[] numArray1 = new int[length];
                    int[] numArray2 = new int[length];
                    for (int index = 0; index < length; ++index)
                        numArray2[index] = this.stream.ReadInt32();
                    for (int index = 0; index < length; ++index)
                        numArray1[index] = (int) this.stream.BaseStream.Position + numArray2[index];
                    goto case 5;
                case OperandType.InlineVar:
                    int index1 = (int) this.stream.ReadUInt16();
                    obj = this.GetVariable(code, index1);
                    localVariableIndex = (long) index1;
                    goto case 5;
                case OperandType.ShortInlineBrTarget:
                    obj = code.FlowControl == FlowControl.Branch || code.FlowControl == FlowControl.Cond_Branch
                        ? (object) ((long) this.stream.ReadSByte() + this.stream.BaseStream.Position)
                        : (object) this.stream.ReadSByte();
                    goto case 5;
                case OperandType.ShortInlineI:
                    obj = !(code == OpCodes.Ldc_I4_S)
                        ? (object) this.stream.ReadByte()
                        : (object) (sbyte) this.stream.ReadByte();
                    goto case 5;
                case OperandType.ShortInlineR:
                    obj = (object) this.stream.ReadSingle();
                    goto case 5;
                case OperandType.ShortInlineVar:
                    int index2 = (int) this.stream.ReadByte();
                    obj = this.GetVariable(code, index2);
                    localVariableIndex = (long) index2;
                    goto case 5;
                default:
                    throw new NotSupportedException();
            }
        }

        private OpCode ReadOpCode()
        {
            byte num = this.stream.ReadByte();
            if ((int) num != 254)
                return this.singleByteOpCode[(int) num];
            else
                return this.doubleByteOpCode[(int) this.stream.ReadByte()];
        }

        private object GetVariable(OpCode code, int index)
        {
            if (code.Name.Contains("loc"))
                return (object) this.locals[index];
            if (!this.currentMethod.IsStatic)
                --index;
            return (object) this.parameters[index];
        }

        private FieldInfo[] GetOpCodeFields()
        {
            return typeof (OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public);
        }

        public class IlInstruction
        {
            public OpCode OpCode;
            public object Operand;
            public long Offset;
            public long LocalVariableIndex;

            public string FormatOperand()
            {
                switch (this.OpCode.OperandType)
                {
                    case OperandType.InlineField:
                    case OperandType.InlineMethod:
                    case OperandType.InlineTok:
                    case OperandType.InlineType:
                        if (this.Operand is MethodInfo)
                        {
                            MethodInfo methodInfo = (MethodInfo) this.Operand;
                            string str =
                                methodInfo.ToString()
                                    .Substring(((object) methodInfo.ReturnType.Name).ToString().Length + 1);
                            return string.Format("{0} {1}::{2}", (object) methodInfo.ReturnType,
                                (object) methodInfo.DeclaringType, (object) str);
                        }
                        else
                        {
                            if (!(this.Operand is ConstructorInfo))
                                return this.Operand.ToString();
                            ConstructorInfo constructorInfo = (ConstructorInfo) this.Operand;
                            string str = constructorInfo.ToString().Substring("Void".Length + 1);
                            return string.Format("{0}::{1}", (object) constructorInfo.DeclaringType, (object) str);
                        }
                    case OperandType.InlineNone:
                        return string.Empty;
                    default:
                        return this.Operand.ToString();
                }
            }

            public override string ToString()
            {
                return (string) (object) this.OpCode + (object) " " + this.FormatOperand();
            }
        }
    }
}