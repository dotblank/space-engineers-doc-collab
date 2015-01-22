// Decompiled with JetBrains decompiler
// Type: ProtoBuf.MemberSerializationOptions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
