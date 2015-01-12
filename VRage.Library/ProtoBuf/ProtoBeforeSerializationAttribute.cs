// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoBeforeSerializationAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.ComponentModel;

namespace ProtoBuf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    [ImmutableObject(true)]
    public sealed class ProtoBeforeSerializationAttribute : Attribute
    {
    }
}