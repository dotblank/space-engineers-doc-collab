// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_GunBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using System.Xml.Serialization;
using VRage.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_GunBase : MyObjectBuilder_DeviceBase
  {
    [ProtoMember(2)]
    public string CurrentAmmoMagazineName = "";
    [ProtoMember(3)]
    public List<MyObjectBuilder_GunBase.RemainingAmmoIns> RemainingAmmosList = new List<MyObjectBuilder_GunBase.RemainingAmmoIns>();
    private SerializableDictionary<string, int> m_remainingAmmos;
    [ProtoMember(1)]
    public int RemainingAmmo;

    public SerializableDictionary<string, int> RemainingAmmos
    {
      get
      {
        return this.m_remainingAmmos;
      }
      set
      {
        this.m_remainingAmmos = value;
        if (this.RemainingAmmosList == null)
          this.RemainingAmmosList = new List<MyObjectBuilder_GunBase.RemainingAmmoIns>();
        foreach (KeyValuePair<string, int> keyValuePair in value.Dictionary)
          this.RemainingAmmosList.Add(new MyObjectBuilder_GunBase.RemainingAmmoIns()
          {
            SubtypeName = keyValuePair.Key,
            Amount = keyValuePair.Value
          });
      }
    }

    public bool ShouldSerializeRemainingAmmos()
    {
      return false;
    }

    [ProtoContract]
    public class RemainingAmmoIns
    {
      [XmlAttribute]
      public string SubtypeName;
      [XmlAttribute]
      public int Amount;
    }
  }
}
