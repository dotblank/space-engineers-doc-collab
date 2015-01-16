// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.BasicList
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;

namespace ProtoBuf.Meta
{
    internal class BasicList : IEnumerable
    {
        private static readonly BasicList.Node nil = new BasicList.Node((object[]) null, 0);
        protected BasicList.Node head = BasicList.nil;

        public object this[int index]
        {
            get { return this.head[index]; }
        }

        public int Count
        {
            get { return this.head.Length; }
        }

        public void CopyTo(Array array, int offset)
        {
            this.head.CopyTo(array, offset);
        }

        public int Add(object value)
        {
            return (this.head = this.head.Append(value)).Length - 1;
        }

        public object TryGet(int index)
        {
            return this.head.TryGet(index);
        }

        public void Trim()
        {
            this.head = this.head.Trim();
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator) new BasicList.NodeEnumerator(this.head);
        }

        internal int IndexOf(BasicList.IPredicate predicate)
        {
            return this.head.IndexOf(predicate);
        }

        internal int IndexOfReference(object instance)
        {
            return this.head.IndexOfReference(instance);
        }

        internal bool Contains(object value)
        {
            foreach (object objA in this)
            {
                if (object.Equals(objA, value))
                    return true;
            }
            return false;
        }

        internal static BasicList GetContiguousGroups(int[] keys, object[] values)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length < keys.Length)
                throw new ArgumentException("Not all keys are covered by values", "values");
            BasicList basicList = new BasicList();
            BasicList.Group group = (BasicList.Group) null;
            for (int index = 0; index < keys.Length; ++index)
            {
                if (index == 0 || keys[index] != keys[index - 1])
                    group = (BasicList.Group) null;
                if (group == null)
                {
                    group = new BasicList.Group(keys[index]);
                    basicList.Add((object) group);
                }
                group.Items.Add(values[index]);
            }
            return basicList;
        }

        private sealed class NodeEnumerator : IEnumerator
        {
            private int position = -1;
            private readonly BasicList.Node node;

            public object Current
            {
                get { return this.node[this.position]; }
            }

            public NodeEnumerator(BasicList.Node node)
            {
                this.node = node;
            }

            void IEnumerator.Reset()
            {
                this.position = -1;
            }

            public bool MoveNext()
            {
                int length = this.node.Length;
                if (this.position <= length)
                    return ++this.position < length;
                else
                    return false;
            }
        }

        protected sealed class Node
        {
            private readonly object[] data;
            private int length;

            public object this[int index]
            {
                get
                {
                    if (index >= 0 && index < this.length)
                        return this.data[index];
                    else
                        throw new ArgumentOutOfRangeException("index");
                }
                set
                {
                    if (index < 0 || index >= this.length)
                        throw new ArgumentOutOfRangeException("index");
                    this.data[index] = value;
                }
            }

            public int Length
            {
                get { return this.length; }
            }

            internal Node(object[] data, int length)
            {
                this.data = data;
                this.length = length;
            }

            public object TryGet(int index)
            {
                if (index < 0 || index >= this.length)
                    return (object) null;
                else
                    return this.data[index];
            }

            public void RemoveLastWithMutate()
            {
                if (this.length == 0)
                    throw new InvalidOperationException();
                --this.length;
            }

            public BasicList.Node Append(object value)
            {
                int length = this.length + 1;
                object[] data;
                if (this.data == null)
                    data = new object[10];
                else if (this.length == this.data.Length)
                {
                    data = new object[this.data.Length*2];
                    Array.Copy((Array) this.data, (Array) data, this.length);
                }
                else
                    data = this.data;
                data[this.length] = value;
                return new BasicList.Node(data, length);
            }

            public BasicList.Node Trim()
            {
                if (this.length == 0 || this.length == this.data.Length)
                    return this;
                object[] data = new object[this.length];
                Array.Copy((Array) this.data, (Array) data, this.length);
                return new BasicList.Node(data, this.length);
            }

            internal int IndexOfReference(object instance)
            {
                for (int index = 0; index < this.length; ++index)
                {
                    if (instance == this.data[index])
                        return index;
                }
                return -1;
            }

            internal int IndexOf(BasicList.IPredicate predicate)
            {
                for (int index = 0; index < this.length; ++index)
                {
                    if (predicate.IsMatch(this.data[index]))
                        return index;
                }
                return -1;
            }

            internal void CopyTo(Array array, int offset)
            {
                if (this.length <= 0)
                    return;
                Array.Copy((Array) this.data, 0, array, offset, this.length);
            }
        }

        internal interface IPredicate
        {
            bool IsMatch(object obj);
        }

        internal class Group
        {
            public readonly int First;
            public readonly BasicList Items;

            public Group(int first)
            {
                this.First = first;
                this.Items = new BasicList();
            }
        }
    }
}