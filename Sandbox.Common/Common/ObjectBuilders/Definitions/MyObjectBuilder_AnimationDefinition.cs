// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_AnimationDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_AnimationDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(4)]
    public bool AllowInCockpit = true;
    [ProtoMember(6)]
    public string SupportedSkeletons = "Humanoid";
    [ProtoMember(1)]
    [ModdableContentFile("mwm")]
    public string AnimationModel;
    [ProtoMember(2)]
    public int ClipIndex;
    [ProtoMember(3)]
    public MyBonesArea InfluenceArea;
    [ProtoMember(5)]
    public bool AllowWithWeapon;
    [ProtoMember(7)]
    public bool Loop;
    [ProtoMember(8)]
    public SerializableDefinitionId LeftHandItem;
  }
}
