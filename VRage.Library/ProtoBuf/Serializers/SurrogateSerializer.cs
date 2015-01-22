// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.SurrogateSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
  internal sealed class SurrogateSerializer : IProtoTypeSerializer, IProtoSerializer
  {
    private readonly Type forType;
    private readonly Type declaredType;
    private readonly MethodInfo toTail;
    private readonly MethodInfo fromTail;
    private IProtoTypeSerializer rootTail;

    public bool ReturnsValue
    {
      get
      {
        return false;
      }
    }

    public bool RequiresOldValue
    {
      get
      {
        return true;
      }
    }

    public Type ExpectedType
    {
      get
      {
        return this.forType;
      }
    }

    public SurrogateSerializer(Type forType, Type declaredType, IProtoTypeSerializer rootTail)
    {
      this.forType = forType;
      this.declaredType = declaredType;
      this.rootTail = rootTail;
      this.toTail = this.GetConversion(true);
      this.fromTail = this.GetConversion(false);
    }

    bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
    {
      return false;
    }

    void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
    {
    }

    void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
    {
      throw new NotSupportedException();
    }

    bool IProtoTypeSerializer.CanCreateInstance()
    {
      return false;
    }

    object IProtoTypeSerializer.CreateInstance(ProtoReader source)
    {
      throw new NotSupportedException();
    }

    void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
    {
    }

    private static bool HasCast(Type type, Type from, Type to, out MethodInfo op)
    {
      foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
      {
        if ((!(methodInfo.Name != "op_Implicit") || !(methodInfo.Name != "op_Explicit")) && !(methodInfo.ReturnType != to))
        {
          ParameterInfo[] parameters = methodInfo.GetParameters();
          if (parameters.Length == 1 && parameters[0].ParameterType == from)
          {
            op = methodInfo;
            return true;
          }
        }
      }
      op = (MethodInfo) null;
      return false;
    }

    public MethodInfo GetConversion(bool toTail)
    {
      Type to = toTail ? this.declaredType : this.forType;
      Type from = toTail ? this.forType : this.declaredType;
      MethodInfo op;
      if (SurrogateSerializer.HasCast(this.declaredType, from, to, out op) || SurrogateSerializer.HasCast(this.forType, from, to, out op))
        return op;
      else
        throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + this.forType.FullName + " / " + this.declaredType.FullName);
    }

    public void Write(object value, ProtoWriter writer)
    {
      this.rootTail.Write(this.toTail.Invoke((object) null, new object[1]
      {
        value
      }), writer);
    }

    public object Read(object value, ProtoReader source)
    {
      object[] parameters = new object[1]
      {
        value
      };
      value = this.toTail.Invoke((object) null, parameters);
      parameters[0] = this.rootTail.Read(value, source);
      return this.fromTail.Invoke((object) null, parameters);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      using (Local local = new Local(ctx, this.declaredType))
      {
        ctx.LoadValue(valueFrom);
        ctx.EmitCall(this.toTail);
        ctx.StoreValue(local);
        this.rootTail.EmitRead(ctx, local);
        ctx.LoadValue(local);
        ctx.EmitCall(this.fromTail);
        ctx.StoreValue(valueFrom);
      }
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.LoadValue(valueFrom);
      ctx.EmitCall(this.toTail);
      this.rootTail.EmitWrite(ctx, (Local) null);
    }
  }
}
