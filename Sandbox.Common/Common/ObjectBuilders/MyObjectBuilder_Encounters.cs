// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Encounters
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
