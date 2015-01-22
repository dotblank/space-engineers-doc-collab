// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyObjectFactory`2
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Serializer;
using System;
using System.Collections.Generic;
using System.Reflection;
using VRage.Collections;

namespace Sandbox.Common
{
    public class MyObjectFactory<TAttribute, TCreatedObjectBase> where TAttribute : MyFactoryTagAttribute
        where TCreatedObjectBase : class
    {
        private Dictionary<Type, TAttribute> m_attributesByProducedType = new Dictionary<Type, TAttribute>();
        private Dictionary<Type, TAttribute> m_attributesByObjectBuilder = new Dictionary<Type, TAttribute>();

        public DictionaryValuesReader<Type, TAttribute> Attributes
        {
            get { return new DictionaryValuesReader<Type, TAttribute>(this.m_attributesByProducedType); }
        }

        public void RegisterFromCreatedObjectAssembly()
        {
            this.RegisterFromAssembly(Assembly.GetAssembly(typeof (TCreatedObjectBase)));
        }

        public void RegisterFromAssembly(Assembly assembly)
        {
            if (assembly == (Assembly) null)
                return;
            foreach (Type type in assembly.GetTypes())
            {
                object[] customAttributes = type.GetCustomAttributes(typeof (TAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    TAttribute attribute = (TAttribute) customAttributes[0];
                    attribute.ProducedType = type;
                    this.m_attributesByProducedType.Add(attribute.ProducedType, attribute);
                    if (attribute.ObjectBuilderType != (Type) null)
                    {
                        try
                        {
                            this.m_attributesByObjectBuilder.Add(attribute.ObjectBuilderType, attribute);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (typeof (MyObjectBuilder_Base).IsAssignableFrom(attribute.ProducedType))
                        this.m_attributesByObjectBuilder.Add(attribute.ProducedType, attribute);
                }
            }
        }

        public TCreatedObjectBase CreateInstance(MyObjectBuilderType objectBuilderType)
        {
            return this.CreateInstance<TCreatedObjectBase>(objectBuilderType);
        }

        public TBase CreateInstance<TBase>(MyObjectBuilderType objectBuilderType)
            where TBase : class, TCreatedObjectBase
        {
            TAttribute attribute;
            if (this.m_attributesByObjectBuilder.TryGetValue((Type) objectBuilderType, out attribute))
                return Activator.CreateInstance(attribute.ProducedType) as TBase;
            else
                return default (TBase);
        }

        public TBase CreateInstance<TBase>() where TBase : TCreatedObjectBase, new()
        {
            return new TBase();
        }

        public Type GetProducedType(MyObjectBuilderType objectBuilderType)
        {
            return this.m_attributesByObjectBuilder[(Type) objectBuilderType].ProducedType;
        }

        public TObjectBuilder CreateObjectBuilder<TObjectBuilder>(TCreatedObjectBase instance)
            where TObjectBuilder : MyObjectBuilder_Base
        {
            TAttribute attribute;
            if (!this.m_attributesByProducedType.TryGetValue(instance.GetType(), out attribute))
                return default (TObjectBuilder);
            else
                return
                    MyObjectBuilderSerializer.CreateNewObject((MyObjectBuilderType) attribute.ObjectBuilderType) as
                        TObjectBuilder;
        }
    }
}