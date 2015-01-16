// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Localization.MyLanguageDescriptionAttribute
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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