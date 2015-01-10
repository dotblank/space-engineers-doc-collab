// Decompiled with JetBrains decompiler
// Type: VRageMath.MyLineSegmentOverlapResult`1
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System.Collections.Generic;

namespace VRageMath
{
  public struct MyLineSegmentOverlapResult<T>
  {
    public static MyLineSegmentOverlapResult<T>.MyLineSegmentOverlapResultComparer DistanceComparer = new MyLineSegmentOverlapResult<T>.MyLineSegmentOverlapResultComparer();
    public double Distance;
    public T Element;

    public class MyLineSegmentOverlapResultComparer : IComparer<MyLineSegmentOverlapResult<T>>
    {
      public int Compare(MyLineSegmentOverlapResult<T> x, MyLineSegmentOverlapResult<T> y)
      {
        return x.Distance.CompareTo(y.Distance);
      }
    }
  }
}
