// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoBeforeDeserializationAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.ComponentModel;

namespace ProtoBuf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    [ImmutableObject(true)]
    public sealed class ProtoBeforeDeserializationAttribute : Attribute
    {
    }
}