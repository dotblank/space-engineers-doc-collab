// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilderExtensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Definitions;

namespace Sandbox.Common.ObjectBuilders
{
  public static class MyObjectBuilderExtensions
  {
    public static MyDefinitionId GetId(this MyObjectBuilder_Base self)
    {
      return new MyDefinitionId(self.TypeId, self.SubtypeId);
    }
  }
}
