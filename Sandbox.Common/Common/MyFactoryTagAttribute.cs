// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFactoryTagAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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