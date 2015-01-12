// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoReader
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProtoBuf
{
    public sealed class ProtoReader : IDisposable
    {
        private static readonly UTF8Encoding encoding = new UTF8Encoding();
        private static readonly byte[] EmptyBlob = new byte[0];
        private WireType wireType = WireType.None;
        private bool internStrings = true;
        private int blockEnd = int.MaxValue;
        private readonly NetObjectCache netCache = new NetObjectCache();
        private uint trapCount = 1U;
        private const long Int64Msb = -9223372036854775808L;
        private const int Int32Msb = -2147483648;
        private Stream source;
        private byte[] ioBuffer;
        private TypeModel model;
        private int fieldNumber;
        private int dataRemaining;
        private readonly bool isFixedLength;
        private readonly SerializationContext context;
        private int ioIndex;
        private int position;
        private int available;
        private Dictionary<string, string> stringInterner;
        private int depth;

        public int FieldNumber
        {
            get { return this.fieldNumber; }
        }

        public WireType WireType
        {
            get { return this.wireType; }
        }

        public bool InternStrings
        {
            get { return this.internStrings; }
            set { this.internStrings = value; }
        }

        public SerializationContext Context
        {
            get { return this.context; }
        }

        public int Position
        {
            get { return this.position; }
        }

        public TypeModel Model
        {
            get { return this.model; }
        }

        internal NetObjectCache NetCache
        {
            get { return this.netCache; }
        }

        public ProtoReader(Stream source, TypeModel model, SerializationContext context)
            : this(source, model, context, -1)
        {
        }

        public ProtoReader(Stream source, TypeModel model, SerializationContext context, int length)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (!source.CanRead)
                throw new ArgumentException("Cannot read from stream", "source");
            this.source = source;
            this.ioBuffer = BufferPool.GetBuffer();
            this.model = model;
            this.isFixedLength = length >= 0;
            this.dataRemaining = this.isFixedLength ? length : 0;
            if (context == null)
                context = SerializationContext.Default;
            else
                context.Freeze();
            this.context = context;
        }

        public void Dispose()
        {
            this.source = (Stream) null;
            this.model = (TypeModel) null;
            BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
        }

        internal int TryReadUInt32VariantWithoutMoving(bool trimNegative, out uint value)
        {
            // removed because trying to fix the errors is pointless
            value = 0;
            return 0;
        }

        private uint ReadUInt32Variant(bool trimNegative)
        {
            uint num1;
            int num2 = this.TryReadUInt32VariantWithoutMoving(trimNegative, out num1);
            if (num2 <= 0)
                throw ProtoReader.EoF(this);
            this.ioIndex += num2;
            this.available -= num2;
            this.position += num2;
            return num1;
        }

        private bool TryReadUInt32Variant(out uint value)
        {
            int num = this.TryReadUInt32VariantWithoutMoving(false, out value);
            if (num <= 0)
                return false;
            this.ioIndex += num;
            this.available -= num;
            this.position += num;
            return true;
        }

        public uint ReadUInt32()
        {
            switch (this.wireType)
            {
                case WireType.Variant:
                    return this.ReadUInt32Variant(false);
                case WireType.Fixed64:
                    return checked ((uint) this.ReadUInt64());
                case WireType.Fixed32:
                    if (this.available < 4)
                        this.Ensure(4, true);
                    this.position += 4;
                    this.available -= 4;
                    return
                        (uint)
                            ((int) this.ioBuffer[this.ioIndex++] | (int) this.ioBuffer[this.ioIndex++] << 8 |
                             (int) this.ioBuffer[this.ioIndex++] << 16 | (int) this.ioBuffer[this.ioIndex++] << 24);
                default:
                    throw this.CreateWireTypeException();
            }
        }

        internal void Ensure(int count, bool strict)
        {
            if (count > this.ioBuffer.Length)
            {
                BufferPool.ResizeAndFlushLeft(ref this.ioBuffer, count, this.ioIndex, this.available);
                this.ioIndex = 0;
            }
            else if (this.ioIndex + count >= this.ioBuffer.Length)
            {
                Helpers.BlockCopy(this.ioBuffer, this.ioIndex, this.ioBuffer, 0, this.available);
                this.ioIndex = 0;
            }
            count -= this.available;
            int offset = this.ioIndex + this.available;
            int count1 = this.ioBuffer.Length - offset;
            if (this.isFixedLength && this.dataRemaining < count1)
                count1 = this.dataRemaining;
            int num;
            while (count > 0 && count1 > 0 && (num = this.source.Read(this.ioBuffer, offset, count1)) > 0)
            {
                this.available += num;
                count -= num;
                count1 -= num;
                offset += num;
                if (this.isFixedLength)
                    this.dataRemaining -= num;
            }
            if (strict && count > 0)
                throw ProtoReader.EoF(this);
        }

        public short ReadInt16()
        {
            return checked ((short) this.ReadInt32());
        }

        public ushort ReadUInt16()
        {
            return checked ((ushort) this.ReadUInt32());
        }

        public byte ReadByte()
        {
            return checked ((byte) this.ReadUInt32());
        }

        public sbyte ReadSByte()
        {
            return checked ((sbyte) this.ReadInt32());
        }

        public int ReadInt32()
        {
            switch (this.wireType)
            {
                case WireType.Variant:
                    return (int) this.ReadUInt32Variant(true);
                case WireType.Fixed64:
                    return checked ((int) this.ReadInt64());
                case WireType.Fixed32:
                    if (this.available < 4)
                        this.Ensure(4, true);
                    this.position += 4;
                    this.available -= 4;
                    return (int) this.ioBuffer[this.ioIndex++] | (int) this.ioBuffer[this.ioIndex++] << 8 |
                           (int) this.ioBuffer[this.ioIndex++] << 16 | (int) this.ioBuffer[this.ioIndex++] << 24;
                case WireType.SignedVariant:
                    return ProtoReader.Zag(this.ReadUInt32Variant(true));
                default:
                    throw this.CreateWireTypeException();
            }
        }

        private static int Zag(uint ziggedValue)
        {
            int num = (int) ziggedValue;
            return -(num & 1) ^ num >> 1 & int.MaxValue;
        }

        private static long Zag(ulong ziggedValue)
        {
            long num = (long) ziggedValue;
            return -(num & 1L) ^ num >> 1 & long.MaxValue;
        }

        public long ReadInt64()
        {
            switch (this.wireType)
            {
                case WireType.Variant:
                    return (long) this.ReadUInt64Variant();
                case WireType.Fixed64:
                    if (this.available < 8)
                        this.Ensure(8, true);
                    this.position += 8;
                    this.available -= 8;
                    return (long) this.ioBuffer[this.ioIndex++] | (long) this.ioBuffer[this.ioIndex++] << 8 |
                           (long) this.ioBuffer[this.ioIndex++] << 16 | (long) this.ioBuffer[this.ioIndex++] << 24 |
                           (long) this.ioBuffer[this.ioIndex++] << 32 | (long) this.ioBuffer[this.ioIndex++] << 40 |
                           (long) this.ioBuffer[this.ioIndex++] << 48 | (long) this.ioBuffer[this.ioIndex++] << 56;
                case WireType.Fixed32:
                    return (long) this.ReadInt32();
                case WireType.SignedVariant:
                    return ProtoReader.Zag(this.ReadUInt64Variant());
                default:
                    throw this.CreateWireTypeException();
            }
        }

        private int TryReadUInt64VariantWithoutMoving(out ulong value)
        {
            // removed because trying to fix the errors is pointless
            value = 0;
            return 0;
        }

        private ulong ReadUInt64Variant()
        {
            ulong num1;
            int num2 = this.TryReadUInt64VariantWithoutMoving(out num1);
            if (num2 <= 0)
                throw ProtoReader.EoF(this);
            this.ioIndex += num2;
            this.available -= num2;
            this.position += num2;
            return num1;
        }

        private string Intern(string value)
        {
            if (value == null)
                return (string) null;
            if (value.Length == 0)
                return "";
            if (this.stringInterner == null)
            {
                this.stringInterner = new Dictionary<string, string>();
                this.stringInterner.Add(value, value);
            }
            else
            {
                string str;
                if (this.stringInterner.TryGetValue(value, out str))
                    value = str;
                else
                    this.stringInterner.Add(value, value);
            }
            return value;
        }

        public string ReadString()
        {
            if (this.wireType != WireType.String)
                throw this.CreateWireTypeException();
            int count = (int) this.ReadUInt32Variant(false);
            if (count == 0)
                return "";
            if (this.available < count)
                this.Ensure(count, true);
            string str = ProtoReader.encoding.GetString(this.ioBuffer, this.ioIndex, count);
            if (this.internStrings)
                str = this.Intern(str);
            this.available -= count;
            this.position += count;
            this.ioIndex += count;
            return str;
        }

        public void ThrowEnumException(Type type, int value)
        {
            throw ProtoReader.AddErrorData((Exception) new ProtoException(string.Concat(new object[4]
            {
                (object) "No ",
                (object) (type == (Type) null ? "<null>" : type.FullName),
                (object) " enum is mapped to the wire-value ",
                (object) value
            })), this);
        }

        private Exception CreateWireTypeException()
        {
            return
                this.CreateException(
                    "Invalid wire-type; this usually means you have over-written a file without truncating or setting the length; see http://stackoverflow.com/q/2152978/23354");
        }

        private Exception CreateException(string message)
        {
            return ProtoReader.AddErrorData((Exception) new ProtoException(message), this);
        }

        public unsafe double ReadDouble()
        {
            // removed because trying to fix the errors is pointless
            return 0;
        }

        public static object ReadObject(object value, int key, ProtoReader reader)
        {
            return ProtoReader.ReadTypedObject(value, key, reader, (Type) null);
        }

        internal static object ReadTypedObject(object value, int key, ProtoReader reader, Type type)
        {
            if (reader.model == null)
                throw ProtoReader.AddErrorData(
                    (Exception)
                        new InvalidOperationException("Cannot deserialize sub-objects unless a model is provided"),
                    reader);
            SubItemToken token = ProtoReader.StartSubItem(reader);
            if (key >= 0)
                value = reader.model.Deserialize(key, value, reader);
            else if (!(type != (Type) null) ||
                     !reader.model.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true,
                         false, true, false))
                TypeModel.ThrowUnexpectedType(type);
            ProtoReader.EndSubItem(token, reader);
            return value;
        }

        public static void EndSubItem(SubItemToken token, ProtoReader reader)
        {
            int num = token.value;
            if (reader.wireType == WireType.EndGroup)
            {
                if (num >= 0)
                    throw ProtoReader.AddErrorData((Exception) new ArgumentException("token"), reader);
                if (-num != reader.fieldNumber)
                    throw reader.CreateException("Wrong group was ended");
                reader.wireType = WireType.None;
                --reader.depth;
            }
            else
            {
                if (num < reader.position)
                    throw reader.CreateException("Sub-message not read entirely");
                if (reader.blockEnd != reader.position && reader.blockEnd != int.MaxValue)
                    throw reader.CreateException("Sub-message not read correctly");
                reader.blockEnd = num;
                --reader.depth;
            }
        }

        public static SubItemToken StartSubItem(ProtoReader reader)
        {
            switch (reader.wireType)
            {
                case WireType.String:
                    int num1 = (int) reader.ReadUInt32Variant(false);
                    if (num1 < 0)
                        throw ProtoReader.AddErrorData((Exception) new InvalidOperationException(), reader);
                    int num2 = reader.blockEnd;
                    reader.blockEnd = reader.position + num1;
                    ++reader.depth;
                    return new SubItemToken(num2);
                case WireType.StartGroup:
                    reader.wireType = WireType.None;
                    ++reader.depth;
                    return new SubItemToken(-reader.fieldNumber);
                default:
                    throw reader.CreateWireTypeException();
            }
        }

        public int ReadFieldHeader()
        {
            if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
                return 0;
            uint num;
            if (this.TryReadUInt32Variant(out num))
            {
                this.wireType = (WireType) ((int) num & 7);
                this.fieldNumber = (int) (num >> 3);
                if (this.fieldNumber < 1)
                    throw new ProtoException("Invalid field in source data: " + (object) this.fieldNumber);
            }
            else
            {
                this.wireType = WireType.None;
                this.fieldNumber = 0;
            }
            if (this.wireType != WireType.EndGroup)
                return this.fieldNumber;
            if (this.depth > 0)
                return 0;
            else
                throw new ProtoException(
                    "Unexpected end-group in source data; this usually means the source data is corrupt");
        }

        public bool TryReadFieldHeader(int field)
        {
            if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
                return false;
            uint num1;
            int num2 = this.TryReadUInt32VariantWithoutMoving(false, out num1);
            WireType wireType;
            if (num2 <= 0 || (int) num1 >> 3 != field || (wireType = (WireType) ((int) num1 & 7)) == WireType.EndGroup)
                return false;
            this.wireType = wireType;
            this.fieldNumber = field;
            this.position += num2;
            this.ioIndex += num2;
            this.available -= num2;
            return true;
        }

        public void Hint(WireType wireType)
        {
            if (this.wireType == wireType || (wireType & (WireType) 7) != this.wireType)
                return;
            this.wireType = wireType;
        }

        public void Assert(WireType wireType)
        {
            if (this.wireType == wireType)
                return;
            if ((wireType & (WireType) 7) != this.wireType)
                throw this.CreateWireTypeException();
            this.wireType = wireType;
        }

        public void SkipField()
        {
            switch (this.wireType)
            {
                case WireType.Variant:
                case WireType.SignedVariant:
                    long num1 = (long) this.ReadUInt64Variant();
                    break;
                case WireType.Fixed64:
                    if (this.available < 8)
                        this.Ensure(8, true);
                    this.available -= 8;
                    this.ioIndex += 8;
                    this.position += 8;
                    break;
                case WireType.String:
                    int num2 = (int) this.ReadUInt32Variant(false);
                    if (num2 <= this.available)
                    {
                        this.available -= num2;
                        this.ioIndex += num2;
                        this.position += num2;
                        break;
                    }
                    else
                    {
                        this.position += num2;
                        int count = num2 - this.available;
                        this.ioIndex = this.available = 0;
                        if (this.isFixedLength)
                        {
                            if (count > this.dataRemaining)
                                throw ProtoReader.EoF(this);
                            this.dataRemaining -= count;
                        }
                        ProtoReader.Seek(this.source, count, this.ioBuffer);
                        break;
                    }
                case WireType.StartGroup:
                    int num3 = this.fieldNumber;
                    ++this.depth;
                    while (this.ReadFieldHeader() > 0)
                        this.SkipField();
                    --this.depth;
                    if (this.wireType != WireType.EndGroup || this.fieldNumber != num3)
                        throw this.CreateWireTypeException();
                    this.wireType = WireType.None;
                    break;
                case WireType.Fixed32:
                    if (this.available < 4)
                        this.Ensure(4, true);
                    this.available -= 4;
                    this.ioIndex += 4;
                    this.position += 4;
                    break;
                default:
                    throw this.CreateWireTypeException();
            }
        }

        public ulong ReadUInt64()
        {
            switch (this.wireType)
            {
                case WireType.Variant:
                    return this.ReadUInt64Variant();
                case WireType.Fixed64:
                    if (this.available < 8)
                        this.Ensure(8, true);
                    this.position += 8;
                    this.available -= 8;
                    return
                        (ulong)
                            ((long) this.ioBuffer[this.ioIndex++] | (long) this.ioBuffer[this.ioIndex++] << 8 |
                             (long) this.ioBuffer[this.ioIndex++] << 16 | (long) this.ioBuffer[this.ioIndex++] << 24 |
                             (long) this.ioBuffer[this.ioIndex++] << 32 | (long) this.ioBuffer[this.ioIndex++] << 40 |
                             (long) this.ioBuffer[this.ioIndex++] << 48 | (long) this.ioBuffer[this.ioIndex++] << 56);
                case WireType.Fixed32:
                    return (ulong) this.ReadUInt32();
                default:
                    throw this.CreateWireTypeException();
            }
        }

        public unsafe float ReadSingle()
        {
            // removed because trying to fix the errors is pointless
            return 0;
        }

        public bool ReadBoolean()
        {
            switch (this.ReadUInt32())
            {
                case 0U:
                    return false;
                case 1U:
                    return true;
                default:
                    throw this.CreateException("Unexpected boolean value");
            }
        }

        public static byte[] AppendBytes(byte[] value, ProtoReader reader)
        {
            if (reader.wireType != WireType.String)
                throw reader.CreateWireTypeException();
            int count1 = (int) reader.ReadUInt32Variant(false);
            reader.wireType = WireType.None;
            if (count1 == 0)
            {
                if (value != null)
                    return value;
                else
                    return ProtoReader.EmptyBlob;
            }
            else
            {
                int toIndex;
                if (value == null || value.Length == 0)
                {
                    toIndex = 0;
                    value = new byte[count1];
                }
                else
                {
                    toIndex = value.Length;
                    byte[] to = new byte[value.Length + count1];
                    Helpers.BlockCopy(value, 0, to, 0, value.Length);
                    value = to;
                }
                reader.position += count1;
                while (count1 > reader.available)
                {
                    if (reader.available > 0)
                    {
                        Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, toIndex, reader.available);
                        count1 -= reader.available;
                        toIndex += reader.available;
                        reader.ioIndex = reader.available = 0;
                    }
                    int count2 = count1 > reader.ioBuffer.Length ? reader.ioBuffer.Length : count1;
                    if (count2 > 0)
                        reader.Ensure(count2, true);
                }
                if (count1 > 0)
                {
                    Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, toIndex, count1);
                    reader.ioIndex += count1;
                    reader.available -= count1;
                }
                return value;
            }
        }

        private static byte[] ReadBytes(Stream stream, int length)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");
            byte[] buffer = new byte[length];
            int offset = 0;
            int num;
            while (length > 0 && (num = stream.Read(buffer, offset, length)) > 0)
                length -= num;
            if (length > 0)
                throw ProtoReader.EoF((ProtoReader) null);
            else
                return buffer;
        }

        private static int ReadByteOrThrow(Stream source)
        {
            int num = source.ReadByte();
            if (num < 0)
                throw ProtoReader.EoF((ProtoReader) null);
            else
                return num;
        }

        public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber)
        {
            int bytesRead;
            return ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out bytesRead);
        }

        public static int DirectReadLittleEndianInt32(Stream source)
        {
            return ProtoReader.ReadByteOrThrow(source) | ProtoReader.ReadByteOrThrow(source) << 8 |
                   ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
        }

        public static int DirectReadBigEndianInt32(Stream source)
        {
            return ProtoReader.ReadByteOrThrow(source) << 24 | ProtoReader.ReadByteOrThrow(source) << 16 |
                   ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
        }

        public static int DirectReadVarintInt32(Stream source)
        {
            uint num;
            if (ProtoReader.TryReadUInt32Variant(source, out num) <= 0)
                throw ProtoReader.EoF((ProtoReader) null);
            else
                return (int) num;
        }

        public static void DirectReadBytes(Stream source, byte[] buffer, int offset, int count)
        {
            int num;
            while (count > 0 && (num = source.Read(buffer, offset, count)) > 0)
            {
                count -= num;
                offset += num;
            }
            if (count > 0)
                throw ProtoReader.EoF((ProtoReader) null);
        }

        public static byte[] DirectReadBytes(Stream source, int count)
        {
            byte[] buffer = new byte[count];
            ProtoReader.DirectReadBytes(source, buffer, 0, count);
            return buffer;
        }

        public static string DirectReadString(Stream source, int length)
        {
            byte[] numArray = new byte[length];
            ProtoReader.DirectReadBytes(source, numArray, 0, length);
            return Encoding.UTF8.GetString(numArray, 0, length);
        }

        public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber,
            out int bytesRead)
        {
            fieldNumber = 0;
            switch (style)
            {
                case PrefixStyle.None:
                    bytesRead = 0;
                    return int.MaxValue;
                case PrefixStyle.Base128:
                    bytesRead = 0;
                    if (expectHeader)
                    {
                        uint num1;
                        int num2 = ProtoReader.TryReadUInt32Variant(source, out num1);
                        bytesRead += num2;
                        if (num2 > 0)
                        {
                            if (((int) num1 & 7) != 2)
                                throw new InvalidOperationException();
                            fieldNumber = (int) (num1 >> 3);
                            int num3 = ProtoReader.TryReadUInt32Variant(source, out num1);
                            bytesRead += num3;
                            if (bytesRead == 0)
                                throw ProtoReader.EoF((ProtoReader) null);
                            else
                                return (int) num1;
                        }
                        else
                        {
                            bytesRead = 0;
                            return -1;
                        }
                    }
                    else
                    {
                        uint num1;
                        int num2 = ProtoReader.TryReadUInt32Variant(source, out num1);
                        bytesRead += num2;
                        if (bytesRead >= 0)
                            return (int) num1;
                        else
                            return -1;
                    }
                case PrefixStyle.Fixed32:
                    int num4 = source.ReadByte();
                    if (num4 < 0)
                    {
                        bytesRead = 0;
                        return -1;
                    }
                    else
                    {
                        bytesRead = 4;
                        return num4 | ProtoReader.ReadByteOrThrow(source) << 8 |
                               ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
                    }
                case PrefixStyle.Fixed32BigEndian:
                    int num5 = source.ReadByte();
                    if (num5 < 0)
                    {
                        bytesRead = 0;
                        return -1;
                    }
                    else
                    {
                        bytesRead = 4;
                        return num5 << 24 | ProtoReader.ReadByteOrThrow(source) << 16 |
                               ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
                    }
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        private static int TryReadUInt32Variant(Stream source, out uint value)
        {
            value = 0U;
            int num1 = source.ReadByte();
            if (num1 < 0)
                return 0;
            value = (uint) num1;
            if (((int) value & 128) == 0)
                return 1;
            value &= (uint) sbyte.MaxValue;
            int num2 = source.ReadByte();
            if (num2 < 0)
                throw ProtoReader.EoF((ProtoReader) null);
            value |= (uint) ((num2 & (int) sbyte.MaxValue) << 7);
            if ((num2 & 128) == 0)
                return 2;
            int num3 = source.ReadByte();
            if (num3 < 0)
                throw ProtoReader.EoF((ProtoReader) null);
            value |= (uint) ((num3 & (int) sbyte.MaxValue) << 14);
            if ((num3 & 128) == 0)
                return 3;
            int num4 = source.ReadByte();
            if (num4 < 0)
                throw ProtoReader.EoF((ProtoReader) null);
            value |= (uint) ((num4 & (int) sbyte.MaxValue) << 21);
            if ((num4 & 128) == 0)
                return 4;
            int num5 = source.ReadByte();
            if (num5 < 0)
                throw ProtoReader.EoF((ProtoReader) null);
            value |= (uint) (num5 << 28);
            if ((num5 & 240) == 0)
                return 5;
            else
                throw new OverflowException();
        }

        internal static void Seek(Stream source, int count, byte[] buffer)
        {
            if (source.CanSeek)
            {
                source.Seek((long) count, SeekOrigin.Current);
                count = 0;
            }
            else if (buffer != null)
            {
                int num1;
                while (count > buffer.Length && (num1 = source.Read(buffer, 0, buffer.Length)) > 0)
                    count -= num1;
                int num2;
                while (count > 0 && (num2 = source.Read(buffer, 0, count)) > 0)
                    count -= num2;
            }
            else
            {
                buffer = BufferPool.GetBuffer();
                try
                {
                    int num1;
                    while (count > buffer.Length && (num1 = source.Read(buffer, 0, buffer.Length)) > 0)
                        count -= num1;
                    while (count > 0)
                    {
                        int num2;
                        if ((num2 = source.Read(buffer, 0, count)) > 0)
                            count -= num2;
                        else
                            break;
                    }
                }
                finally
                {
                    BufferPool.ReleaseBufferToPool(ref buffer);
                }
            }
            if (count > 0)
                throw ProtoReader.EoF((ProtoReader) null);
        }

        internal static Exception AddErrorData(Exception exception, ProtoReader source)
        {
            if (exception != null && source != null && !exception.Data.Contains((object) "protoSource"))
                exception.Data.Add((object) "protoSource",
                    (object)
                        string.Format("tag={0}; wire-type={1}; offset={2}; depth={3}", (object) source.fieldNumber,
                            (object) source.wireType, (object) source.position, (object) source.depth));
            return exception;
        }

        private static Exception EoF(ProtoReader source)
        {
            return ProtoReader.AddErrorData((Exception) new EndOfStreamException(), source);
        }

        public void AppendExtensionData(IExtensible instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            IExtension extensionObject = instance.GetExtensionObject(true);
            bool commit = false;
            Stream stream = extensionObject.BeginAppend();
            try
            {
                using (ProtoWriter writer = new ProtoWriter(stream, this.model, (SerializationContext) null))
                {
                    this.AppendExtensionField(writer);
                    writer.Close();
                }
                commit = true;
            }
            finally
            {
                extensionObject.EndAppend(stream, commit);
            }
        }

        private void AppendExtensionField(ProtoWriter writer)
        {
            ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, writer);
            switch (this.wireType)
            {
                case WireType.Variant:
                case WireType.Fixed64:
                case WireType.SignedVariant:
                    ProtoWriter.WriteInt64(this.ReadInt64(), writer);
                    break;
                case WireType.String:
                    ProtoWriter.WriteBytes(ProtoReader.AppendBytes((byte[]) null, this), writer);
                    break;
                case WireType.StartGroup:
                    SubItemToken token1 = ProtoReader.StartSubItem(this);
                    SubItemToken token2 = ProtoWriter.StartSubItem((object) null, writer);
                    while (this.ReadFieldHeader() > 0)
                        this.AppendExtensionField(writer);
                    ProtoReader.EndSubItem(token1, this);
                    ProtoWriter.EndSubItem(token2, writer);
                    break;
                case WireType.Fixed32:
                    ProtoWriter.WriteInt32(this.ReadInt32(), writer);
                    break;
                default:
                    throw this.CreateWireTypeException();
            }
        }

        public static bool HasSubValue(WireType wireType, ProtoReader source)
        {
            if (source.blockEnd <= source.position || wireType == WireType.EndGroup)
                return false;
            source.wireType = wireType;
            return true;
        }

        internal int GetTypeKey(ref Type type)
        {
            return this.model.GetKey(ref type);
        }

        internal Type DeserializeType(string value)
        {
            return TypeModel.DeserializeType(this.model, value);
        }

        internal void SetRootObject(object value)
        {
            this.netCache.SetKeyedObject(0, value);
            --this.trapCount;
        }

        public static void NoteObject(object value, ProtoReader reader)
        {
            if ((int) reader.trapCount == 0)
                return;
            reader.netCache.RegisterTrappedObject(value);
            --reader.trapCount;
        }

        public Type ReadType()
        {
            return TypeModel.DeserializeType(this.model, this.ReadString());
        }

        internal void TrapNextObject(int newObjectKey)
        {
            ++this.trapCount;
            this.netCache.SetKeyedObject(newObjectKey, (object) null);
        }

        internal void CheckFullyConsumed()
        {
            if (this.isFixedLength)
            {
                if (this.dataRemaining != 0)
                    throw new ProtoException("Incorrect number of bytes consumed");
            }
            else if (this.available != 0)
                throw new ProtoException("Unconsumed data left in the buffer; this suggests corrupt input");
        }

        public static object Merge(ProtoReader parent, object from, object to)
        {
            TypeModel model = parent.Model;
            SerializationContext context = parent.Context;
            if (model == null)
                throw new InvalidOperationException("Types cannot be merged unless a type-model has been specified");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                model.Serialize((Stream) memoryStream, from, context);
                memoryStream.Position = 0L;
                return model.Deserialize((Stream) memoryStream, to, (Type) null);
            }
        }
    }
}