// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyComponentContainer
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;
using System;
using System.Collections.Generic;

namespace Sandbox.Common.Components
{
  public class MyComponentContainer
  {
    private Dictionary<Type, MyComponentBase> m_components = new Dictionary<Type, MyComponentBase>();

    public IMyEntity Entity { get; private set; }

    public event Action<Type, MyComponentBase> ComponentAdded;

    public event Action<Type, MyComponentBase> ComponentRemoved;

    public MyComponentContainer(IMyEntity entity)
    {
      this.Entity = entity;
    }

    public void Add<T>(MyComponentBase component) where T : MyComponentBase
    {
      Type index = typeof (T);
      this.Remove<T>();
      if (component == null)
        return;
      this.m_components[index] = component;
      component.OnAddedToContainer(this);
      Action<Type, MyComponentBase> action = this.ComponentAdded;
      if (action == null)
        return;
      action(index, component);
    }

    public void Remove<T>() where T : MyComponentBase
    {
      Type key = typeof (T);
      MyComponentBase myComponentBase;
      if (!this.m_components.TryGetValue(key, out myComponentBase))
        return;
      myComponentBase.OnRemovedFromContainer(this);
      this.m_components.Remove(key);
      Action<Type, MyComponentBase> action = this.ComponentRemoved;
      if (action == null)
        return;
      action(key, myComponentBase);
    }

    public T Get<T>() where T : MyComponentBase
    {
      MyComponentBase myComponentBase;
      this.m_components.TryGetValue(typeof (T), out myComponentBase);
      return myComponentBase as T;
    }

    public bool TryGet<T>(out T component) where T : MyComponentBase
    {
      MyComponentBase myComponentBase;
      bool flag = this.m_components.TryGetValue(typeof (T), out myComponentBase);
      component = myComponentBase as T;
      return flag;
    }

    public bool Contains(Type type)
    {
      foreach (Type c in this.m_components.Keys)
      {
        if (type.IsAssignableFrom(c))
          return true;
      }
      return false;
    }
  }
}
