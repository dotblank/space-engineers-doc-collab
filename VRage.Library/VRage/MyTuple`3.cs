// Decompiled with JetBrains decompiler
// Type: VRage.MyTuple`3
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace VRage
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct MyTuple<T1, T2, T3>
  {
    public T1 Item1;
    public T2 Item2;
    public T3 Item3;

    public MyTuple(T1 item1, T2 item2, T3 item3)
    {
      this.Item1 = item1;
      this.Item2 = item2;
      this.Item3 = item3;
    }
  }
}
