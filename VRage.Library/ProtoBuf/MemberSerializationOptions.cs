// Decompiled with JetBrains decompiler
// Type: ProtoBuf.MemberSerializationOptions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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