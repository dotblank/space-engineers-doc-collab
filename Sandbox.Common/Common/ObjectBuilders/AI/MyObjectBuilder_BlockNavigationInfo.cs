﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.AI.MyObjectBuilder_BlockNavigationInfo
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.AI
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_BlockNavigationInfo : MyObjectBuilder_Base
    {
        [ProtoMember(1)] [XmlArrayItem("Triangle")] public MyObjectBuilder_BlockNavigationInfo.Triangle[] Triangles;

        [ProtoContract]
        public class Triangle
        {
            [ProtoMember(1)] [XmlArrayItem("Point")] public SerializableVector3[] Points;
        }
    }
}