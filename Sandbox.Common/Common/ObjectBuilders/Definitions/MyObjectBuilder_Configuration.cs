// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Configuration
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_Configuration : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public MyObjectBuilder_Configuration.CubeSizeSettings CubeSizes;
    [ProtoMember(2)]
    public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabs;
    [ProtoMember(3)]
    public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabsSurvival;

    [ProtoContract]
    public struct CubeSizeSettings
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public float Large;
      [ProtoMember(2)]
      [XmlAttribute]
      public float Small;
    }

    [ProtoContract]
    public struct BaseBlockSettings
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public string SmallStatic;
      [ProtoMember(2)]
      [XmlAttribute]
      public string LargeStatic;
      [ProtoMember(3)]
      [XmlAttribute]
      public string SmallDynamic;
      [ProtoMember(4)]
      [XmlAttribute]
      public string LargeDynamic;
    }
  }
}
