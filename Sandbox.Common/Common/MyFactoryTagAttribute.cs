// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFactoryTagAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MyFactoryTagAttribute : Attribute
    {
        public readonly Type ObjectBuilderType;
        public Type ProducedType;

        public MyFactoryTagAttribute(Type objectBuilderType)
        {
            this.ObjectBuilderType = objectBuilderType;
        }
    }
}