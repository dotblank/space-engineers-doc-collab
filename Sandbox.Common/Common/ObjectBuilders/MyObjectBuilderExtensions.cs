// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilderExtensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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
