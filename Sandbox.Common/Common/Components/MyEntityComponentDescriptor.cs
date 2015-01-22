// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyEntityComponentDescriptor
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyEntityComponentDescriptor : Attribute
    {
        public Type EntityBuilderType;

        public MyEntityComponentDescriptor(Type entityBuilderType)
        {
            this.EntityBuilderType = entityBuilderType;
        }
    }
}