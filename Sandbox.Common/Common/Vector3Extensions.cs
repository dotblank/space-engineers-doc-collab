// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Vector3Extensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;

namespace Sandbox.Common
{
  public static class Vector3Extensions
  {
    public static Vector3 Project(this Vector3 projectedOntoVector, Vector3 projectedVector)
    {
      return Vector3.Dot(projectedVector, projectedOntoVector) / projectedOntoVector.LengthSquared() * projectedOntoVector;
    }
  }
}
