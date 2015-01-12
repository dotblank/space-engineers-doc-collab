// Decompiled with JetBrains decompiler
// Type: ProtoBuf.WireType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace ProtoBuf
{
    public enum WireType
    {
        None = -1,
        Variant = 0,
        Fixed64 = 1,
        String = 2,
        StartGroup = 3,
        EndGroup = 4,
        Fixed32 = 5,
        SignedVariant = 8,
    }
}