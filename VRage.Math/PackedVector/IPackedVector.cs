// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.IPackedVector
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
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