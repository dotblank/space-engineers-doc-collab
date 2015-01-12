// Decompiled with JetBrains decompiler
// Type: KeenSoftwareHouse.Library.Extensions.EnabledAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace KeenSoftwareHouse.Library.Extensions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EnabledAttribute : Attribute
    {
        public bool Enabled { get; set; }

        public EnabledAttribute(bool enabled = true)
        {
            this.Enabled = enabled;
        }
    }
}