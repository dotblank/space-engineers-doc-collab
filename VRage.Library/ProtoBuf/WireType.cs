// Decompiled with JetBrains decompiler
// Type: ProtoBuf.WireType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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