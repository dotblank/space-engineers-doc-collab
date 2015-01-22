// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.BlitSerializer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using VRage;
using VRage.Utils;

namespace VRage.Serialization
{
  public class BlitSerializer<T> : ISerializer<T>
  {
    private static BlitSerializer<T>.Reader m_reader = BlitSerializer<T>.GenerateReader();
    private static BlitSerializer<T>.Writer m_writer = BlitSerializer<T>.GenerateWriter();
    public static int StructSize = (int) BlitSerializer<T>.GenerateSize()();
    public static readonly BlitSerializer<T> Default = new BlitSerializer<T>();

    public BlitSerializer()
    {
      MyUtils.ThrowNonBlittable<T>();
    }

    public unsafe void Serialize(ByteStream destination, ref T data)
    {
      destination.EnsureCapacity(destination.Position + (long) BlitSerializer<T>.StructSize);
      fixed (byte* buffer = &destination.Data[destination.Position])
        BlitSerializer<T>.m_writer(ref data, buffer);
      destination.Position += (long) BlitSerializer<T>.StructSize;
    }

    public unsafe void Deserialize(ByteStream source, out T data)
    {
      source.CheckCapacity(source.Position + (long) BlitSerializer<T>.StructSize);
      fixed (byte* buffer = &source.Data[source.Position])
        BlitSerializer<T>.m_reader(out data, buffer);
      source.Position += (long) BlitSerializer<T>.StructSize;
    }

    public void SerializeList(ByteStream destination, List<T> data)
    {
      int count = data.Count;
      StreamExtensions.Write7BitEncodedInt((Stream) destination, count);
      for (int index = 0; index < count; ++index)
      {
        T[] internalArray = ListExtensions.GetInternalArray<T>(data);
        this.Serialize(destination, ref internalArray[index]);
      }
    }

    public void DeserializeList(ByteStream source, List<T> resultList)
    {
      int num = StreamExtensions.Read7BitEncodedInt((Stream) source);
      if (resultList.Capacity < num)
        resultList.Capacity = num;
      for (int index = 0; index < num; ++index)
      {
        T data;
        this.Deserialize(source, out data);
        resultList.Add(data);
      }
    }

    private static BlitSerializer<T>.Size GenerateSize()
    {
      DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof (uint), new Type[0], Assembly.GetExecutingAssembly().ManifestModule);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Sizeof, typeof (T));
      ilGenerator.Emit(OpCodes.Ret);
      return (BlitSerializer<T>.Size) dynamicMethod.CreateDelegate(typeof (BlitSerializer<T>.Size));
    }

    private static BlitSerializer<T>.Writer GenerateWriter()
    {
      DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, (Type) null, new Type[2]
      {
        typeof (T).MakeByRefType(),
        typeof (byte*)
      }, Assembly.GetExecutingAssembly().ManifestModule);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Sizeof, typeof (T));
      ilGenerator.Emit(OpCodes.Cpblk);
      ilGenerator.Emit(OpCodes.Ret);
      return (BlitSerializer<T>.Writer) dynamicMethod.CreateDelegate(typeof (BlitSerializer<T>.Writer));
    }

    private static BlitSerializer<T>.Reader GenerateReader()
    {
      DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, (Type) null, new Type[2]
      {
        typeof (T).MakeByRefType(),
        typeof (byte*)
      }, Assembly.GetExecutingAssembly().ManifestModule);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(OpCodes.Sizeof, typeof (T));
      ilGenerator.Emit(OpCodes.Cpblk);
      ilGenerator.Emit(OpCodes.Ret);
      return (BlitSerializer<T>.Reader) dynamicMethod.CreateDelegate(typeof (BlitSerializer<T>.Reader));
    }

    private unsafe delegate void Writer(ref T data, byte* buffer);

    private unsafe delegate void Reader(out T data, byte* buffer);

    private delegate uint Size();
  }
}
