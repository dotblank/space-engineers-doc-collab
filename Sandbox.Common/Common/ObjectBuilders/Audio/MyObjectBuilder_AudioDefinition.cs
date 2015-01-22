﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Audio.MyObjectBuilder_AudioDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Audio
{
    [MyObjectBuilderDefinition]
    [XmlType("Sound")]
    [ProtoContract]
    public class MyObjectBuilder_AudioDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(2)] public string Category = "Undefined";

        [ProtoMember(3)] [DefaultValue(MyAudioHelpers.CurveType.Custom_1)] public MyAudioHelpers.CurveType VolumeCurve =
            MyAudioHelpers.CurveType.Custom_1;

        [DefaultValue(1f)] [ProtoMember(5)] public float Volume = 1f;
        [ProtoMember(4)] public float MaxDistance;
        [DefaultValue(0.0f)] [ProtoMember(6)] public float VolumeVariation;
        [DefaultValue(0.0f)] [ProtoMember(7)] public float PitchVariation;
        [ProtoMember(8)] [DefaultValue(false)] public bool Loopable;
        [ProtoMember(9)] public string Alternative2D;
        [DefaultValue(false)] [ProtoMember(10)] public bool UseOcclusion;
        [ProtoMember(11)] public List<MyAudioHelpers.Wave> Waves;
        [ProtoMember(12)] public string TransitionCategory;
        [ProtoMember(13)] public string MusicCategory;

        public bool IsHudCue
        {
            get { return StringComparer.InvariantCultureIgnoreCase.Equals(this.Category, "hud"); }
        }
    }
}