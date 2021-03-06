﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_AmmoMagazineDefinition
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
  public class MyObjectBuilder_AmmoMagazineDefinition : MyObjectBuilder_PhysicalItemDefinition
  {
    [ProtoMember(1)]
    public int Capacity;
    [ProtoMember(2)]
    public MyAmmoCategoryEnum Category;
    [ProtoMember(3)]
    public MyObjectBuilder_AmmoMagazineDefinition.AmmoDefinition AmmoDefinitionId;

    [ProtoContract]
    public class AmmoDefinition
    {
      [XmlIgnore]
      public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_AmmoDefinition);
      [XmlAttribute]
      [ProtoMember(1)]
      public string Subtype;
    }
  }
}
