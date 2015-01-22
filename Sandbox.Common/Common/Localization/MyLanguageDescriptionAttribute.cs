// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Localization.MyLanguageDescriptionAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.ComponentModel;

namespace Sandbox.Common.Localization
{
    public class MyLanguageDescriptionAttribute : DescriptionAttribute
    {
        public readonly float GuiTextScale;

        public MyLanguageDescriptionAttribute(string description, float guiTextScale = 1f)
            : base(description)
        {
            this.GuiTextScale = guiTextScale;
        }
    }
}