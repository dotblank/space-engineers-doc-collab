// Decompiled with JetBrains decompiler
// Type: ProtoBuf.MemberSerializationOptions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
    [Flags]
    public enum MemberSerializationOptions
    {
        None = 0,
        Packed = 1,
        Required = 2,
        AsReference = 4,
        DynamicType = 8,
        OverwriteList = 16,
        AsReferenceHasValue = 32,
    }
}