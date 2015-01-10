// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFactoryTagAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
