// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.SerializableDictionary`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F987C912-6032-4943-850E-69DEE0217B30
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
  public class SerializableDictionary<T, U>
  {
    [ProtoMember(1)]
    private Dictionary<T, U> m_dictionary = new Dictionary<T, U>();

    [XmlIgnore]
    public Dictionary<T, U> Dictionary
    {
      get
      {
        return this.m_dictionary;
      }
      set
      {
        this.m_dictionary = value;
      }
    }

    [XmlArrayItem("item", Type = typeof (DictionaryEntry))]
    [XmlArray("dictionary")]
    public DictionaryEntry[] DictionaryEntryProp
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
            this.Dictionary.Add((T) value[index].Key, (U) value[index].Value);
          }
          catch (Exception ex)
          {
          }
        }
      }
    }

    public U this[T key]
    {
      get
      {
        return this.Dictionary[key];
      }
      set
      {
        this.Dictionary[key] = value;
      }
    }

    public SerializableDictionary()
    {
    }

    public SerializableDictionary(Dictionary<T, U> dict)
    {
      this.Dictionary = dict;
    }
  }
}
