// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.ModdableContentFile
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.ObjectBuilders
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class ModdableContentFile : Attribute
  {
    public string FileExtension;

    public ModdableContentFile(string fileExtension)
    {
      this.FileExtension = fileExtension;
    }
  }
}
