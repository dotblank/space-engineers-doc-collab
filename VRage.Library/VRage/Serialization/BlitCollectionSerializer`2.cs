// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.BlitCollectionSerializer`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using VRage;

namespace VRage.Serialization
{
    public class BlitCollectionSerializer<T, TData> : ISerializer<T> where T : ICollection<TData>, new()
    {
        public static readonly BlitCollectionSerializer<T, TData> Default = new BlitCollectionSerializer<T, TData>();
        public static readonly BlitSerializer<TData> InnerSerializer = BlitSerializer<TData>.Default;

        public void Serialize(ByteStream destination, ref T data)
        {
            StreamExtensions.Write7BitEncodedInt((Stream) destination, data.Count);
            using (IEnumerator<TData> enumerator = data.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    TData current = enumerator.Current;
                    BlitCollectionSerializer<T, TData>.InnerSerializer.Serialize(destination, ref current);
                }
            }
        }

        public void Deserialize(ByteStream source, out T data)
        {
            data = new T();
            int num = StreamExtensions.Read7BitEncodedInt((Stream) source);
            for (int index = 0; index < num; ++index)
            {
                TData data1;
                BlitCollectionSerializer<T, TData>.InnerSerializer.Deserialize(source, out data1);
                data.Add(data1);
            }
        }
    }
}