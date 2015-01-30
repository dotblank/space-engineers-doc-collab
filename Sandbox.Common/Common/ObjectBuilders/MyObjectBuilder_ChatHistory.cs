// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ChatHistory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ChatHistory : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public long IdentityId;
    [ProtoMember(2)]
    public List<MyObjectBuilder_PlayerChatHistory> PlayerChatHistory;
    [ProtoMember(3)]
    public MyObjectBuilder_GlobalChatHistory GlobalChatHistory;
  }
}
