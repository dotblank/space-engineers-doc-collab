// Decompiled with JetBrains decompiler
// Type: VRageMath.MyLineSegmentOverlapResult`1
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System.Collections.Generic;

namespace VRageMath
{
    public struct MyLineSegmentOverlapResult<T>
    {
        public static MyLineSegmentOverlapResult<T>.MyLineSegmentOverlapResultComparer DistanceComparer =
            new MyLineSegmentOverlapResult<T>.MyLineSegmentOverlapResultComparer();

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