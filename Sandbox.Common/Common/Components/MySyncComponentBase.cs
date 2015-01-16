// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MySyncComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

namespace Sandbox.Common.Components
{
    public abstract class MySyncComponentBase : MyComponentBase
    {
        public abstract bool UpdatesOnlyOnServer { get; set; }

        public abstract void SendCloseRequest();

        public abstract void Tick();

        public abstract void UpdatePosition();
    }
}