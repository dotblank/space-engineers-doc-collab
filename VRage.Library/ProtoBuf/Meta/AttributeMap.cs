// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.AttributeMap
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
  internal abstract class AttributeMap
  {
    public abstract Type AttributeType { get; }

    public abstract object Target { get; }

    [Obsolete("Please use AttributeType instead")]
    public new Type GetType()
    {
      return this.AttributeType;
    }

    public abstract bool TryGet(string key, bool publicOnly, out object value);

    public bool TryGet(string key, out object value)
    {
      return this.TryGet(key, true, out value);
    }

    public static AttributeMap[] Create(TypeModel model, Type type, bool inherit)
    {
      object[] customAttributes = type.GetCustomAttributes(inherit);
      AttributeMap[] attributeMapArray = new AttributeMap[customAttributes.Length];
      for (int index = 0; index < customAttributes.Length; ++index)
        attributeMapArray[index] = (AttributeMap) new AttributeMap.ReflectionAttributeMap((Attribute) customAttributes[index]);
      return attributeMapArray;
    }

    public static AttributeMap[] Create(TypeModel model, MemberInfo member, bool inherit)
    {
      object[] customAttributes = member.GetCustomAttributes(inherit);
      AttributeMap[] attributeMapArray = new AttributeMap[customAttributes.Length];
      for (int index = 0; index < customAttributes.Length; ++index)
        attributeMapArray[index] = (AttributeMap) new AttributeMap.ReflectionAttributeMap((Attribute) customAttributes[index]);
      return attributeMapArray;
    }

    public static AttributeMap[] Create(TypeModel model, Assembly assembly)
    {
      object[] customAttributes = assembly.GetCustomAttributes(false);
      AttributeMap[] attributeMapArray = new AttributeMap[customAttributes.Length];
      for (int index = 0; index < customAttributes.Length; ++index)
        attributeMapArray[index] = (AttributeMap) new AttributeMap.ReflectionAttributeMap((Attribute) customAttributes[index]);
      return attributeMapArray;
    }

    private class ReflectionAttributeMap : AttributeMap
    {
      private readonly Attribute attribute;

      public override object Target
      {
        get
        {
          return (object) this.attribute;
        }
      }

      public override Type AttributeType
      {
        get
        {
          return this.attribute.GetType();
        }
      }

      public ReflectionAttributeMap(Attribute attribute)
      {
        this.attribute = attribute;
      }

      public override bool TryGet(string key, bool publicOnly, out object value)
      {
        foreach (MemberInfo memberInfo in Helpers.GetInstanceFieldsAndProperties(this.attribute.GetType(), publicOnly))
        {
          if (string.Equals(memberInfo.Name, key, StringComparison.OrdinalIgnoreCase))
          {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != (PropertyInfo) null)
            {
              value = propertyInfo.GetValue((object) this.attribute, (object[]) null);
              return true;
            }
            else
            {
              FieldInfo fieldInfo = memberInfo as FieldInfo;
              if (!(fieldInfo != (FieldInfo) null))
                throw new NotSupportedException(memberInfo.GetType().Name);
              value = fieldInfo.GetValue((object) this.attribute);
              return true;
            }
          }
        }
        value = (object) null;
        return false;
      }
    }
  }
}
