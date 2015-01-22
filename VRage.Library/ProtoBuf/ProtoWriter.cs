// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoWriter
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.IO;
using System.Text;

namespace ProtoBuf
{
  public sealed class ProtoWriter : IDisposable
  {
    private static readonly UTF8Encoding encoding = new UTF8Encoding();
    private readonly NetObjectCache netCache = new NetObjectCache();
    private const int RecursionCheckDepth = 25;
    private Stream dest;
    private TypeModel model;
    private int fieldNumber;
    private int flushLock;
    private WireType wireType;
    private int depth;
    private MutableList recursionStack;
    private readonly SerializationContext context;
    private byte[] ioBuffer;
    private int ioIndex;
    private int position;
    private int packedFieldNumber;

    internal NetObjectCache NetCache
    {
      get
      {
        return this.netCache;
      }
    }

    internal WireType WireType
    {
      get
      {
        return this.wireType;
      }
    }

    public SerializationContext Context
    {
      get
      {
        return this.context;
      }
    }

    public TypeModel Model
    {
      get
      {
        return this.model;
      }
    }

    public ProtoWriter(Stream dest, TypeModel model, SerializationContext context)
    {
      if (dest == null)
        throw new ArgumentNullException("dest");
      if (!dest.CanWrite)
        throw new ArgumentException("Cannot write to stream", "dest");
      this.dest = dest;
      this.ioBuffer = BufferPool.GetBuffer();
      this.model = model;
      this.wireType = WireType.None;
      if (context == null)
        context = SerializationContext.Default;
      else
        context.Freeze();
      this.context = context;
    }

    public static void WriteObject(object value, int key, ProtoWriter writer)
    {
      if (writer.model == null)
        throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
      SubItemToken token = ProtoWriter.StartSubItem(value, writer);
      if (key >= 0)
        writer.model.Serialize(key, value, writer);
      else if (writer.model == null || !writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
        TypeModel.ThrowUnexpectedType(value.GetType());
      ProtoWriter.EndSubItem(token, writer);
    }

    public static void WriteRecursionSafeObject(object value, int key, ProtoWriter writer)
    {
      if (writer.model == null)
        throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
      SubItemToken token = ProtoWriter.StartSubItem((object) null, writer);
      writer.model.Serialize(key, value, writer);
      ProtoWriter.EndSubItem(token, writer);
    }

    internal static void WriteObject(object value, int key, ProtoWriter writer, PrefixStyle style, int fieldNumber)
    {
      if (writer.model == null)
        throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
      if (writer.wireType != WireType.None)
        throw ProtoWriter.CreateException(writer);
      switch (style)
      {
        case PrefixStyle.Base128:
          writer.wireType = WireType.String;
          writer.fieldNumber = fieldNumber;
          if (fieldNumber > 0)
          {
            ProtoWriter.WriteHeaderCore(fieldNumber, WireType.String, writer);
            break;
          }
          else
            break;
        case PrefixStyle.Fixed32:
        case PrefixStyle.Fixed32BigEndian:
          writer.fieldNumber = 0;
          writer.wireType = WireType.Fixed32;
          break;
        default:
          throw new ArgumentOutOfRangeException("style");
      }
      SubItemToken token = ProtoWriter.StartSubItem(value, writer, true);
      if (key < 0)
      {
        if (!writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
          TypeModel.ThrowUnexpectedType(value.GetType());
      }
      else
        writer.model.Serialize(key, value, writer);
      ProtoWriter.EndSubItem(token, writer, style);
    }

    internal int GetTypeKey(ref Type type)
    {
      return this.model.GetKey(ref type);
    }

    public static void WriteFieldHeader(int fieldNumber, WireType wireType, ProtoWriter writer)
    {
      if (writer.wireType != WireType.None)
      {
        throw new InvalidOperationException("Cannot write a " + (object) wireType + " header until the " + (string) (object) writer.wireType + " data has been written");
      }
      else
      {
        if (fieldNumber < 0)
          throw new ArgumentOutOfRangeException("fieldNumber");
        if (writer.packedFieldNumber == 0)
        {
          writer.fieldNumber = fieldNumber;
          writer.wireType = wireType;
          ProtoWriter.WriteHeaderCore(fieldNumber, wireType, writer);
        }
        else if (writer.packedFieldNumber == fieldNumber)
        {
          switch (wireType)
          {
            case WireType.Variant:
            case WireType.Fixed64:
            case WireType.Fixed32:
            case WireType.SignedVariant:
              writer.fieldNumber = fieldNumber;
              writer.wireType = wireType;
              break;
            default:
              throw new InvalidOperationException("Wire-type cannot be encoded as packed: " + (object) wireType);
          }
        }
        else
          throw new InvalidOperationException(string.Concat(new object[4]
          {
            (object) "Field mismatch during packed encoding; expected ",
            (object) writer.packedFieldNumber,
            (object) " but received ",
            (object) fieldNumber
          }));
      }
    }

    internal static void WriteHeaderCore(int fieldNumber, WireType wireType, ProtoWriter writer)
    {
      ProtoWriter.WriteUInt32Variant((uint) ((WireType) (fieldNumber << 3) | wireType & (WireType) 7), writer);
    }

    public static void WriteBytes(byte[] data, ProtoWriter writer)
    {
      ProtoWriter.WriteBytes(data, 0, data.Length, writer);
    }

    public static void WriteBytes(byte[] data, int offset, int length, ProtoWriter writer)
    {
      if (data == null)
        throw new ArgumentNullException("blob");
      switch (writer.wireType)
      {
        case WireType.Fixed64:
          if (length != 8)
            throw new ArgumentException("length");
          else
            break;
        case WireType.String:
          ProtoWriter.WriteUInt32Variant((uint) length, writer);
          writer.wireType = WireType.None;
          if (length == 0)
            return;
          if (writer.flushLock == 0 && length > writer.ioBuffer.Length)
          {
            ProtoWriter.Flush(writer);
            writer.dest.Write(data, offset, length);
            writer.position += length;
            return;
          }
          else
            break;
        case WireType.Fixed32:
          if (length != 4)
            throw new ArgumentException("length");
          else
            break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
      ProtoWriter.DemandSpace(length, writer);
      Helpers.BlockCopy(data, offset, writer.ioBuffer, writer.ioIndex, length);
      ProtoWriter.IncrementedAndReset(length, writer);
    }

    private static void CopyRawFromStream(Stream source, ProtoWriter writer)
    {
      byte[] buffer = writer.ioBuffer;
      int count1 = buffer.Length - writer.ioIndex;
      int num1 = 1;
      while (count1 > 0 && (num1 = source.Read(buffer, writer.ioIndex, count1)) > 0)
      {
        writer.ioIndex += num1;
        writer.position += num1;
        count1 -= num1;
      }
      if (num1 <= 0)
        return;
      if (writer.flushLock == 0)
      {
        ProtoWriter.Flush(writer);
        int count2;
        while ((count2 = source.Read(buffer, 0, buffer.Length)) > 0)
        {
          writer.dest.Write(buffer, 0, count2);
          writer.position += count2;
        }
      }
      else
      {
        while (true)
        {
          ProtoWriter.DemandSpace(128, writer);
          int num2;
          if ((num2 = source.Read(writer.ioBuffer, writer.ioIndex, writer.ioBuffer.Length - writer.ioIndex)) > 0)
          {
            writer.position += num2;
            writer.ioIndex += num2;
          }
          else
            break;
        }
      }
    }

    private static void IncrementedAndReset(int length, ProtoWriter writer)
    {
      writer.ioIndex += length;
      writer.position += length;
      writer.wireType = WireType.None;
    }

    public static SubItemToken StartSubItem(object instance, ProtoWriter writer)
    {
      return ProtoWriter.StartSubItem(instance, writer, false);
    }

    private void CheckRecursionStackAndPush(object instance)
    {
      if (this.recursionStack == null)
      {
        this.recursionStack = new MutableList();
      }
      else
      {
        int num;
        if (instance != null && (num = this.recursionStack.IndexOfReference(instance)) >= 0)
          throw new ProtoException("Possible recursion detected (offset: " + (this.recursionStack.Count - num).ToString() + " level(s)): " + instance.ToString());
      }
      this.recursionStack.Add(instance);
    }

    private void PopRecursionStack()
    {
      this.recursionStack.RemoveLast();
    }

    private static SubItemToken StartSubItem(object instance, ProtoWriter writer, bool allowFixed)
    {
      if (++writer.depth > 25)
        writer.CheckRecursionStackAndPush(instance);
      if (writer.packedFieldNumber != 0)
        throw new InvalidOperationException("Cannot begin a sub-item while performing packed encoding");
      switch (writer.wireType)
      {
        case WireType.String:
          writer.wireType = WireType.None;
          ProtoWriter.DemandSpace(32, writer);
          ++writer.flushLock;
          ++writer.position;
          return new SubItemToken(writer.ioIndex++);
        case WireType.StartGroup:
          writer.wireType = WireType.None;
          return new SubItemToken(-writer.fieldNumber);
        case WireType.Fixed32:
          if (!allowFixed)
            throw ProtoWriter.CreateException(writer);
          ProtoWriter.DemandSpace(32, writer);
          ++writer.flushLock;
          SubItemToken subItemToken = new SubItemToken(writer.ioIndex);
          ProtoWriter.IncrementedAndReset(4, writer);
          return subItemToken;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static void EndSubItem(SubItemToken token, ProtoWriter writer)
    {
      ProtoWriter.EndSubItem(token, writer, PrefixStyle.Base128);
    }

    private static void EndSubItem(SubItemToken token, ProtoWriter writer, PrefixStyle style)
    {
      if (writer.wireType != WireType.None)
        throw ProtoWriter.CreateException(writer);
      int index = token.value;
      if (writer.depth <= 0)
        throw ProtoWriter.CreateException(writer);
      if (writer.depth-- > 25)
        writer.PopRecursionStack();
      writer.packedFieldNumber = 0;
      if (index < 0)
      {
        ProtoWriter.WriteHeaderCore(-index, WireType.EndGroup, writer);
        writer.wireType = WireType.None;
      }
      else
      {
        switch (style)
        {
          case PrefixStyle.Base128:
            int count = writer.ioIndex - index - 1;
            int required = 0;
            uint num1 = (uint) count;
            while ((int) (num1 >>= 7) != 0)
              ++required;
            if (required == 0)
            {
              writer.ioBuffer[index] = (byte) (count & (int) sbyte.MaxValue);
              break;
            }
            else
            {
              ProtoWriter.DemandSpace(required, writer);
              byte[] numArray = writer.ioBuffer;
              Helpers.BlockCopy(numArray, index + 1, numArray, index + 1 + required, count);
              uint num2 = (uint) count;
              do
              {
                numArray[index++] = (byte) ((int) num2 & (int) sbyte.MaxValue | 128);
              }
              while ((int) (num2 >>= 7) != 0);
              numArray[index - 1] = (byte) ((uint) numArray[index - 1] & 4294967167U);
              writer.position += required;
              writer.ioIndex += required;
              break;
            }
          case PrefixStyle.Fixed32:
            ProtoWriter.WriteInt32ToBuffer(writer.ioIndex - index - 4, writer.ioBuffer, index);
            break;
          case PrefixStyle.Fixed32BigEndian:
            int num3 = writer.ioIndex - index - 4;
            byte[] buffer = writer.ioBuffer;
            ProtoWriter.WriteInt32ToBuffer(num3, buffer, index);
            byte num4 = buffer[index];
            buffer[index] = buffer[index + 3];
            buffer[index + 3] = num4;
            byte num5 = buffer[index + 1];
            buffer[index + 1] = buffer[index + 2];
            buffer[index + 2] = num5;
            break;
          default:
            throw new ArgumentOutOfRangeException("style");
        }
        if (--writer.flushLock != 0 || writer.ioIndex < 1024)
          return;
        ProtoWriter.Flush(writer);
      }
    }

    void IDisposable.Dispose()
    {
      this.Dispose();
    }

    private void Dispose()
    {
      if (this.dest != null)
      {
        ProtoWriter.Flush(this);
        this.dest = (Stream) null;
      }
      this.model = (TypeModel) null;
      BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
    }

    internal static int GetPosition(ProtoWriter writer)
    {
      return writer.position;
    }

    private static void DemandSpace(int required, ProtoWriter writer)
    {
      if (writer.ioBuffer.Length - writer.ioIndex >= required)
        return;
      if (writer.flushLock == 0)
      {
        ProtoWriter.Flush(writer);
        if (writer.ioBuffer.Length - writer.ioIndex >= required)
          return;
      }
      BufferPool.ResizeAndFlushLeft(ref writer.ioBuffer, required + writer.ioIndex, 0, writer.ioIndex);
    }

    public void Close()
    {
      if (this.depth != 0 || this.flushLock != 0)
        throw new InvalidOperationException("Unable to close stream in an incomplete state");
      this.Dispose();
    }

    internal void CheckDepthFlushlock()
    {
      if (this.depth != 0 || this.flushLock != 0)
        throw new InvalidOperationException("The writer is in an incomplete state");
    }

    internal static void Flush(ProtoWriter writer)
    {
      if (writer.flushLock != 0 || writer.ioIndex == 0)
        return;
      writer.dest.Write(writer.ioBuffer, 0, writer.ioIndex);
      writer.ioIndex = 0;
    }

    private static void WriteUInt32Variant(uint value, ProtoWriter writer)
    {
      ProtoWriter.DemandSpace(5, writer);
      int num = 0;
      do
      {
        writer.ioBuffer[writer.ioIndex++] = (byte) ((int) value & (int) sbyte.MaxValue | 128);
        ++num;
      }
      while ((int) (value >>= 7) != 0);
      writer.ioBuffer[writer.ioIndex - 1] &= (byte) 127;
      writer.position += num;
    }

    internal static uint Zig(int value)
    {
      return (uint) (value << 1 ^ value >> 31);
    }

    internal static ulong Zig(long value)
    {
      return (ulong) (value << 1 ^ value >> 63);
    }

    private static void WriteUInt64Variant(ulong value, ProtoWriter writer)
    {
      ProtoWriter.DemandSpace(10, writer);
      int num = 0;
      do
      {
        writer.ioBuffer[writer.ioIndex++] = (byte) (value & (ulong) sbyte.MaxValue | 128UL);
        ++num;
      }
      while ((long) (value >>= 7) != 0L);
      writer.ioBuffer[writer.ioIndex - 1] &= (byte) 127;
      writer.position += num;
    }

    public static void WriteString(string value, ProtoWriter writer)
    {
      if (writer.wireType != WireType.String)
        throw ProtoWriter.CreateException(writer);
      if (value == null)
        throw new ArgumentNullException("value");
      if (value.Length == 0)
      {
        ProtoWriter.WriteUInt32Variant(0U, writer);
        writer.wireType = WireType.None;
      }
      else
      {
        int byteCount = ProtoWriter.encoding.GetByteCount(value);
        ProtoWriter.WriteUInt32Variant((uint) byteCount, writer);
        ProtoWriter.DemandSpace(byteCount, writer);
        ProtoWriter.IncrementedAndReset(ProtoWriter.encoding.GetBytes(value, 0, value.Length, writer.ioBuffer, writer.ioIndex), writer);
      }
    }

    public static void WriteUInt64(ulong value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Variant:
          ProtoWriter.WriteUInt64Variant(value, writer);
          writer.wireType = WireType.None;
          break;
        case WireType.Fixed64:
          ProtoWriter.WriteInt64((long) value, writer);
          break;
        case WireType.Fixed32:
          ProtoWriter.WriteUInt32(checked ((uint) value), writer);
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static void WriteInt64(long value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Variant:
          if (value >= 0L)
          {
            ProtoWriter.WriteUInt64Variant((ulong) value, writer);
            writer.wireType = WireType.None;
            break;
          }
          else
          {
            ProtoWriter.DemandSpace(10, writer);
            byte[] numArray = writer.ioBuffer;
            int index = writer.ioIndex;
            numArray[index] = (byte) ((ulong) value | 128UL);
            numArray[index + 1] = (byte) ((uint) (int) (value >> 7) | 128U);
            numArray[index + 2] = (byte) ((uint) (int) (value >> 14) | 128U);
            numArray[index + 3] = (byte) ((uint) (int) (value >> 21) | 128U);
            numArray[index + 4] = (byte) ((uint) (int) (value >> 28) | 128U);
            numArray[index + 5] = (byte) ((uint) (int) (value >> 35) | 128U);
            numArray[index + 6] = (byte) ((uint) (int) (value >> 42) | 128U);
            numArray[index + 7] = (byte) ((uint) (int) (value >> 49) | 128U);
            numArray[index + 8] = (byte) ((uint) (int) (value >> 56) | 128U);
            numArray[index + 9] = (byte) 1;
            ProtoWriter.IncrementedAndReset(10, writer);
            break;
          }
        case WireType.Fixed64:
          ProtoWriter.DemandSpace(8, writer);
          byte[] numArray1 = writer.ioBuffer;
          int index1 = writer.ioIndex;
          numArray1[index1] = (byte) value;
          numArray1[index1 + 1] = (byte) (value >> 8);
          numArray1[index1 + 2] = (byte) (value >> 16);
          numArray1[index1 + 3] = (byte) (value >> 24);
          numArray1[index1 + 4] = (byte) (value >> 32);
          numArray1[index1 + 5] = (byte) (value >> 40);
          numArray1[index1 + 6] = (byte) (value >> 48);
          numArray1[index1 + 7] = (byte) (value >> 56);
          ProtoWriter.IncrementedAndReset(8, writer);
          break;
        case WireType.Fixed32:
          ProtoWriter.WriteInt32(checked ((int) value), writer);
          break;
        case WireType.SignedVariant:
          ProtoWriter.WriteUInt64Variant(ProtoWriter.Zig(value), writer);
          writer.wireType = WireType.None;
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static void WriteUInt32(uint value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Variant:
          ProtoWriter.WriteUInt32Variant(value, writer);
          writer.wireType = WireType.None;
          break;
        case WireType.Fixed64:
          ProtoWriter.WriteInt64((long) (int) value, writer);
          break;
        case WireType.Fixed32:
          ProtoWriter.WriteInt32((int) value, writer);
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static void WriteInt16(short value, ProtoWriter writer)
    {
      ProtoWriter.WriteInt32((int) value, writer);
    }

    public static void WriteUInt16(ushort value, ProtoWriter writer)
    {
      ProtoWriter.WriteUInt32((uint) value, writer);
    }

    public static void WriteByte(byte value, ProtoWriter writer)
    {
      ProtoWriter.WriteUInt32((uint) value, writer);
    }

    public static void WriteSByte(sbyte value, ProtoWriter writer)
    {
      ProtoWriter.WriteInt32((int) value, writer);
    }

    private static void WriteInt32ToBuffer(int value, byte[] buffer, int index)
    {
      buffer[index] = (byte) value;
      buffer[index + 1] = (byte) (value >> 8);
      buffer[index + 2] = (byte) (value >> 16);
      buffer[index + 3] = (byte) (value >> 24);
    }

    public static void WriteInt32(int value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Variant:
          if (value >= 0)
          {
            ProtoWriter.WriteUInt32Variant((uint) value, writer);
            writer.wireType = WireType.None;
            break;
          }
          else
          {
            ProtoWriter.DemandSpace(10, writer);
            byte[] numArray = writer.ioBuffer;
            int index = writer.ioIndex;
            numArray[index] = (byte) (value | 128);
            numArray[index + 1] = (byte) (value >> 7 | 128);
            numArray[index + 2] = (byte) (value >> 14 | 128);
            numArray[index + 3] = (byte) (value >> 21 | 128);
            numArray[index + 4] = (byte) (value >> 28 | 128);
            numArray[index + 5] = numArray[index + 6] = numArray[index + 7] = numArray[index + 8] = byte.MaxValue;
            numArray[index + 9] = (byte) 1;
            ProtoWriter.IncrementedAndReset(10, writer);
            break;
          }
        case WireType.Fixed64:
          ProtoWriter.DemandSpace(8, writer);
          byte[] numArray1 = writer.ioBuffer;
          int index1 = writer.ioIndex;
          numArray1[index1] = (byte) value;
          numArray1[index1 + 1] = (byte) (value >> 8);
          numArray1[index1 + 2] = (byte) (value >> 16);
          numArray1[index1 + 3] = (byte) (value >> 24);
          numArray1[index1 + 4] = numArray1[index1 + 5] = numArray1[index1 + 6] = numArray1[index1 + 7] = (byte) 0;
          ProtoWriter.IncrementedAndReset(8, writer);
          break;
        case WireType.Fixed32:
          ProtoWriter.DemandSpace(4, writer);
          ProtoWriter.WriteInt32ToBuffer(value, writer.ioBuffer, writer.ioIndex);
          ProtoWriter.IncrementedAndReset(4, writer);
          break;
        case WireType.SignedVariant:
          ProtoWriter.WriteUInt32Variant(ProtoWriter.Zig(value), writer);
          writer.wireType = WireType.None;
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static unsafe void WriteDouble(double value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Fixed64:
          ProtoWriter.WriteInt64(*(long*) &value, writer);
          break;
        case WireType.Fixed32:
          float num = (float) value;
          if (Helpers.IsInfinity(num) && !Helpers.IsInfinity(value))
            throw new OverflowException();
          ProtoWriter.WriteSingle(num, writer);
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static unsafe void WriteSingle(float value, ProtoWriter writer)
    {
      switch (writer.wireType)
      {
        case WireType.Fixed64:
          ProtoWriter.WriteDouble((double) value, writer);
          break;
        case WireType.Fixed32:
          ProtoWriter.WriteInt32(*(int*) &value, writer);
          break;
        default:
          throw ProtoWriter.CreateException(writer);
      }
    }

    public static void ThrowEnumException(ProtoWriter writer, object enumValue)
    {
      throw new ProtoException("No wire-value is mapped to the enum " + (enumValue == null ? "<null>" : enumValue.GetType().FullName + "." + enumValue.ToString()));
    }

    internal static Exception CreateException(ProtoWriter writer)
    {
      return (Exception) new ProtoException(string.Concat(new object[4]
      {
        (object) "Invalid serialization operation with wire-type ",
        (object) writer.wireType,
        (object) " at position ",
        (object) writer.position
      }));
    }

    public static void WriteBoolean(bool value, ProtoWriter writer)
    {
      ProtoWriter.WriteUInt32(value ? 1U : 0U, writer);
    }

    public static void AppendExtensionData(IExtensible instance, ProtoWriter writer)
    {
      if (instance == null)
        throw new ArgumentNullException("instance");
      if (writer.wireType != WireType.None)
        throw ProtoWriter.CreateException(writer);
      IExtension extensionObject = instance.GetExtensionObject(false);
      if (extensionObject == null)
        return;
      Stream stream = extensionObject.BeginQuery();
      try
      {
        ProtoWriter.CopyRawFromStream(stream, writer);
      }
      finally
      {
        extensionObject.EndQuery(stream);
      }
    }

    public static void SetPackedField(int fieldNumber, ProtoWriter writer)
    {
      if (fieldNumber <= 0)
        throw new ArgumentOutOfRangeException("fieldNumber");
      writer.packedFieldNumber = fieldNumber;
    }

    internal string SerializeType(Type type)
    {
      return TypeModel.SerializeType(this.model, type);
    }

    public void SetRootObject(object value)
    {
      this.NetCache.SetKeyedObject(0, value);
    }

    public static void WriteType(Type value, ProtoWriter writer)
    {
      ProtoWriter.WriteString(writer.SerializeType(value), writer);
    }
  }
}
