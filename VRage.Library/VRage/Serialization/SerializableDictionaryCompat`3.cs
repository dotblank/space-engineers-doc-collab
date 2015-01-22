// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.SerializableDictionaryCompat`3
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace VRage.Serialization
{
  [XmlRoot("Dictionary")]
  [ProtoContract]
  [Obfuscation(ApplyToMembers = true, Exclude = true, Feature = "cw symbol renaming")]
  public class SerializableDictionaryCompat<T, U, V> : SerializableDictionary<T, U> where U : IAssignableFrom<V>, new()
  {
    [XmlArrayItem("item", Type = typeof (DictionaryEntry))]
    [XmlArray("dictionary")]
    public new DictionaryEntry[] DictionaryEntryProp
    {
      get
      {
        DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[this.Dictionary.Count];
        int index = 0;
        foreach (KeyValuePair<T, U> keyValuePair in this.Dictionary)
        {
          dictionaryEntryArray[index] = new DictionaryEntry()
          {
            Key = (object) keyValuePair.Key,
            Value = (object) keyValuePair.Value
          };
          ++index;
        }
        return dictionaryEntryArray;
      }
      set
      {
        this.Dictionary.Clear();
        for (int index = 0; index < value.Length; ++index)
        {
          try
          {
            if (value[index].Value.GetType() == typeof (U))
            {
              this.Dictionary.Add((T) value[index].Key, (U) value[index].Value);
            }
            else
            {
              U u = new U();
              u.AssignFrom((V) value[index].Value);
              this.Dictionary.Add((T) value[index].Key, u);
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
    }
  }
}
