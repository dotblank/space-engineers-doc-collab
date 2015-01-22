// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.ModdableContentFile
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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