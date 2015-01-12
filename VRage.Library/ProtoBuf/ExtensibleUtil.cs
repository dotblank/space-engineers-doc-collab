// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ExtensibleUtil
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ProtoBuf
{
    internal static class ExtensibleUtil
    {
        internal static IEnumerable<TValue> GetExtendedValues<TValue>(IExtensible instance, int tag, DataFormat format,
            bool singleton, bool allowDefinedTag)
        {
            foreach (
                TValue obj in
                    ExtensibleUtil.GetExtendedValues((TypeModel) RuntimeTypeModel.Default, typeof (TValue), instance,
                        tag, format, singleton, allowDefinedTag))
                yield return obj;
        }

        internal static IEnumerable GetExtendedValues(TypeModel model, Type type, IExtensible instance, int tag,
            DataFormat format, bool singleton, bool allowDefinedTag)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            if (tag <= 0)
                throw new ArgumentOutOfRangeException("tag");
            IExtension extn = instance.GetExtensionObject(false);
            if (extn != null)
            {
                Stream stream = extn.BeginQuery();
                object value = (object) null;
                try
                {
                    SerializationContext ctx = new SerializationContext();
                    using (ProtoReader reader = new ProtoReader(stream, model, ctx))
                    {
                        while (
                            model.TryDeserializeAuxiliaryType(reader, format, tag, type, ref value, true, false, false,
                                false) && value != null)
                        {
                            if (!singleton)
                            {
                                yield return value;
                                value = (object) null;
                            }
                        }
                    }
                    if (singleton && value != null)
                        yield return value;
                }
                finally
                {
                    extn.EndQuery(stream);
                }
            }
        }

        internal static void AppendExtendValue(TypeModel model, IExtensible instance, int tag, DataFormat format,
            object value)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            if (value == null)
                throw new ArgumentNullException("value");
            IExtension extensionObject = instance.GetExtensionObject(true);
            if (extensionObject == null)
                throw new InvalidOperationException("No extension object available; appended data would be lost.");
            bool commit = false;
            Stream stream = extensionObject.BeginAppend();
            try
            {
                using (ProtoWriter writer = new ProtoWriter(stream, model, (SerializationContext) null))
                {
                    model.TrySerializeAuxiliaryType(writer, (Type) null, format, tag, value, false);
                    writer.Close();
                }
                commit = true;
            }
            finally
            {
                extensionObject.EndAppend(stream, commit);
            }
        }

        public static void AppendExtendValueTyped<TSource, TValue>(TypeModel model, TSource instance, int tag,
            DataFormat format, TValue value) where TSource : class, IExtensible
        {
            ExtensibleUtil.AppendExtendValue(model, (IExtensible) instance, tag, format, (object) value);
        }
    }
}