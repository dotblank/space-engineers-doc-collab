// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MySyncComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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