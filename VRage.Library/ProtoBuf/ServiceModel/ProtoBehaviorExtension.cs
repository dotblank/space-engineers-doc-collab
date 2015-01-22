// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.ProtoBehaviorExtension
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.ServiceModel.Configuration;

namespace ProtoBuf.ServiceModel
{
  public class ProtoBehaviorExtension : BehaviorExtensionElement
  {
    public override Type BehaviorType
    {
      get
      {
        return typeof (ProtoEndpointBehavior);
      }
    }

    protected override object CreateBehavior()
    {
      return (object) new ProtoEndpointBehavior();
    }
  }
}
