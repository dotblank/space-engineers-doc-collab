// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.CallbackSet
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace ProtoBuf.Meta
{
    public class CallbackSet
    {
        private readonly MetaType metaType;
        private MethodInfo beforeSerialize;
        private MethodInfo afterSerialize;
        private MethodInfo beforeDeserialize;
        private MethodInfo afterDeserialize;

        internal MethodInfo this[TypeModel.CallbackType callbackType]
        {
            get
            {
                switch (callbackType)
                {
                    case TypeModel.CallbackType.BeforeSerialize:
                        return this.beforeSerialize;
                    case TypeModel.CallbackType.AfterSerialize:
                        return this.afterSerialize;
                    case TypeModel.CallbackType.BeforeDeserialize:
                        return this.beforeDeserialize;
                    case TypeModel.CallbackType.AfterDeserialize:
                        return this.afterDeserialize;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public MethodInfo BeforeSerialize
        {
            get { return this.beforeSerialize; }
            set { this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value); }
        }

        public MethodInfo BeforeDeserialize
        {
            get { return this.beforeDeserialize; }
            set { this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value); }
        }

        public MethodInfo AfterSerialize
        {
            get { return this.afterSerialize; }
            set { this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value); }
        }

        public MethodInfo AfterDeserialize
        {
            get { return this.afterDeserialize; }
            set { this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value); }
        }

        public bool NonTrivial
        {
            get
            {
                if (!(this.beforeSerialize != (MethodInfo) null) && !(this.beforeDeserialize != (MethodInfo) null) &&
                    !(this.afterSerialize != (MethodInfo) null))
                    return this.afterDeserialize != (MethodInfo) null;
                else
                    return true;
            }
        }

        internal CallbackSet(MetaType metaType)
        {
            if (metaType == null)
                throw new ArgumentNullException("metaType");
            this.metaType = metaType;
        }

        internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
        {
            foreach (ParameterInfo parameterInfo in method.GetParameters())
            {
                Type parameterType = parameterInfo.ParameterType;
                if (!(parameterType == model.MapType(typeof (SerializationContext))) &&
                    !(parameterType == model.MapType(typeof (Type))) &&
                    !(parameterType == model.MapType(typeof (StreamingContext))))
                    return false;
            }
            return true;
        }

        private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
        {
            this.metaType.ThrowIfFrozen();
            if (callback == (MethodInfo) null)
                return callback;
            if (callback.IsStatic)
                throw new ArgumentException("Callbacks cannot be static", "callback");
            if (callback.ReturnType != model.MapType(typeof (void)) ||
                !CallbackSet.CheckCallbackParameters(model, callback))
                throw CallbackSet.CreateInvalidCallbackSignature(callback);
            else
                return callback;
        }

        internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
        {
            return
                (Exception)
                    new NotSupportedException("Invalid callback signature in " + method.DeclaringType.FullName + "." +
                                              method.Name);
        }
    }
}