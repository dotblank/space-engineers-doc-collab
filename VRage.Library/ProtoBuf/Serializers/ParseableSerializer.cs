// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.ParseableSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
    internal sealed class ParseableSerializer : IProtoSerializer
    {
        private readonly MethodInfo parse;

        public Type ExpectedType
        {
            get { return this.parse.DeclaringType; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return false; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        private ParseableSerializer(MethodInfo parse)
        {
            this.parse = parse;
        }

        public static ParseableSerializer TryCreate(Type type, TypeModel model)
        {
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            MethodInfo method = type.GetMethod("Parse",
                BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[1]
                {
                    model.MapType(typeof (string))
                }, (ParameterModifier[]) null);
            if (!(method != (MethodInfo) null) || !(method.ReturnType == type))
                return (ParseableSerializer) null;
            if (Helpers.IsValueType(type))
            {
                MethodInfo customToString = ParseableSerializer.GetCustomToString(type);
                if (customToString == (MethodInfo) null || customToString.ReturnType != model.MapType(typeof (string)))
                    return (ParseableSerializer) null;
            }
            return new ParseableSerializer(method);
        }

        private static MethodInfo GetCustomToString(Type type)
        {
            return type.GetMethod("ToString", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public,
                (Binder) null, Helpers.EmptyTypes, (ParameterModifier[]) null);
        }

        public object Read(object value, ProtoReader source)
        {
            return this.parse.Invoke((object) null, new object[1]
            {
                (object) source.ReadString()
            });
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteString(value.ToString(), dest);
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            Type expectedType = this.ExpectedType;
            if (expectedType.IsValueType)
            {
                using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
                {
                    ctx.LoadAddress(localWithValue, expectedType);
                    ctx.EmitCall(ParseableSerializer.GetCustomToString(expectedType));
                }
            }
            else
                ctx.EmitCall(ctx.MapType(typeof (object)).GetMethod("ToString"));
            ctx.EmitBasicWrite("WriteString", valueFrom);
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            ctx.EmitBasicRead("ReadString", ctx.MapType(typeof (string)));
            ctx.EmitCall(this.parse);
        }
    }
}