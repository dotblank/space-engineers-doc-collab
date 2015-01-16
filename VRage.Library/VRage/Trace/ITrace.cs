// Decompiled with JetBrains decompiler
// Type: VRage.Trace.ITrace
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace VRage.Trace
{
    public interface ITrace
    {
        void Watch(string name, object value);

        void Send(string msg, string comment = null);
    }
}