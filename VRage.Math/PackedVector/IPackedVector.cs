// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.IPackedVector
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
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
