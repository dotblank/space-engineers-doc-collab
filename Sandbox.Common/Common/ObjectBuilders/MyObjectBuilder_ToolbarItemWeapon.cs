// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ToolbarItemWeapon
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_ToolbarItemWeapon : MyObjectBuilder_ToolbarItemDefinition
  {
    public SerializableDefinitionId defId
    {
      get
      {
        return this.DefinitionId;
      }
      set
      {
        this.DefinitionId = value;
      }
    }

    public bool ShouldSerializedefId()
    {
      return false;
    }
  }
}
