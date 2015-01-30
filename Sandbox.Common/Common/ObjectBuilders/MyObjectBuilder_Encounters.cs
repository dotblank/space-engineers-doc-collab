// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Encounters
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using VRage.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_Encounters : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public HashSet<MyEncounterId> SavedEcounters;
    [ProtoMember(2)]
    public SerializableDictionary<MyEncounterId, Vector3D> MovedOnlyEncounters;
  }
}
