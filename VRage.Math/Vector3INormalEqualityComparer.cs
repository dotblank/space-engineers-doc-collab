// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3INormalEqualityComparer
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System.Collections.Generic;

namespace VRageMath
{
  public class Vector3INormalEqualityComparer : IEqualityComparer<Vector3I>
  {
    public bool Equals(Vector3I x, Vector3I y)
    {
      return x.X + 1 + (x.Y + 1) * 3 + (x.Z + 1) * 9 == y.X + 1 + (y.Y + 1) * 3 + (y.Z + 1) * 9;
    }

    public int GetHashCode(Vector3I x)
    {
      return x.X + 1 + (x.Y + 1) * 3 + (x.Z + 1) * 9;
    }
  }
}
