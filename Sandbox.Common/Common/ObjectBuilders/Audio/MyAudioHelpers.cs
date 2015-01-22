// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Audio.MyAudioHelpers
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Audio
{
    public class MyAudioHelpers
    {
        public enum CurveType
        {
            Linear,
            Quadratic,
            Poly2,
            Custom_1,
        }

        public enum Dimensions
        {
            D2,
            D3,
        }

        [ProtoContract]
        [XmlType("Wave")]
        public class Wave
        {
            [XmlAttribute] [ProtoMember(1)] public MyAudioHelpers.Dimensions Type;
            [ProtoMember(2)] [DefaultValue("")] [ModdableContentFile("xwm")] public string Start;
            [ModdableContentFile("xwm")] [DefaultValue("")] [ProtoMember(3)] public string Loop;
            [DefaultValue("")] [ProtoMember(4)] [ModdableContentFile("xwm")] public string End;
        }
    }
}