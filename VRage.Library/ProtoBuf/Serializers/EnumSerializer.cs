// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.EnumSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class EnumSerializer : IProtoSerializer
    {
        private readonly Type enumType;
        private readonly EnumSerializer.EnumPair[] map;

        public Type ExpectedType
        {
            get { return this.enumType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public EnumSerializer(Type enumType, EnumSerializer.EnumPair[] map)
        {
            if (enumType == (Type) null)
                throw new ArgumentNullException("enumType");
            this.enumType = enumType;
            this.map = map;
            if (map == null)
                return;
            for (int index1 = 1; index1 < map.Length; ++index1)
            {
                for (int index2 = 0; index2 < index1; ++index2)
                {
                    if (map[index1].WireValue == map[index2].WireValue &&
                        !object.Equals(map[index1].RawValue, map[index2].RawValue))
                        throw new ProtoException("Multiple enums with wire-value " + (object) map[index1].WireValue);
                    if (object.Equals(map[index1].RawValue, map[index2].RawValue) &&
                        map[index1].WireValue != map[index2].WireValue)
                        throw new ProtoException("Multiple enums with deserialized-value " + map[index1].RawValue);
                }
            }
        }

        private ProtoTypeCode GetTypeCode()
        {
            Type type = Helpers.GetUnderlyingType(this.enumType);
            if (type == (Type) null)
                type = this.enumType;
            return Helpers.GetTypeCode(type);
        }

        private int EnumToWire(object value)
        {
            switch (this.GetTypeCode())
            {
                case ProtoTypeCode.SByte:
                    return (int) (sbyte) value;
                case ProtoTypeCode.Byte:
                    return (int) (byte) value;
                case ProtoTypeCode.Int16:
                    return (int) (short) value;
                case ProtoTypeCode.UInt16:
                    return (int) (ushort) value;
                case ProtoTypeCode.Int32:
                    return (int) value;
                case ProtoTypeCode.UInt32:
                    return (int) (uint) value;
                case ProtoTypeCode.Int64:
                    return (int) (long) value;
                case ProtoTypeCode.UInt64:
                    return (int) (ulong) value;
                default:
                    throw new InvalidOperationException();
            }
        }

        private object WireToEnum(int value)
        {
            switch (this.GetTypeCode())
            {
                case ProtoTypeCode.SByte:
                    return Enum.ToObject(this.enumType, (sbyte) value);
                case ProtoTypeCode.Byte:
                    return Enum.ToObject(this.enumType, (byte) value);
                case ProtoTypeCode.Int16:
                    return Enum.ToObject(this.enumType, (short) value);
                case ProtoTypeCode.UInt16:
                    return Enum.ToObject(this.enumType, (ushort) value);
                case ProtoTypeCode.Int32:
                    return Enum.ToObject(this.enumType, value);
                case ProtoTypeCode.UInt32:
                    return Enum.ToObject(this.enumType, (uint) value);
                case ProtoTypeCode.Int64:
                    return Enum.ToObject(this.enumType, (long) value);
                case ProtoTypeCode.UInt64:
                    return Enum.ToObject(this.enumType, (ulong) value);
                default:
                    throw new InvalidOperationException();
            }
        }

        public object Read(object value, ProtoReader source)
        {
            int num = source.ReadInt32();
            if (this.map == null)
                return this.WireToEnum(num);
            for (int index = 0; index < this.map.Length; ++index)
            {
                if (this.map[index].WireValue == num)
                    return (object) this.map[index].TypedValue;
            }
            source.ThrowEnumException(this.ExpectedType, num);
            return (object) null;
        }

        public void Write(object value, ProtoWriter dest)
        {
            if (this.map == null)
            {
                ProtoWriter.WriteInt32(this.EnumToWire(value), dest);
            }
            else
            {
                for (int index = 0; index < this.map.Length; ++index)
                {
                    if (object.Equals((object) this.map[index].TypedValue, value))
                    {
                        ProtoWriter.WriteInt32(this.map[index].WireValue, dest);
                        return;
                    }
                }
                ProtoWriter.ThrowEnumException(dest, value);
            }
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ProtoTypeCode typeCode = this.GetTypeCode();
            if (this.map == null)
            {
                ctx.LoadValue(valueFrom);
                ctx.ConvertToInt32(typeCode, false);
                ctx.EmitBasicWrite("WriteInt32", (Local) null);
            }
            else
            {
                using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
                {
                    CodeLabel label1 = ctx.DefineLabel();
                    for (int index = 0; index < this.map.Length; ++index)
                    {
                        CodeLabel label2 = ctx.DefineLabel();
                        CodeLabel label3 = ctx.DefineLabel();
                        ctx.LoadValue(localWithValue);
                        EnumSerializer.WriteEnumValue(ctx, typeCode, this.map[index].RawValue);
                        ctx.BranchIfEqual(label3, true);
                        ctx.Branch(label2, true);
                        ctx.MarkLabel(label3);
                        ctx.LoadValue(this.map[index].WireValue);
                        ctx.EmitBasicWrite("WriteInt32", (Local) null);
                        ctx.Branch(label1, false);
                        ctx.MarkLabel(label2);
                    }
                    ctx.LoadReaderWriter();
                    ctx.LoadValue(localWithValue);
                    ctx.CastToObject(this.ExpectedType);
                    ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("ThrowEnumException"));
                    ctx.MarkLabel(label1);
                }
            }
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ProtoTypeCode typeCode = this.GetTypeCode();
            if (this.map == null)
            {
                ctx.EmitBasicRead("ReadInt32", ctx.MapType(typeof (int)));
                ctx.ConvertFromInt32(typeCode, false);
            }
            else
            {
                int[] keys = new int[this.map.Length];
                object[] values = new object[this.map.Length];
                for (int index = 0; index < this.map.Length; ++index)
                {
                    keys[index] = this.map[index].WireValue;
                    values[index] = this.map[index].RawValue;
                }
                using (Local local1 = new Local(ctx, this.ExpectedType))
                {
                    using (Local local2 = new Local(ctx, ctx.MapType(typeof (int))))
                    {
                        ctx.EmitBasicRead("ReadInt32", ctx.MapType(typeof (int)));
                        ctx.StoreValue(local2);
                        CodeLabel codeLabel1 = ctx.DefineLabel();
                        foreach (BasicList.Group group in BasicList.GetContiguousGroups(keys, values))
                        {
                            CodeLabel label = ctx.DefineLabel();
                            int count = group.Items.Count;
                            if (count == 1)
                            {
                                ctx.LoadValue(local2);
                                ctx.LoadValue(group.First);
                                CodeLabel codeLabel2 = ctx.DefineLabel();
                                ctx.BranchIfEqual(codeLabel2, true);
                                ctx.Branch(label, false);
                                EnumSerializer.WriteEnumValue(ctx, typeCode, codeLabel2, codeLabel1, group.Items[0],
                                    local1);
                            }
                            else
                            {
                                ctx.LoadValue(local2);
                                ctx.LoadValue(group.First);
                                ctx.Subtract();
                                CodeLabel[] jumpTable = new CodeLabel[count];
                                for (int index = 0; index < count; ++index)
                                    jumpTable[index] = ctx.DefineLabel();
                                ctx.Switch(jumpTable);
                                ctx.Branch(label, false);
                                for (int index = 0; index < count; ++index)
                                    EnumSerializer.WriteEnumValue(ctx, typeCode, jumpTable[index], codeLabel1,
                                        group.Items[index], local1);
                            }
                            ctx.MarkLabel(label);
                        }
                        ctx.LoadReaderWriter();
                        ctx.LoadValue(this.ExpectedType);
                        ctx.LoadValue(local2);
                        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("ThrowEnumException"));
                        ctx.MarkLabel(codeLabel1);
                        ctx.LoadValue(local1);
                    }
                }
            }
        }

        private static void WriteEnumValue(CompilerContext ctx, ProtoTypeCode typeCode, object value)
        {
            switch (typeCode)
            {
                case ProtoTypeCode.SByte:
                    ctx.LoadValue((int) (sbyte) value);
                    break;
                case ProtoTypeCode.Byte:
                    ctx.LoadValue((int) (byte) value);
                    break;
                case ProtoTypeCode.Int16:
                    ctx.LoadValue((int) (short) value);
                    break;
                case ProtoTypeCode.UInt16:
                    ctx.LoadValue((int) (ushort) value);
                    break;
                case ProtoTypeCode.Int32:
                    ctx.LoadValue((int) value);
                    break;
                case ProtoTypeCode.UInt32:
                    ctx.LoadValue((int) (uint) value);
                    break;
                case ProtoTypeCode.Int64:
                    ctx.LoadValue((long) value);
                    break;
                case ProtoTypeCode.UInt64:
                    ctx.LoadValue((long) (ulong) value);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static void WriteEnumValue(CompilerContext ctx, ProtoTypeCode typeCode, CodeLabel handler,
            CodeLabel @continue, object value, Local local)
        {
            ctx.MarkLabel(handler);
            EnumSerializer.WriteEnumValue(ctx, typeCode, value);
            ctx.StoreValue(local);
            ctx.Branch(@continue, false);
        }

        public struct EnumPair
        {
            public readonly object RawValue;
            public readonly Enum TypedValue;
            public readonly int WireValue;

            public EnumPair(int wireValue, object raw, Type type)
            {
                this.WireValue = wireValue;
                this.RawValue = raw;
                this.TypedValue = (Enum) Enum.ToObject(type, raw);
            }
        }
    }
}