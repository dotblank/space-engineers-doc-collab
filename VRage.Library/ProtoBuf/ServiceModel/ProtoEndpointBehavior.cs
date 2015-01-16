// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.ProtoEndpointBehavior
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ProtoBuf.ServiceModel
{
    public class ProtoEndpointBehavior : IEndpointBehavior
    {
        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(endpoint);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(endpoint);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        private static void ReplaceDataContractSerializerOperationBehavior(ServiceEndpoint serviceEndpoint)
        {
            foreach (
                OperationDescription description in
                    (Collection<OperationDescription>) serviceEndpoint.Contract.Operations)
                ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(description);
        }

        private static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
        {
            DataContractSerializerOperationBehavior operationBehavior =
                description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (operationBehavior == null)
                return;
            ((Collection<IOperationBehavior>) description.Behaviors).Remove((IOperationBehavior) operationBehavior);
            description.Behaviors.Add((IOperationBehavior) new ProtoOperationBehavior(description));
        }
    }
}