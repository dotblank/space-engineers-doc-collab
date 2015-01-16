// Decompiled with JetBrains decompiler
// Type: LitJson.OrderedDictionaryEnumerator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
    internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
        private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;

        public object Current
        {
            get { return (object) this.Entry; }
        }

        public DictionaryEntry Entry
        {
            get
            {
                KeyValuePair<string, JsonData> current = this.list_enumerator.Current;
                return new DictionaryEntry((object) current.Key, (object) current.Value);
            }
        }

        public object Key
        {
            get { return (object) this.list_enumerator.Current.Key; }
        }

        public object Value
        {
            get { return (object) this.list_enumerator.Current.Value; }
        }

        public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
        {
            this.list_enumerator = enumerator;
        }

        public bool MoveNext()
        {
            return this.list_enumerator.MoveNext();
        }

        public void Reset()
        {
            this.list_enumerator.Reset();
        }
    }
}