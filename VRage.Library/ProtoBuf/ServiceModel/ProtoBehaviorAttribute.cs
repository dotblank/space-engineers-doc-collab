// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.ProtoBehaviorAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ProtoBuf.ServiceModel
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class ProtoBehaviorAttribute : Attribute, IOperationBehavior
  {
    void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
    {
    }

    void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
    {
      
    }

    void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
    {
      
    }

    void IOperationBehavior.Validate(OperationDescription operationDescription)
    {
    }
  }
}
