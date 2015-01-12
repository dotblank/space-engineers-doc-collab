// Decompiled with JetBrains decompiler
// Type: VRage.MyTuple`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace VRage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MyTuple<T1>
    {
        public T1 Item1;

        public MyTuple(T1 item1)
        {
            this.Item1 = item1;
        }
    }
}