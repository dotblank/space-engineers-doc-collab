// Decompiled with JetBrains decompiler
// Type: ProtoBuf.NetObjectCache
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ProtoBuf
{
  internal sealed class NetObjectCache
  {
    internal const int Root = 0;
    private MutableList underlyingList;
    private object rootObject;
    private int trapStartIndex;
    private Dictionary<string, int> stringKeys;
    private Dictionary<object, int> objectKeys;

    private MutableList List
    {
      get
      {
        if (this.underlyingList == null)
          this.underlyingList = new MutableList();
        return this.underlyingList;
      }
    }

    internal object GetKeyedObject(int key)
    {
      if (key-- == 0)
      {
        if (this.rootObject == null)
          throw new ProtoException("No root object assigned");
        else
          return this.rootObject;
      }
      else
      {
        BasicList basicList = (BasicList) this.List;
        if (key < 0 || key >= basicList.Count)
          throw new ProtoException("Internal error; a missing key occurred");
        object obj = basicList[key];
        if (obj == null)
          throw new ProtoException("A deferred key does not have a value yet");
        else
          return obj;
      }
    }

    internal void SetKeyedObject(int key, object value)
    {
      if (key-- == 0)
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (this.rootObject != null && this.rootObject != value)
          throw new ProtoException("The root object cannot be reassigned");
        this.rootObject = value;
      }
      else
      {
        MutableList list = this.List;
        if (key < list.Count)
        {
          object objA = list[key];
          if (objA == null)
            list[key] = value;
          else if (!object.ReferenceEquals(objA, value))
            throw new ProtoException("Reference-tracked objects cannot change reference");
        }
        else if (key != list.Add(value))
          throw new ProtoException("Internal error; a key mismatch occurred");
      }
    }

    internal int AddObjectKey(object value, out bool existing)
    {
        existing = false;
        return 1;
    }

    internal void RegisterTrappedObject(object value)
    {
      if (this.rootObject == null)
      {
        this.rootObject = value;
      }
      else
      {
        if (this.underlyingList == null)
          return;
        for (int index = this.trapStartIndex; index < this.underlyingList.Count; ++index)
        {
          this.trapStartIndex = index + 1;
          if (this.underlyingList[index] == null)
          {
            this.underlyingList[index] = value;
            break;
          }
        }
      }
    }

    private sealed class ReferenceComparer : IEqualityComparer<object>
    {
      public static readonly NetObjectCache.ReferenceComparer Default = new NetObjectCache.ReferenceComparer();

      private ReferenceComparer()
      {
      }

      bool IEqualityComparer<object>.Equals(object x, object y)
      {
        return x == y;
      }

      int IEqualityComparer<object>.GetHashCode(object obj)
      {
        return RuntimeHelpers.GetHashCode(obj);
      }
    }
  }
}
