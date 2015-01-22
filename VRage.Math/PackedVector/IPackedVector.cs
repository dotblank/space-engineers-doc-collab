// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.IPackedVector
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using VRageMath;

namespace VRageMath.PackedVector
{
  public interface IPackedVector
  {
    Vector4 ToVector4();

    void PackFromVector4(Vector4 vector);
  }
}
