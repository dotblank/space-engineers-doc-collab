// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Serializer.MyObjectBuilderSerializer
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf.Meta;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using SysUtils.Utils;
using VRage.Common.Plugins;
using VRage.Common.Utils;

namespace Sandbox.Common.ObjectBuilders.Serializer
{
  public class MyObjectBuilderSerializer
  {
    private static readonly List<Type> m_serializationTypes = new List<Type>();
    private static readonly Dictionary<Type, XmlSerializer> m_serializersByType = new Dictionary<Type, XmlSerializer>();
    private static readonly Dictionary<string, XmlSerializer> m_serializersBySerializedName = new Dictionary<string, XmlSerializer>();
    private static readonly Dictionary<Type, string> m_serializedNameByType = new Dictionary<Type, string>();
    public static readonly RuntimeTypeModel Serializer = ProtoBuf.Meta.TypeModel.Create();
    private static readonly MyObjectFactory<MyObjectBuilderDefinitionAttribute, MyObjectBuilder_Base> m_objectFactory;

    static MyObjectBuilderSerializer()
    {
      MyObjectBuilderSerializer.Serializer.AutoAddMissingTypes = true;
      MyObjectBuilderSerializer.Serializer.UseImplicitZeroDefaults = false;
      MyObjectBuilderSerializer.m_objectFactory = new MyObjectFactory<MyObjectBuilderDefinitionAttribute, MyObjectBuilder_Base>();
      MyObjectBuilderSerializer.m_objectFactory.RegisterFromAssembly(Assembly.GetExecutingAssembly());
      MyObjectBuilderSerializer.m_objectFactory.RegisterFromAssembly(MyPlugins.GameAssembly);
      MyObjectBuilderSerializer.m_objectFactory.RegisterFromAssembly(MyPlugins.UserAssembly);
      MyObjectBuilderSerializer.LoadSerializers();
    }

    private static void LoadSerializers()
    {
      foreach (MyObjectBuilderDefinitionAttribute definitionAttribute in MyObjectBuilderSerializer.m_objectFactory.Attributes)
      {
        MyObjectBuilderSerializer.m_serializationTypes.Add(definitionAttribute.ProducedType);
        MyRuntimeObjectBuilderId runtimeObjectBuilderId = (MyRuntimeObjectBuilderId) (MyObjectBuilderType) definitionAttribute.ProducedType;
        MyObjectBuilderSerializer.Serializer.Add(definitionAttribute.ProducedType.BaseType, true).AddSubType((int) runtimeObjectBuilderId.Value * 1000, definitionAttribute.ProducedType);
      }
      foreach (Type type in MyObjectBuilderSerializer.m_serializationTypes)
      {
        try
        {
          XmlSerializer xmlSerializer = new XmlSerializer(type);
          MyObjectBuilderSerializer.m_serializersByType.Add(type, xmlSerializer);
          string key = type.Name;
          object[] customAttributes = type.GetCustomAttributes(typeof (XmlTypeAttribute), false);
          if (customAttributes.Length != 0)
            key = (customAttributes[0] as XmlTypeAttribute).TypeName;
          MyObjectBuilderSerializer.m_serializersBySerializedName.Add(key, xmlSerializer);
          MyObjectBuilderSerializer.m_serializedNameByType.Add(type, key);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException("Error creating XML serializer for type " + type.Name, ex);
        }
      }
    }

    internal static XmlSerializer GetSerializer(Type type)
    {
      return MyObjectBuilderSerializer.m_serializersByType[type];
    }

    internal static string GetSerializedName(Type type)
    {
      return MyObjectBuilderSerializer.m_serializedNameByType[type];
    }

    internal static XmlSerializer GetSerializer(string serializedName)
    {
      return MyObjectBuilderSerializer.m_serializersBySerializedName[serializedName];
    }

    private static void SerializeXMLInternal(Stream writeTo, MyObjectBuilder_Base objectBuilder, Type serializeAsType = null)
    {
      MyObjectBuilderSerializer.m_serializersByType[serializeAsType ?? objectBuilder.GetType()].Serialize(writeTo, (object) objectBuilder);
    }

    private static void SerializeGZippedXMLInternal(Stream writeTo, MyObjectBuilder_Base objectBuilder, Type serializeAsType = null)
    {
      using (GZipStream gzipStream = new GZipStream(writeTo, CompressionMode.Compress, true))
      {
        using (BufferedStream bufferedStream = new BufferedStream((Stream) gzipStream, 32768))
          MyObjectBuilderSerializer.SerializeXMLInternal((Stream) bufferedStream, objectBuilder, serializeAsType);
      }
    }

    public static bool SerializeXML(Stream writeTo, MyObjectBuilder_Base objectBuilder, MyObjectBuilderSerializer.XmlCompression compress = MyObjectBuilderSerializer.XmlCompression.Uncompressed, Type serializeAsType = null)
    {
      try
      {
        if (compress == MyObjectBuilderSerializer.XmlCompression.Gzip)
          MyObjectBuilderSerializer.SerializeGZippedXMLInternal(writeTo, objectBuilder, serializeAsType);
        else if (compress == MyObjectBuilderSerializer.XmlCompression.Uncompressed)
          MyObjectBuilderSerializer.SerializeXMLInternal(writeTo, objectBuilder, serializeAsType);
      }
      catch (Exception ex)
      {
        MyLog.Default.WriteLine("Error during serialization.");
        MyLog.Default.WriteLine(((object) ex).ToString());
        return false;
      }
      return true;
    }

    public static bool SerializeXML(string path, bool compress, MyObjectBuilder_Base objectBuilder, Type serializeAsType = null)
    {
      ulong sizeInBytes;
      return MyObjectBuilderSerializer.SerializeXML(path, compress, objectBuilder, out sizeInBytes, serializeAsType);
    }

    public static bool SerializeXML(string path, bool compress, MyObjectBuilder_Base objectBuilder, out ulong sizeInBytes, Type serializeAsType = null)
    {
      try
      {
        using (Stream stream1 = MyFileSystem.OpenWrite(path, FileMode.Create))
        {
          using (Stream stream2 = compress ? StreamExtensions.WrapGZip(stream1, true) : stream1)
          {
            long position = stream1.Position;
            MyObjectBuilderSerializer.m_serializersByType[serializeAsType ?? objectBuilder.GetType()].Serialize(stream2, (object) objectBuilder);
            sizeInBytes = (ulong) (stream1.Position - position);
          }
        }
      }
      catch (Exception ex)
      {
        MyLog.Default.WriteLine("Error: " + path + " failed to serialize.");
        MyLog.Default.WriteLine(((object) ex).ToString());
        sizeInBytes = 0UL;
        return false;
      }
      return true;
    }

    public static bool DeserializeXML<T>(string path, out T objectBuilder) where T : MyObjectBuilder_Base
    {
      ulong fileSize;
      return MyObjectBuilderSerializer.DeserializeXML<T>(path, out objectBuilder, out fileSize);
    }

    public static bool DeserializeXML<T>(string path, out T objectBuilder, out ulong fileSize) where T : MyObjectBuilder_Base
    {
      bool flag = false;
      fileSize = 0UL;
      objectBuilder = default (T);
      using (Stream stream = MyFileSystem.OpenRead(path))
      {
        if (stream != null)
        {
          using (Stream reader = StreamExtensions.UnwrapGZip(stream))
          {
            if (reader != null)
            {
              fileSize = (ulong) stream.Length;
              flag = MyObjectBuilderSerializer.DeserializeXML<T>(reader, out objectBuilder);
            }
          }
        }
      }
      if (!flag)
        MyLog.Default.WriteLine(string.Format("Failed to deserialize file '{0}'", (object) path));
      return flag;
    }

    public static bool DeserializeXML<T>(Stream reader, out T objectBuilder) where T : MyObjectBuilder_Base
    {
      MyObjectBuilder_Base objectBuilder1;
      bool flag = MyObjectBuilderSerializer.DeserializeXML(reader, out objectBuilder1, typeof (T));
      objectBuilder = (T) objectBuilder1;
      return flag;
    }

    private static bool DeserializeXML(Stream reader, out MyObjectBuilder_Base objectBuilder, Type builderType)
    {
      objectBuilder = (MyObjectBuilder_Base) null;
      try
      {
        XmlSerializer xmlSerializer = MyObjectBuilderSerializer.m_serializersByType[builderType];
        objectBuilder = (MyObjectBuilder_Base) xmlSerializer.Deserialize(reader);
      }
      catch (Exception ex)
      {
        MyLog.Default.WriteLine("ERROR: Exception during objectbuilder read! (xml): " + builderType.Name);
        MyLog.Default.WriteLine(ex);
        if (Debugger.IsAttached)
          Debugger.Break();
        return false;
      }
      return true;
    }

    public static bool DeserializeGZippedXML<T>(Stream reader, out T objectBuilder) where T : MyObjectBuilder_Base
    {
      objectBuilder = default (T);
      try
      {
        using (GZipStream gzipStream = new GZipStream(reader, CompressionMode.Decompress))
        {
          using (BufferedStream bufferedStream = new BufferedStream((Stream) gzipStream, 32768))
          {
            XmlSerializer xmlSerializer = MyObjectBuilderSerializer.m_serializersByType[typeof (T)];
            objectBuilder = (T) xmlSerializer.Deserialize((Stream) bufferedStream);
          }
        }
      }
      catch (Exception ex)
      {
        MyLog.Default.WriteLine("ERROR: Exception during objectbuilder read! (xml): " + typeof (T).Name);
        MyLog.Default.WriteLine(ex);
        if (Debugger.IsAttached)
          Debugger.Break();
        return false;
      }
      return true;
    }

    public MyObjectBuilder_Base ChangeType(MyObjectBuilderType type, string subtypeName)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        ((ProtoBuf.Meta.TypeModel) MyObjectBuilderSerializer.Serializer).Serialize((Stream) memoryStream, (object) this);
        memoryStream.Position = 0L;
        MyObjectBuilder_Base newObject = MyObjectBuilderSerializer.CreateNewObject(type, subtypeName);
        return (MyObjectBuilder_Base) ((ProtoBuf.Meta.TypeModel) MyObjectBuilderSerializer.Serializer).Deserialize((Stream) memoryStream, (object) newObject, MyObjectBuilderSerializer.m_objectFactory.GetProducedType(type));
      }
    }

    public static MyObjectBuilder_Base CreateNewObject(SerializableDefinitionId id)
    {
      return MyObjectBuilderSerializer.CreateNewObject(id.TypeId, id.SubtypeId);
    }

    public static MyObjectBuilder_Base CreateNewObject(MyObjectBuilderType type, string subtypeName)
    {
      MyObjectBuilder_Base newObject = MyObjectBuilderSerializer.CreateNewObject(type);
      newObject.SubtypeName = subtypeName;
      return newObject;
    }

    public static MyObjectBuilder_Base CreateNewObject(MyObjectBuilderType type)
    {
      return MyObjectBuilderSerializer.m_objectFactory.CreateInstance(type);
    }

    public static T CreateNewObject<T>(string subtypeName) where T : MyObjectBuilder_Base, new()
    {
      T newObject = MyObjectBuilderSerializer.CreateNewObject<T>();
      newObject.SubtypeName = subtypeName;
      return newObject;
    }

    public static T CreateNewObject<T>() where T : MyObjectBuilder_Base, new()
    {
      return MyObjectBuilderSerializer.m_objectFactory.CreateInstance<T>();
    }

    public static MyObjectBuilder_Base Clone(MyObjectBuilder_Base toClone)
    {
      MyObjectBuilder_Base objectBuilder = (MyObjectBuilder_Base) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        MyObjectBuilderSerializer.SerializeXMLInternal((Stream) memoryStream, toClone, (Type) null);
        memoryStream.Position = 0L;
        MyObjectBuilderSerializer.DeserializeXML((Stream) memoryStream, out objectBuilder, toClone.GetType());
      }
      return objectBuilder;
    }

    public static void MemberwiseAssignment(MyObjectBuilder_Base source, MyObjectBuilder_Base target)
    {
      FieldInfo[] fields1 = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty);
      FieldInfo[] fields2 = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField | BindingFlags.SetProperty);
      foreach (FieldInfo fieldInfo1 in fields1)
      {
        FieldInfo sourceField = fieldInfo1;
        FieldInfo fieldInfo2 = Enumerable.FirstOrDefault<FieldInfo>((IEnumerable<FieldInfo>) fields2, (Func<FieldInfo, bool>) (fieldInfo =>
        {
          if (fieldInfo.FieldType == sourceField.FieldType)
            return fieldInfo.Name == sourceField.Name;
          else
            return false;
        }));
        if (!(fieldInfo2 == (FieldInfo) null))
          fieldInfo2.SetValue((object) target, sourceField.GetValue((object) source));
      }
    }

    public enum XmlCompression
    {
      Uncompressed,
      Gzip,
    }
  }
}
