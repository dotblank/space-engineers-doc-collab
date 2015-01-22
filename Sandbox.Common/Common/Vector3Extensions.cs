// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Vector3Extensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;

namespace Sandbox.Common
{
    public static class Vector3Extensions
    {
        public static Vector3 Project(this Vector3 projectedOntoVector, Vector3 projectedVector)
        {
            return Vector3.Dot(projectedVector, projectedOntoVector)/projectedOntoVector.LengthSquared()*
                   projectedOntoVector;
        }
    }
}