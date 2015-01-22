// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.ProtoOperationBehavior
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;

namespace ProtoBuf.ServiceModel
{
  public sealed class ProtoOperationBehavior : DataContractSerializerOperationBehavior
  {
    private TypeModel model;

    public TypeModel Model
    {
      get
      {
        return this.model;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Model");
        this.model = value;
      }
    }

    public ProtoOperationBehavior(OperationDescription operation)
      : base(operation)
    {
      this.model = (TypeModel) RuntimeTypeModel.Default;
    }

    public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
    {
      if (this.model == null)
        throw new InvalidOperationException("No Model instance has been assigned to the ProtoOperationBehavior");
      else
        return (XmlObjectSerializer) XmlProtoSerializer.TryCreate(this.model, type) ?? base.CreateSerializer(type, name, ns, knownTypes);
    }
  }
}
