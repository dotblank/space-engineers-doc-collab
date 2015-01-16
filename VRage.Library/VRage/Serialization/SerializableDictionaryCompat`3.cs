// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.SerializableDictionaryCompat`3
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace VRage.Serialization
{
    [Obfuscation(ApplyToMembers = true, Exclude = true, Feature = "cw symbol renaming")]
    [ProtoContract]
    [XmlRoot("Dictionary")]
    public class SerializableDictionaryCompat<T, U, V> : SerializableDictionary<T, U>
        where U : IAssignableFrom<V>, new()
    {
        [XmlArray("dictionary")]
        [XmlArrayItem("item", Type = typeof (DictionaryEntry))]
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