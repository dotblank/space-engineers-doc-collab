// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyEntityComponentDescriptor
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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