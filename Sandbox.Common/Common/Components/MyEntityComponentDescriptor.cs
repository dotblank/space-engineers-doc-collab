// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyEntityComponentDescriptor
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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