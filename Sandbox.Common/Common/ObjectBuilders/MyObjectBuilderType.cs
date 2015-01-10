// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilderType
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;
using System.Reflection;
using VRage.Common.Plugins;
using VRage.Reflection;

namespace Sandbox.Common.ObjectBuilders
{
  public struct MyObjectBuilderType
  {
    public static readonly MyObjectBuilderType Invalid = new MyObjectBuilderType((Type) null);
    public static readonly MyObjectBuilderType.ComparerType Comparer = new MyObjectBuilderType.ComparerType();
    private static Dictionary<string, MyObjectBuilderType> m_typeByName = new Dictionary<string, MyObjectBuilderType>(200);
    private static Dictionary<string, MyObjectBuilderType> m_typeByLegacyName = new Dictionary<string, MyObjectBuilderType>(200);
    private static Dictionary<MyRuntimeObjectBuilderId, MyObjectBuilderType> m_typeById = new Dictionary<MyRuntimeObjectBuilderId, MyObjectBuilderType>(200, (IEqualityComparer<MyRuntimeObjectBuilderId>) MyRuntimeObjectBuilderId.Comparer);
    private static Dictionary<MyObjectBuilderType, MyRuntimeObjectBuilderId> m_idByType = new Dictionary<MyObjectBuilderType, MyRuntimeObjectBuilderId>(200, (IEqualityComparer<MyObjectBuilderType>) MyObjectBuilderType.Comparer);
    private readonly Type m_type;
    private static ushort m_idCounter;

    public bool IsNull
    {
      get
      {
        return this.m_type == (Type) null;
      }
    }

    static MyObjectBuilderType()
    {
      MyObjectBuilderType.RegisterFromAssembly(Assembly.GetExecutingAssembly(), true);
      MyObjectBuilderType.RegisterLegacyName((MyObjectBuilderType) typeof (MyObjectBuilder_GlobalEventDefinition), "EventDefinition");
      MyObjectBuilderType.RegisterLegacyName((MyObjectBuilderType) typeof (MyObjectBuilder_FactionCollection), "Factions");
      MyObjectBuilderType.RegisterFromAssembly(MyPlugins.GameAssembly, true);
      MyObjectBuilderType.RegisterFromAssembly(MyPlugins.UserAssembly, true);
    }

    public MyObjectBuilderType(Type type)
    {
      this.m_type = type;
    }

    public static implicit operator MyObjectBuilderType(Type t)
    {
      return new MyObjectBuilderType(t);
    }

    public static implicit operator Type(MyObjectBuilderType t)
    {
      return t.m_type;
    }

    public static explicit operator MyRuntimeObjectBuilderId(MyObjectBuilderType t)
    {
      return MyObjectBuilderType.m_idByType[t];
    }

    public static explicit operator MyObjectBuilderType(MyRuntimeObjectBuilderId id)
    {
      return MyObjectBuilderType.m_typeById[id];
    }

    public static bool operator ==(MyObjectBuilderType lhs, MyObjectBuilderType rhs)
    {
      return lhs.m_type == rhs.m_type;
    }

    public static bool operator !=(MyObjectBuilderType lhs, MyObjectBuilderType rhs)
    {
      return lhs.m_type != rhs.m_type;
    }

    public static bool operator ==(Type lhs, MyObjectBuilderType rhs)
    {
      return lhs == rhs.m_type;
    }

    public static bool operator !=(Type lhs, MyObjectBuilderType rhs)
    {
      return lhs != rhs.m_type;
    }

    public static bool operator ==(MyObjectBuilderType lhs, Type rhs)
    {
      return lhs.m_type == rhs;
    }

    public static bool operator !=(MyObjectBuilderType lhs, Type rhs)
    {
      return lhs.m_type != rhs;
    }

    public override bool Equals(object obj)
    {
      if (obj != null && obj is MyObjectBuilderType)
        return this.Equals((MyObjectBuilderType) obj);
      else
        return false;
    }

    public bool Equals(MyObjectBuilderType type)
    {
      return type.m_type == this.m_type;
    }

    public override int GetHashCode()
    {
      return this.m_type.GetHashCode();
    }

    public override string ToString()
    {
      return this.m_type.Name;
    }

    public static MyObjectBuilderType Parse(string value)
    {
      return MyObjectBuilderType.m_typeByName[value];
    }

    public static MyObjectBuilderType ParseBackwardsCompatible(string value)
    {
      MyObjectBuilderType objectBuilderType;
      if (MyObjectBuilderType.m_typeByName.TryGetValue(value, out objectBuilderType))
        return objectBuilderType;
      else
        return MyObjectBuilderType.m_typeByLegacyName[value];
    }

    public static bool TryParse(string value, out MyObjectBuilderType result)
    {
      return MyObjectBuilderType.m_typeByName.TryGetValue(value, out result);
    }

    internal static void RegisterFromAssembly(Assembly assembly, bool registerLegacyNames = false)
    {
      if (assembly == (Assembly) null)
        return;
      Type type1 = typeof (MyObjectBuilder_Base);
      Type[] types = assembly.GetTypes();
      Array.Sort<Type>(types, (IComparer<Type>) FullyQualifiedNameComparer.Default);
      foreach (Type type2 in types)
      {
        if (type1.IsAssignableFrom(type2))
        {
          MyObjectBuilderType key1 = new MyObjectBuilderType(type2);
          MyRuntimeObjectBuilderId key2 = new MyRuntimeObjectBuilderId(++MyObjectBuilderType.m_idCounter);
          MyObjectBuilderType.m_typeById.Add(key2, key1);
          MyObjectBuilderType.m_idByType.Add(key1, key2);
          MyObjectBuilderType.m_typeByName.Add(type2.Name, key1);
          if (registerLegacyNames && type2.Name.StartsWith("MyObjectBuilder_"))
            MyObjectBuilderType.m_typeByLegacyName.Add(type2.Name.Substring("MyObjectBuilder_".Length), key1);
        }
      }
    }

    internal static void RegisterLegacyName(MyObjectBuilderType type, string legacyName)
    {
      MyObjectBuilderType.m_typeByLegacyName.Add(legacyName, type);
    }

    public class ComparerType : IEqualityComparer<MyObjectBuilderType>
    {
      public bool Equals(MyObjectBuilderType x, MyObjectBuilderType y)
      {
        return x == y;
      }

      public int GetHashCode(MyObjectBuilderType obj)
      {
        return obj.GetHashCode();
      }
    }
  }
}
