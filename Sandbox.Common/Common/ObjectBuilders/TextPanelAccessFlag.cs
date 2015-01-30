// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.TextPanelAccessFlag
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.ObjectBuilders
{
  [Flags]
  public enum TextPanelAccessFlag : byte
  {
    NONE = (byte) 0,
    READ_FACTION = (byte) 2,
    WRITE_FACTION = (byte) 4,
    READ_AND_WRITE_FACTION = WRITE_FACTION | READ_FACTION,
    READ_ENEMY = (byte) 8,
    WRITE_ENEMY = (byte) 16,
    READ_ALL = READ_ENEMY | READ_FACTION,
    WRITE_ALL = WRITE_ENEMY | WRITE_FACTION,
    READ_AND_WRITE_ALL = WRITE_ALL | READ_ALL,
  }
}
