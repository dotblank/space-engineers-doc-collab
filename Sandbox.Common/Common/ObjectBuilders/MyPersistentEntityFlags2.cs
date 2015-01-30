// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyPersistentEntityFlags2
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.ObjectBuilders
{
  [Flags]
  public enum MyPersistentEntityFlags2
  {
    None = 0,
    Enabled = 2,
    CastShadows = 4,
    InScene = 16,
  }
}
