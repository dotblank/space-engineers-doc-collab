﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyDefinitionTypeAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using System;

namespace Sandbox.Definitions
{
    public class MyDefinitionTypeAttribute : MyFactoryTagAttribute
    {
        public MyDefinitionTypeAttribute(Type objectBuilderType)
            : base(objectBuilderType)
        {
        }
    }
}