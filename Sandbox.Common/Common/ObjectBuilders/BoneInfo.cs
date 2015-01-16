// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.BoneInfo
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public struct BoneInfo
    {
        [ProtoMember(1)] public SerializableVector3I BonePosition;
        [ProtoMember(2)] public SerializableVector3UByte BoneOffset;
    }
}