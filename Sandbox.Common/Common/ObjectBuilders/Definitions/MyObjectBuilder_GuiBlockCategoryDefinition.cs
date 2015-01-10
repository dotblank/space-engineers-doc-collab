// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_GuiBlockCategoryDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_GuiBlockCategoryDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(4)]
    public bool IsBlockCategory = true;
    [ProtoMember(5)]
    public bool SearchBlocks = true;
    [ProtoMember(7)]
    public new bool Public = true;
    [ProtoMember(1)]
    public string Name;
    [ProtoMember(2)]
    public string[] ItemIds;
    [ProtoMember(3)]
    public bool IsShipCategory;
    [ProtoMember(6)]
    public bool ShowAnimations;
  }
}
