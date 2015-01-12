// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.ProtoBehaviorAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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
        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription,
            ClientOperation clientOperation)
        {
            // removed because trying to fix the errors is pointless
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription,
            DispatchOperation dispatchOperation)
        {
            // removed because trying to fix the errors is pointless
        }

        void IOperationBehavior.Validate(OperationDescription operationDescription)
        {
        }
    }
}