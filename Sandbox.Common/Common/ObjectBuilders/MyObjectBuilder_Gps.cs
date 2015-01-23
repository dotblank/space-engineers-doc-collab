﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Gps
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Gps : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public List<MyObjectBuilder_Gps.Entry> Entries;

        [ProtoContract]
        public struct Entry
        {
            [ProtoMember(1)] public string name;
            [ProtoMember(2)] public string description;
            [ProtoMember(3)] public Vector3D coords;
            [ProtoMember(4)] public bool isFinal;
            [ProtoMember(4)] public bool showOnHud;
        }
    }
}