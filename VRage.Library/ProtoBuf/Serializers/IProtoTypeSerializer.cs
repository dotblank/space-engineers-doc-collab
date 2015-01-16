// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.IProtoTypeSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
    internal interface IProtoTypeSerializer : IProtoSerializer
    {
        bool HasCallbacks(TypeModel.CallbackType callbackType);

        bool CanCreateInstance();

        object CreateInstance(ProtoReader source);

        void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context);

        void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType);

        void EmitCreateInstance(CompilerContext ctx);
    }
}