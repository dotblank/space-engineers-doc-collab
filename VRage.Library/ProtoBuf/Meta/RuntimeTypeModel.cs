// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.RuntimeTypeModel
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ProtoBuf.Meta
{
  public sealed class RuntimeTypeModel : TypeModel
  {
    private readonly BasicList types = new BasicList();
    private int metadataTimeoutMilliseconds = 5000;
    private int contentionCounter = 1;
    private const byte OPTIONS_InferTagFromNameDefault = (byte) 1;
    private const byte OPTIONS_IsDefaultModel = (byte) 2;
    private const byte OPTIONS_Frozen = (byte) 4;
    private const byte OPTIONS_AutoAddMissingTypes = (byte) 8;
    private const byte OPTIONS_AutoCompile = (byte) 16;
    private const byte OPTIONS_UseImplicitZeroDefaults = (byte) 32;
    private const byte OPTIONS_AllowParseableTypes = (byte) 64;
    private const byte OPTIONS_AutoAddProtoContractTypesOnly = (byte) 128;
    private byte options;
    private MethodInfo defaultFactory;

    public bool InferTagFromNameDefault
    {
      get
      {
        return this.GetOption((byte) 1);
      }
      set
      {
        this.SetOption((byte) 1, value);
      }
    }

    public bool AutoAddProtoContractTypesOnly
    {
      get
      {
        return this.GetOption((byte) byte.MinValue);
      }
      set
      {
        this.SetOption((byte) byte.MinValue, value);
      }
    }

    public bool UseImplicitZeroDefaults
    {
      get
      {
        return this.GetOption((byte) 32);
      }
      set
      {
        if (!value && this.GetOption((byte) 2))
          throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
        this.SetOption((byte) 32, value);
      }
    }

    public bool AllowParseableTypes
    {
      get
      {
        return this.GetOption((byte) 64);
      }
      set
      {
        this.SetOption((byte) 64, value);
      }
    }

    public static RuntimeTypeModel Default
    {
      get
      {
        return RuntimeTypeModel.Singleton.Value;
      }
    }

    public MetaType this[Type type]
    {
      get
      {
        return (MetaType) this.types[this.FindOrAddAuto(type, true, false, false)];
      }
    }

    public bool AutoCompile
    {
      get
      {
        return this.GetOption((byte) 16);
      }
      set
      {
        this.SetOption((byte) 16, value);
      }
    }

    public bool AutoAddMissingTypes
    {
      get
      {
        return this.GetOption((byte) 8);
      }
      set
      {
        if (!value && this.GetOption((byte) 2))
          throw new InvalidOperationException("The default model must allow missing types");
        this.ThrowIfFrozen();
        this.SetOption((byte) 8, value);
      }
    }

    public int MetadataTimeoutMilliseconds
    {
      get
      {
        return this.metadataTimeoutMilliseconds;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
        this.metadataTimeoutMilliseconds = value;
      }
    }

    public event LockContentedEventHandler LockContended;

    internal RuntimeTypeModel(bool isDefault)
    {
      this.AutoAddMissingTypes = true;
      this.UseImplicitZeroDefaults = true;
      this.SetOption((byte) 2, isDefault);
      this.AutoCompile = true;
    }

    private bool GetOption(byte option)
    {
      return ((int) this.options & (int) option) == (int) option;
    }

    private void SetOption(byte option, bool value)
    {
    }

    public IEnumerable GetTypes()
    {
      return (IEnumerable) this.types;
    }

    public override string GetSchema(Type type)
    {
      BasicList list = new BasicList();
      MetaType metaType1 = (MetaType) null;
      bool flag = false;
      if (type == (Type) null)
      {
        foreach (MetaType metaType2 in this.types)
        {
          MetaType surrogateOrBaseOrSelf = metaType2.GetSurrogateOrBaseOrSelf(false);
          if (!list.Contains((object) surrogateOrBaseOrSelf))
          {
            list.Add((object) surrogateOrBaseOrSelf);
            this.CascadeDependents(list, surrogateOrBaseOrSelf);
          }
        }
      }
      else
      {
        Type underlyingType = Helpers.GetUnderlyingType(type);
        if (underlyingType != (Type) null)
          type = underlyingType;
        WireType defaultWireType;
        flag = ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out defaultWireType, false, false, false, false) != null;
        if (!flag)
        {
          int orAddAuto = this.FindOrAddAuto(type, false, false, false);
          if (orAddAuto < 0)
            throw new ArgumentException("The type specified is not a contract-type", "type");
          metaType1 = ((MetaType) this.types[orAddAuto]).GetSurrogateOrBaseOrSelf(false);
          list.Add((object) metaType1);
          this.CascadeDependents(list, metaType1);
        }
      }
      StringBuilder builder1 = new StringBuilder();
      string str = (string) null;
      if (!flag)
      {
        foreach (MetaType metaType2 in metaType1 == null ? (IEnumerable) this.types : (IEnumerable) list)
        {
          if (!metaType2.IsList)
          {
            string @namespace = metaType2.Type.Namespace;
            if (!Helpers.IsNullOrEmpty(@namespace) && !@namespace.StartsWith("System."))
            {
              if (str == null)
                str = @namespace;
              else if (!(str == @namespace))
              {
                str = (string) null;
                break;
              }
            }
          }
        }
      }
      if (!Helpers.IsNullOrEmpty(str))
      {
        builder1.Append("package ").Append(str).Append(';');
        Helpers.AppendLine(builder1);
      }
      bool requiresBclImport = false;
      StringBuilder builder2 = new StringBuilder();
      MetaType[] array = new MetaType[list.Count];
      list.CopyTo((Array) array, 0);
      Array.Sort<MetaType>(array, (IComparer<MetaType>) MetaType.Comparer.Default);
      if (flag)
      {
        Helpers.AppendLine(builder2).Append("message ").Append(type.Name).Append(" {");
        MetaType.NewLine(builder2, 1).Append("optional ").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref requiresBclImport)).Append(" value = 1;");
        Helpers.AppendLine(builder2).Append('}');
      }
      else
      {
        for (int index = 0; index < array.Length; ++index)
        {
          MetaType metaType2 = array[index];
          if (!metaType2.IsList || metaType2 == metaType1)
            metaType2.WriteSchema(builder2, 0, ref requiresBclImport);
        }
      }
      if (requiresBclImport)
      {
        builder1.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
        Helpers.AppendLine(builder1);
      }
      return ((object) Helpers.AppendLine(builder1.Append((object) builder2))).ToString();
    }

    private void CascadeDependents(BasicList list, MetaType metaType)
    {
      if (metaType.IsList)
      {
        Type listItemType = TypeModel.GetListItemType((TypeModel) this, metaType.Type);
        WireType defaultWireType;
        if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out defaultWireType, false, false, false, false) != null)
          return;
        int orAddAuto = this.FindOrAddAuto(listItemType, false, false, false);
        if (orAddAuto < 0)
          return;
        MetaType surrogateOrBaseOrSelf = ((MetaType) this.types[orAddAuto]).GetSurrogateOrBaseOrSelf(false);
        if (list.Contains((object) surrogateOrBaseOrSelf))
          return;
        list.Add((object) surrogateOrBaseOrSelf);
        this.CascadeDependents(list, surrogateOrBaseOrSelf);
      }
      else
      {
        if (metaType.IsAutoTuple)
        {
          MemberInfo[] mappedMembers;
          if (MetaType.ResolveTupleConstructor(metaType.Type, out mappedMembers) != (ConstructorInfo) null)
          {
            for (int index = 0; index < mappedMembers.Length; ++index)
            {
              Type type = (Type) null;
              if (mappedMembers[index] is PropertyInfo)
                type = ((PropertyInfo) mappedMembers[index]).PropertyType;
              else if (mappedMembers[index] is FieldInfo)
                type = ((FieldInfo) mappedMembers[index]).FieldType;
              WireType defaultWireType;
              if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out defaultWireType, false, false, false, false) == null)
              {
                int orAddAuto = this.FindOrAddAuto(type, false, false, false);
                if (orAddAuto >= 0)
                {
                  MetaType surrogateOrBaseOrSelf = ((MetaType) this.types[orAddAuto]).GetSurrogateOrBaseOrSelf(false);
                  if (!list.Contains((object) surrogateOrBaseOrSelf))
                  {
                    list.Add((object) surrogateOrBaseOrSelf);
                    this.CascadeDependents(list, surrogateOrBaseOrSelf);
                  }
                }
              }
            }
          }
        }
        else
        {
          foreach (ValueMember valueMember in metaType.Fields)
          {
            Type type = valueMember.ItemType;
            if (type == (Type) null)
              type = valueMember.MemberType;
            WireType defaultWireType;
            if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out defaultWireType, false, false, false, false) == null)
            {
              int orAddAuto = this.FindOrAddAuto(type, false, false, false);
              if (orAddAuto >= 0)
              {
                MetaType surrogateOrBaseOrSelf = ((MetaType) this.types[orAddAuto]).GetSurrogateOrBaseOrSelf(false);
                if (!list.Contains((object) surrogateOrBaseOrSelf))
                {
                  list.Add((object) surrogateOrBaseOrSelf);
                  this.CascadeDependents(list, surrogateOrBaseOrSelf);
                }
              }
            }
          }
        }
        if (metaType.HasSubtypes)
        {
          foreach (SubType subType in metaType.GetSubtypes())
          {
            MetaType surrogateOrSelf = subType.DerivedType.GetSurrogateOrSelf();
            if (!list.Contains((object) surrogateOrSelf))
            {
              list.Add((object) surrogateOrSelf);
              this.CascadeDependents(list, surrogateOrSelf);
            }
          }
        }
        MetaType metaType1 = metaType.BaseType;
        if (metaType1 != null)
          metaType1 = metaType1.GetSurrogateOrSelf();
        if (metaType1 == null || list.Contains((object) metaType1))
          return;
        list.Add((object) metaType1);
        this.CascadeDependents(list, metaType1);
      }
    }

    internal MetaType FindWithoutAdd(Type type)
    {
      foreach (MetaType type1 in this.types)
      {
        if (type1.Type == type)
        {
          if (type1.Pending)
            this.WaitOnLock(type1);
          return type1;
        }
      }
      Type type2 = TypeModel.ResolveProxies(type);
      if (!(type2 == (Type) null))
        return this.FindWithoutAdd(type2);
      else
        return (MetaType) null;
    }

    private void WaitOnLock(MetaType type)
    {
      int opaqueToken = 0;
      try
      {
        this.TakeLock(ref opaqueToken);
      }
      finally
      {
        this.ReleaseLock(opaqueToken);
      }
    }

    internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
    {
      RuntimeTypeModel.TypeFinder typeFinder = new RuntimeTypeModel.TypeFinder(type);
      int index = this.types.IndexOf((BasicList.IPredicate) typeFinder);
      MetaType type1;
      if (index >= 0 && (type1 = (MetaType) this.types[index]).Pending)
        this.WaitOnLock(type1);
      if (index < 0)
      {
        Type type2 = TypeModel.ResolveProxies(type);
        if (type2 != (Type) null)
        {
          typeFinder = new RuntimeTypeModel.TypeFinder(type2);
          index = this.types.IndexOf((BasicList.IPredicate) typeFinder);
          type = type2;
        }
      }
      if (index < 0)
      {
        int opaqueToken = 0;
        try
        {
          this.TakeLock(ref opaqueToken);
          MetaType metaType;
          if ((metaType = this.RecogniseCommonTypes(type)) == null)
          {
            MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, (AttributeMap[]) null);
            if (contractFamily == MetaType.AttributeFamily.AutoTuple)
              addEvenIfAutoDisabled = true;
            if (!this.AutoAddMissingTypes && !addEvenIfAutoDisabled || !Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None)
            {
              if (demand)
                TypeModel.ThrowUnexpectedType(type);
              return index;
            }
            else
              metaType = this.Create(type);
          }
          metaType.Pending = true;
          bool flag = false;
          int num = this.types.IndexOf((BasicList.IPredicate) typeFinder);
          if (num < 0)
          {
            this.ThrowIfFrozen();
            index = this.types.Add((object) metaType);
            flag = true;
          }
          else
            index = num;
          if (flag)
          {
            metaType.ApplyDefaultBehaviour();
            metaType.Pending = false;
          }
        }
        finally
        {
          this.ReleaseLock(opaqueToken);
        }
      }
      return index;
    }

    private MetaType RecogniseCommonTypes(Type type)
    {
      return (MetaType) null;
    }

    private MetaType Create(Type type)
    {
      this.ThrowIfFrozen();
      return new MetaType(this, type, this.defaultFactory);
    }

    public MetaType Add(Type type, bool applyDefaultBehaviour)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      MetaType metaType = this.FindWithoutAdd(type);
      if (metaType != null)
        return metaType;
      int opaqueToken = 0;
      if (type.IsInterface)
      {
        if (this.MapType(MetaType.ienumerable).IsAssignableFrom(type))
        {
          if (TypeModel.GetListItemType((TypeModel) this, type) == (Type) null)
            throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
        }
      }
      try
      {
        metaType = this.RecogniseCommonTypes(type);
        if (metaType != null)
        {
          if (!applyDefaultBehaviour)
            throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.FullName, "applyDefaultBehaviour");
          applyDefaultBehaviour = false;
        }
        if (metaType == null)
          metaType = this.Create(type);
        metaType.Pending = true;
        this.TakeLock(ref opaqueToken);
        if (this.FindWithoutAdd(type) != null)
          throw new ArgumentException("Duplicate type", "type");
        this.ThrowIfFrozen();
        this.types.Add((object) metaType);
        if (applyDefaultBehaviour)
          metaType.ApplyDefaultBehaviour();
        metaType.Pending = false;
      }
      finally
      {
        this.ReleaseLock(opaqueToken);
      }
      return metaType;
    }

    private void ThrowIfFrozen()
    {
      if (this.GetOption((byte) 4))
        throw new InvalidOperationException("The model cannot be changed once frozen");
    }

    public void Freeze()
    {
      if (this.GetOption((byte) 2))
        throw new InvalidOperationException("The default model cannot be frozen");
      this.SetOption((byte) 4, true);
    }

    protected override int GetKeyImpl(Type type)
    {
      return this.GetKey(type, false, true);
    }

    internal int GetKey(Type type, bool demand, bool getBaseKey)
    {
      try
      {
        int orAddAuto = this.FindOrAddAuto(type, demand, true, false);
        if (orAddAuto >= 0)
        {
          MetaType source = (MetaType) this.types[orAddAuto];
          if (getBaseKey)
            orAddAuto = this.FindOrAddAuto(MetaType.GetRootType(source).Type, true, true, false);
        }
        return orAddAuto;
      }
      catch (NotSupportedException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        if (ex.Message.IndexOf(type.FullName) < 0)
          throw new ProtoException(ex.Message + " (" + type.FullName + ")", ex);
        throw;
      }
    }

    protected internal override void Serialize(int key, object value, ProtoWriter dest)
    {
      ((MetaType) this.types[key]).Serializer.Write(value, dest);
    }

    protected internal override object Deserialize(int key, object value, ProtoReader source)
    {
      IProtoSerializer protoSerializer = (IProtoSerializer) ((MetaType) this.types[key]).Serializer;
      if (value != null || !Helpers.IsValueType(protoSerializer.ExpectedType) || !protoSerializer.RequiresOldValue)
        return protoSerializer.Read(value, source);
      value = Activator.CreateInstance(protoSerializer.ExpectedType);
      return protoSerializer.Read(value, source);
    }

    internal ProtoSerializer GetSerializer(IProtoSerializer serializer, bool compiled)
    {
      if (serializer == null)
        throw new ArgumentNullException("serializer");
      if (compiled)
        return CompilerContext.BuildSerializer(serializer, (TypeModel) this);
      else
        return new ProtoSerializer(serializer.Write);
    }

    public void CompileInPlace()
    {
      foreach (MetaType metaType in this.types)
        metaType.CompileInPlace();
    }

    private void BuildAllSerializers()
    {
      for (int index = 0; index < this.types.Count; ++index)
      {
        MetaType metaType = (MetaType) this.types[index];
        if (metaType.Serializer == null)
          throw new InvalidOperationException("No serializer available for " + metaType.Type.Name);
      }
    }

    public TypeModel Compile()
    {
      return this.Compile((string) null, (string) null);
    }

    private static ILGenerator Override(TypeBuilder type, string name)
    {
      MethodInfo method = type.BaseType.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
      ParameterInfo[] parameters = method.GetParameters();
      Type[] parameterTypes = new Type[parameters.Length];
      for (int index = 0; index < parameterTypes.Length; ++index)
        parameterTypes[index] = parameters[index].ParameterType;
      MethodBuilder methodBuilder = type.DefineMethod(method.Name, method.Attributes & ~MethodAttributes.Abstract | MethodAttributes.Final, method.CallingConvention, method.ReturnType, parameterTypes);
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      type.DefineMethodOverride((MethodInfo) methodBuilder, method);
      return ilGenerator;
    }

    public TypeModel Compile(string name, string path)
    {
      return this.Compile(new RuntimeTypeModel.CompilerOptions()
      {
        TypeName = name,
        OutputPath = path
      });
    }

    public TypeModel Compile(RuntimeTypeModel.CompilerOptions options)
    {
      if (options == null)
        throw new ArgumentNullException("options");
      string name1 = options.TypeName;
      string outputPath = options.OutputPath;
      this.BuildAllSerializers();
      this.Freeze();
      bool flag1 = !Helpers.IsNullOrEmpty(outputPath);
      if (Helpers.IsNullOrEmpty(name1))
      {
        if (flag1)
          throw new ArgumentNullException("typeName");
        name1 = Guid.NewGuid().ToString();
      }
      string assemblyName;
      string name2;
      if (outputPath == null)
      {
        assemblyName = name1;
        name2 = assemblyName + ".dll";
      }
      else
      {
        assemblyName = new FileInfo(Path.GetFileNameWithoutExtension(outputPath)).Name;
        name2 = assemblyName + Path.GetExtension(outputPath);
      }
      AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName()
      {
        Name = assemblyName
      }, flag1 ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
      ModuleBuilder moduleBuilder = flag1 ? assemblyBuilder.DefineDynamicModule(name2, outputPath) : assemblyBuilder.DefineDynamicModule(name2);
      if (!Helpers.IsNullOrEmpty(options.TargetFrameworkName))
      {
        Type type = (Type) null;
        try
        {
          type = this.GetType("System.Runtime.Versioning.TargetFrameworkAttribute", this.MapType(typeof (string)).Assembly);
        }
        catch
        {
        }
        if (type != (Type) null)
        {
          PropertyInfo[] namedProperties;
          object[] propertyValues;
          if (Helpers.IsNullOrEmpty(options.TargetFrameworkDisplayName))
          {
            namedProperties = new PropertyInfo[0];
            propertyValues = new object[0];
          }
          else
          {
            namedProperties = new PropertyInfo[1]
            {
              type.GetProperty("FrameworkDisplayName")
            };
            propertyValues = new object[1]
            {
              (object) options.TargetFrameworkDisplayName
            };
          }
          CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(type.GetConstructor(new Type[1]
          {
            this.MapType(typeof (string))
          }), new object[1]
          {
            (object) options.TargetFrameworkName
          }, namedProperties, propertyValues);
          assemblyBuilder.SetCustomAttribute(customBuilder);
        }
      }
      Type type1 = (Type) null;
      try
      {
        type1 = this.MapType(typeof (InternalsVisibleToAttribute));
      }
      catch
      {
      }
      if (type1 != (Type) null)
      {
        BasicList basicList1 = new BasicList();
        BasicList basicList2 = new BasicList();
        foreach (MetaType metaType in this.types)
        {
          Assembly assembly = metaType.Type.Assembly;
          if (basicList2.IndexOfReference((object) assembly) < 0)
          {
            basicList2.Add((object) assembly);
            AttributeMap[] attributeMapArray = AttributeMap.Create((TypeModel) this, assembly);
            for (int index = 0; index < attributeMapArray.Length; ++index)
            {
              if (!(attributeMapArray[index].AttributeType != type1))
              {
                object obj;
                attributeMapArray[index].TryGet("AssemblyName", out obj);
                string str = obj as string;
                if (!(str == assemblyName) && !Helpers.IsNullOrEmpty(str) && basicList1.IndexOf((BasicList.IPredicate) new RuntimeTypeModel.StringFinder(str)) < 0)
                {
                  basicList1.Add((object) str);
                  CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(type1.GetConstructor(new Type[1]
                  {
                    this.MapType(typeof (string))
                  }), new object[1]
                  {
                    (object) str
                  });
                  assemblyBuilder.SetCustomAttribute(customBuilder);
                }
              }
            }
          }
        }
      }
      Type parent = this.MapType(typeof (TypeModel));
      TypeAttributes attr = parent.Attributes & ~TypeAttributes.Abstract | TypeAttributes.Sealed;
      if (options.Accessibility == RuntimeTypeModel.Accessibility.Internal)
        attr &= ~TypeAttributes.Public;
      TypeBuilder type2 = moduleBuilder.DefineType(name1, attr, parent);
      int num1 = 0;
      bool flag2 = false;
      RuntimeTypeModel.SerializerPair[] serializerPairArray = new RuntimeTypeModel.SerializerPair[this.types.Count];
      foreach (MetaType type3 in this.types)
      {
        MethodBuilder serialize = type2.DefineMethod("Write", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard, this.MapType(typeof (void)), new Type[2]
        {
          type3.Type,
          this.MapType(typeof (ProtoWriter))
        });
        MethodBuilder deserialize = type2.DefineMethod("Read", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard, type3.Type, new Type[2]
        {
          type3.Type,
          this.MapType(typeof (ProtoReader))
        });
        RuntimeTypeModel.SerializerPair serializerPair = new RuntimeTypeModel.SerializerPair(this.GetKey(type3.Type, true, false), this.GetKey(type3.Type, true, true), type3, serialize, deserialize, serialize.GetILGenerator(), deserialize.GetILGenerator());
        serializerPairArray[num1++] = serializerPair;
        if (serializerPair.MetaKey != serializerPair.BaseKey)
          flag2 = true;
      }
      if (flag2)
        Array.Sort<RuntimeTypeModel.SerializerPair>(serializerPairArray);
      CompilerContext.ILVersion ilVersion = CompilerContext.ILVersion.Net2;
      if (options.MetaDataVersion == 65536)
        ilVersion = CompilerContext.ILVersion.Net1;
      for (int index = 0; index < serializerPairArray.Length; ++index)
      {
        RuntimeTypeModel.SerializerPair serializerPair = serializerPairArray[index];
        CompilerContext ctx1 = new CompilerContext(serializerPair.SerializeBody, true, true, serializerPairArray, (TypeModel) this, ilVersion, assemblyName);
        ctx1.CheckAccessibility((MemberInfo) serializerPair.Deserialize.ReturnType);
        serializerPair.Type.Serializer.EmitWrite(ctx1, Local.InputValue);
        ctx1.Return();
        CompilerContext ctx2 = new CompilerContext(serializerPair.DeserializeBody, true, false, serializerPairArray, (TypeModel) this, ilVersion, assemblyName);
        serializerPair.Type.Serializer.EmitRead(ctx2, Local.InputValue);
        if (!serializerPair.Type.Serializer.ReturnsValue)
          ctx2.LoadValue(Local.InputValue);
        ctx2.Return();
      }
      ILGenerator il1 = RuntimeTypeModel.Override(type2, "GetKeyImpl");
      int num2;
      Type type4;
      if (this.types.Count <= 20)
      {
        num2 = 1;
        type4 = this.MapType(typeof (Type[]), true);
      }
      else
      {
        type4 = this.MapType(typeof (Dictionary<Type, int>), false);
        if (type4 == (Type) null)
        {
          type4 = this.MapType(typeof (Hashtable), true);
          num2 = 3;
        }
        else
          num2 = 2;
      }
      FieldBuilder fieldBuilder = type2.DefineField("knownTypes", type4, FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly);
      switch (num2)
      {
        case 1:
          il1.Emit(OpCodes.Ldsfld, (FieldInfo) fieldBuilder);
          il1.Emit(OpCodes.Ldarg_1);
          il1.EmitCall(OpCodes.Callvirt, this.MapType(typeof (IList)).GetMethod("IndexOf", new Type[1]
          {
            this.MapType(typeof (object))
          }), (Type[]) null);
          if (flag2)
          {
            il1.DeclareLocal(this.MapType(typeof (int)));
            il1.Emit(OpCodes.Dup);
            il1.Emit(OpCodes.Stloc_0);
            BasicList basicList = new BasicList();
            int num3 = -1;
            for (int index = 0; index < serializerPairArray.Length && serializerPairArray[index].MetaKey != serializerPairArray[index].BaseKey; ++index)
            {
              if (num3 == serializerPairArray[index].BaseKey)
              {
                basicList.Add(basicList[basicList.Count - 1]);
              }
              else
              {
                basicList.Add((object) il1.DefineLabel());
                num3 = serializerPairArray[index].BaseKey;
              }
            }
            Label[] labels = new Label[basicList.Count];
            basicList.CopyTo((Array) labels, 0);
            il1.Emit(OpCodes.Switch, labels);
            il1.Emit(OpCodes.Ldloc_0);
            il1.Emit(OpCodes.Ret);
            int num4 = -1;
            for (int index = labels.Length - 1; index >= 0; --index)
            {
              if (num4 != serializerPairArray[index].BaseKey)
              {
                num4 = serializerPairArray[index].BaseKey;
                int num5 = -1;
                for (int length = labels.Length; length < serializerPairArray.Length; ++length)
                {
                  if (serializerPairArray[length].BaseKey == num4 && serializerPairArray[length].MetaKey == num4)
                  {
                    num5 = length;
                    break;
                  }
                }
                il1.MarkLabel(labels[index]);
                CompilerContext.LoadValue(il1, num5);
                il1.Emit(OpCodes.Ret);
              }
            }
            break;
          }
          else
          {
            il1.Emit(OpCodes.Ret);
            break;
          }
        case 2:
          LocalBuilder local = il1.DeclareLocal(this.MapType(typeof (int)));
          Label label1 = il1.DefineLabel();
          il1.Emit(OpCodes.Ldsfld, (FieldInfo) fieldBuilder);
          il1.Emit(OpCodes.Ldarg_1);
          il1.Emit(OpCodes.Ldloca_S, local);
          il1.EmitCall(OpCodes.Callvirt, type4.GetMethod("TryGetValue", BindingFlags.Instance | BindingFlags.Public), (Type[]) null);
          il1.Emit(OpCodes.Brfalse_S, label1);
          il1.Emit(OpCodes.Ldloc_S, local);
          il1.Emit(OpCodes.Ret);
          il1.MarkLabel(label1);
          il1.Emit(OpCodes.Ldc_I4_M1);
          il1.Emit(OpCodes.Ret);
          break;
        case 3:
          Label label2 = il1.DefineLabel();
          il1.Emit(OpCodes.Ldsfld, (FieldInfo) fieldBuilder);
          il1.Emit(OpCodes.Ldarg_1);
          il1.EmitCall(OpCodes.Callvirt, type4.GetProperty("Item").GetGetMethod(), (Type[]) null);
          il1.Emit(OpCodes.Dup);
          il1.Emit(OpCodes.Brfalse_S, label2);
          if (ilVersion == CompilerContext.ILVersion.Net1)
          {
            il1.Emit(OpCodes.Unbox, this.MapType(typeof (int)));
            il1.Emit(OpCodes.Ldobj, this.MapType(typeof (int)));
          }
          else
            il1.Emit(OpCodes.Unbox_Any, this.MapType(typeof (int)));
          il1.Emit(OpCodes.Ret);
          il1.MarkLabel(label2);
          il1.Emit(OpCodes.Pop);
          il1.Emit(OpCodes.Ldc_I4_M1);
          il1.Emit(OpCodes.Ret);
          break;
        default:
          throw new InvalidOperationException();
      }
      ILGenerator il2 = RuntimeTypeModel.Override(type2, "Serialize");
      CompilerContext compilerContext1 = new CompilerContext(il2, false, true, serializerPairArray, (TypeModel) this, ilVersion, assemblyName);
      Label[] labels1 = new Label[this.types.Count];
      for (int index = 0; index < labels1.Length; ++index)
        labels1[index] = il2.DefineLabel();
      il2.Emit(OpCodes.Ldarg_1);
      il2.Emit(OpCodes.Switch, labels1);
      compilerContext1.Return();
      for (int index = 0; index < labels1.Length; ++index)
      {
        RuntimeTypeModel.SerializerPair serializerPair = serializerPairArray[index];
        il2.MarkLabel(labels1[index]);
        il2.Emit(OpCodes.Ldarg_2);
        compilerContext1.CastFromObject(serializerPair.Type.Type);
        il2.Emit(OpCodes.Ldarg_3);
        il2.EmitCall(OpCodes.Call, (MethodInfo) serializerPair.Serialize, (Type[]) null);
        compilerContext1.Return();
      }
      ILGenerator il3 = RuntimeTypeModel.Override(type2, "Deserialize");
      CompilerContext compilerContext2 = new CompilerContext(il3, false, false, serializerPairArray, (TypeModel) this, ilVersion, assemblyName);
      for (int index = 0; index < labels1.Length; ++index)
        labels1[index] = il3.DefineLabel();
      il3.Emit(OpCodes.Ldarg_1);
      il3.Emit(OpCodes.Switch, labels1);
      compilerContext2.LoadNullRef();
      compilerContext2.Return();
      for (int i = 0; i < labels1.Length; ++i)
      {
        RuntimeTypeModel.SerializerPair serializerPair = serializerPairArray[i];
        il3.MarkLabel(labels1[i]);
        Type type3 = serializerPair.Type.Type;
        if (type3.IsValueType)
        {
          il3.Emit(OpCodes.Ldarg_2);
          il3.Emit(OpCodes.Ldarg_3);
          il3.EmitCall(OpCodes.Call, (MethodInfo) RuntimeTypeModel.EmitBoxedSerializer(type2, i, type3, serializerPairArray, (TypeModel) this, ilVersion, assemblyName), (Type[]) null);
          compilerContext2.Return();
        }
        else
        {
          il3.Emit(OpCodes.Ldarg_2);
          compilerContext2.CastFromObject(type3);
          il3.Emit(OpCodes.Ldarg_3);
          il3.EmitCall(OpCodes.Call, (MethodInfo) serializerPair.Deserialize, (Type[]) null);
          compilerContext2.Return();
        }
      }
      type2.DefineDefaultConstructor(MethodAttributes.Public);
      ILGenerator ilGenerator = type2.DefineTypeInitializer().GetILGenerator();
      switch (num2)
      {
        case 1:
          CompilerContext.LoadValue(ilGenerator, this.types.Count);
          ilGenerator.Emit(OpCodes.Newarr, compilerContext2.MapType(typeof (Type)));
          int num6 = 0;
          foreach (RuntimeTypeModel.SerializerPair serializerPair in serializerPairArray)
          {
            ilGenerator.Emit(OpCodes.Dup);
            CompilerContext.LoadValue(ilGenerator, num6);
            ilGenerator.Emit(OpCodes.Ldtoken, serializerPair.Type.Type);
            ilGenerator.EmitCall(OpCodes.Call, compilerContext2.MapType(typeof (Type)).GetMethod("GetTypeFromHandle"), (Type[]) null);
            ilGenerator.Emit(OpCodes.Stelem_Ref);
            ++num6;
          }
          ilGenerator.Emit(OpCodes.Stsfld, (FieldInfo) fieldBuilder);
          ilGenerator.Emit(OpCodes.Ret);
          break;
        case 2:
          CompilerContext.LoadValue(ilGenerator, this.types.Count);
          ilGenerator.DeclareLocal(type4);
          ilGenerator.Emit(OpCodes.Newobj, type4.GetConstructor(new Type[1]
          {
            this.MapType(typeof (int))
          }));
          ilGenerator.Emit(OpCodes.Stsfld, (FieldInfo) fieldBuilder);
          int num7 = 0;
          foreach (RuntimeTypeModel.SerializerPair serializerPair in serializerPairArray)
          {
            ilGenerator.Emit(OpCodes.Ldsfld, (FieldInfo) fieldBuilder);
            ilGenerator.Emit(OpCodes.Ldtoken, serializerPair.Type.Type);
            ilGenerator.EmitCall(OpCodes.Call, compilerContext2.MapType(typeof (Type)).GetMethod("GetTypeFromHandle"), (Type[]) null);
            int num3 = num7++;
            int num4 = serializerPair.BaseKey;
            if (num4 != serializerPair.MetaKey)
            {
              num3 = -1;
              for (int index = 0; index < serializerPairArray.Length; ++index)
              {
                if (serializerPairArray[index].BaseKey == num4 && serializerPairArray[index].MetaKey == num4)
                {
                  num3 = index;
                  break;
                }
              }
            }
            CompilerContext.LoadValue(ilGenerator, num3);
            ilGenerator.EmitCall(OpCodes.Callvirt, type4.GetMethod("Add", new Type[2]
            {
              this.MapType(typeof (Type)),
              this.MapType(typeof (int))
            }), (Type[]) null);
          }
          ilGenerator.Emit(OpCodes.Ret);
          break;
        case 3:
          CompilerContext.LoadValue(ilGenerator, this.types.Count);
          ilGenerator.Emit(OpCodes.Newobj, type4.GetConstructor(new Type[1]
          {
            this.MapType(typeof (int))
          }));
          ilGenerator.Emit(OpCodes.Stsfld, (FieldInfo) fieldBuilder);
          int num8 = 0;
          foreach (RuntimeTypeModel.SerializerPair serializerPair in serializerPairArray)
          {
            ilGenerator.Emit(OpCodes.Ldsfld, (FieldInfo) fieldBuilder);
            ilGenerator.Emit(OpCodes.Ldtoken, serializerPair.Type.Type);
            ilGenerator.EmitCall(OpCodes.Call, compilerContext2.MapType(typeof (Type)).GetMethod("GetTypeFromHandle"), (Type[]) null);
            int num3 = num8++;
            int num4 = serializerPair.BaseKey;
            if (num4 != serializerPair.MetaKey)
            {
              num3 = -1;
              for (int index = 0; index < serializerPairArray.Length; ++index)
              {
                if (serializerPairArray[index].BaseKey == num4 && serializerPairArray[index].MetaKey == num4)
                {
                  num3 = index;
                  break;
                }
              }
            }
            CompilerContext.LoadValue(ilGenerator, num3);
            ilGenerator.Emit(OpCodes.Box, this.MapType(typeof (int)));
            ilGenerator.EmitCall(OpCodes.Callvirt, type4.GetMethod("Add", new Type[2]
            {
              this.MapType(typeof (object)),
              this.MapType(typeof (object))
            }), (Type[]) null);
          }
          ilGenerator.Emit(OpCodes.Ret);
          break;
        default:
          throw new InvalidOperationException();
      }
      Type type5 = type2.CreateType();
      if (!Helpers.IsNullOrEmpty(outputPath))
        assemblyBuilder.Save(outputPath);
      return (TypeModel) Activator.CreateInstance(type5);
    }

    private static MethodBuilder EmitBoxedSerializer(TypeBuilder type, int i, Type valueType, RuntimeTypeModel.SerializerPair[] methodPairs, TypeModel model, CompilerContext.ILVersion ilVersion, string assemblyName)
    {
      MethodInfo method = (MethodInfo) methodPairs[i].Deserialize;
      MethodBuilder methodBuilder = type.DefineMethod("_" + (object) i, MethodAttributes.Static, CallingConventions.Standard, model.MapType(typeof (object)), new Type[2]
      {
        model.MapType(typeof (object)),
        model.MapType(typeof (ProtoReader))
      });
      CompilerContext ctx = new CompilerContext(methodBuilder.GetILGenerator(), true, false, methodPairs, model, ilVersion, assemblyName);
      ctx.LoadValue(Local.InputValue);
      CodeLabel label = ctx.DefineLabel();
      ctx.BranchIfFalse(label, true);
      Type type1 = valueType;
      ctx.LoadValue(Local.InputValue);
      ctx.CastFromObject(type1);
      ctx.LoadReaderWriter();
      ctx.EmitCall(method);
      ctx.CastToObject(type1);
      ctx.Return();
      ctx.MarkLabel(label);
      using (Local local = new Local(ctx, type1))
      {
        ctx.LoadAddress(local, type1);
        ctx.EmitCtor(type1);
        ctx.LoadValue(local);
        ctx.LoadReaderWriter();
        ctx.EmitCall(method);
        ctx.CastToObject(type1);
        ctx.Return();
      }
      return methodBuilder;
    }

    internal bool IsDefined(Type type, int fieldNumber)
    {
      return this.FindWithoutAdd(type).IsDefined(fieldNumber);
    }

    internal bool IsPrepared(Type type)
    {
      MetaType withoutAdd = this.FindWithoutAdd(type);
      if (withoutAdd != null)
        return withoutAdd.IsPrepared();
      else
        return false;
    }

    internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
    {
      int orAddAuto = this.FindOrAddAuto(type, false, false, false);
      if (orAddAuto >= 0)
        return ((MetaType) this.types[orAddAuto]).GetEnumMap();
      else
        return (EnumSerializer.EnumPair[]) null;
    }

    internal void TakeLock(ref int opaqueToken)
    {
      opaqueToken = 0;
      if (Monitor.TryEnter((object) this.types, this.metadataTimeoutMilliseconds))
      {
        opaqueToken = this.GetContention();
      }
      else
      {
        this.AddContention();
        throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
      }
    }

    private int GetContention()
    {
      return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
    }

    private void AddContention()
    {
      Interlocked.Increment(ref this.contentionCounter);
    }

    internal void ReleaseLock(int opaqueToken)
    {
      if (opaqueToken == 0)
        return;
      Monitor.Exit((object) this.types);
      if (opaqueToken == this.GetContention())
        return;
      LockContentedEventHandler contentedEventHandler = this.LockContended;
      if (contentedEventHandler == null)
        return;
      string stackTrace;
      try
      {
        throw new Exception();
      }
      catch (Exception ex)
      {
        stackTrace = ex.StackTrace;
      }
      contentedEventHandler((object) this, new LockContentedEventArgs(stackTrace));
    }

    internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
    {
      if (type == (Type) null || Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown || this[type].IgnoreListHandling)
        return;
      if (type.IsArray)
      {
        if (type.GetArrayRank() != 1)
          throw new NotSupportedException("Multi-dimension arrays are supported");
        itemType = type.GetElementType();
        defaultType = !(itemType == this.MapType(typeof (byte))) ? type : (itemType = (Type) null);
      }
      if (itemType == (Type) null)
        itemType = TypeModel.GetListItemType((TypeModel) this, type);
      if (itemType != (Type) null)
      {
        Type itemType1 = (Type) null;
        Type defaultType1 = (Type) null;
        this.ResolveListTypes(itemType, ref itemType1, ref defaultType1);
        if (itemType1 != (Type) null)
          throw TypeModel.CreateNestedListsNotSupported();
      }
      if (!(itemType != (Type) null) || !(defaultType == (Type) null))
        return;
      if (type.IsClass && !type.IsAbstract && Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != (ConstructorInfo) null)
        defaultType = type;
      if (defaultType == (Type) null && type.IsInterface)
      {
        Type[] genericArguments;
        if (type.IsGenericType && type.GetGenericTypeDefinition() == this.MapType(typeof (IDictionary<,>)) && itemType == this.MapType(typeof (KeyValuePair<,>)).MakeGenericType(genericArguments = type.GetGenericArguments()))
          defaultType = this.MapType(typeof (Dictionary<,>)).MakeGenericType(genericArguments);
        else
          defaultType = this.MapType(typeof (List<>)).MakeGenericType(itemType);
      }
      if (!(defaultType != (Type) null) || Helpers.IsAssignableFrom(type, defaultType))
        return;
      defaultType = (Type) null;
    }

    internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
    {
      Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
      if (underlyingType != (Type) null)
        effectiveType = underlyingType;
      if (effectiveType == this.MapType(typeof (byte[])))
        return "bytes";
      WireType defaultWireType;
      IProtoSerializer coreSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out defaultWireType, false, false, false, false);
      if (coreSerializer == null)
      {
        if (!asReference && !dynamicType)
          return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
        requiresBclImport = true;
        return "bcl.NetObjectProxy";
      }
      else if (coreSerializer is ParseableSerializer)
      {
        if (asReference)
          requiresBclImport = true;
        return !asReference ? "string" : "bcl.NetObjectProxy";
      }
      else
      {
        switch (Helpers.GetTypeCode(effectiveType))
        {
          case ProtoTypeCode.Boolean:
            return "bool";
          case ProtoTypeCode.Char:
          case ProtoTypeCode.Byte:
          case ProtoTypeCode.UInt16:
          case ProtoTypeCode.UInt32:
            return dataFormat == DataFormat.FixedSize ? "fixed32" : "uint32";
          case ProtoTypeCode.SByte:
          case ProtoTypeCode.Int16:
          case ProtoTypeCode.Int32:
            switch (dataFormat)
            {
              case DataFormat.ZigZag:
                return "sint32";
              case DataFormat.FixedSize:
                return "sfixed32";
              default:
                return "int32";
            }
          case ProtoTypeCode.Int64:
            switch (dataFormat)
            {
              case DataFormat.ZigZag:
                return "sint64";
              case DataFormat.FixedSize:
                return "sfixed64";
              default:
                return "int64";
            }
          case ProtoTypeCode.UInt64:
            return dataFormat == DataFormat.FixedSize ? "fixed64" : "uint64";
          case ProtoTypeCode.Single:
            return "float";
          case ProtoTypeCode.Double:
            return "double";
          case ProtoTypeCode.Decimal:
            requiresBclImport = true;
            return "bcl.Decimal";
          case ProtoTypeCode.DateTime:
            requiresBclImport = true;
            return "bcl.DateTime";
          case ProtoTypeCode.String:
            if (asReference)
              requiresBclImport = true;
            return !asReference ? "string" : "bcl.NetObjectProxy";
          case ProtoTypeCode.TimeSpan:
            requiresBclImport = true;
            return "bcl.TimeSpan";
          case ProtoTypeCode.Guid:
            requiresBclImport = true;
            return "bcl.Guid";
          default:
            throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
        }
      }
    }

    public void SetDefaultFactory(MethodInfo methodInfo)
    {
      this.VerifyFactory(methodInfo, (Type) null);
      this.defaultFactory = methodInfo;
    }

    internal void VerifyFactory(MethodInfo factory, Type type)
    {
      if (!(factory != (MethodInfo) null))
        return;
      if (type != (Type) null && Helpers.IsValueType(type))
        throw new InvalidOperationException();
      if (!factory.IsStatic)
        throw new ArgumentException("A factory-method must be static", "factory");
      if (type != (Type) null && factory.ReturnType != type && factory.ReturnType != this.MapType(typeof (object)))
        throw new ArgumentException("The factory-method must return object" + (type == (Type) null ? "" : " or " + type.FullName), "factory");
      if (!CallbackSet.CheckCallbackParameters((TypeModel) this, factory))
        throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
    }

    private class Singleton
    {
      internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);

      private Singleton()
      {
      }
    }

    private sealed class TypeFinder : BasicList.IPredicate
    {
      private readonly Type type;

      public TypeFinder(Type type)
      {
        this.type = type;
      }

      public bool IsMatch(object obj)
      {
        return ((MetaType) obj).Type == this.type;
      }
    }

    internal class SerializerPair : IComparable
    {
      public readonly int MetaKey;
      public readonly int BaseKey;
      public readonly MetaType Type;
      public readonly MethodBuilder Serialize;
      public readonly MethodBuilder Deserialize;
      public readonly ILGenerator SerializeBody;
      public readonly ILGenerator DeserializeBody;

      public SerializerPair(int metaKey, int baseKey, MetaType type, MethodBuilder serialize, MethodBuilder deserialize, ILGenerator serializeBody, ILGenerator deserializeBody)
      {
        this.MetaKey = metaKey;
        this.BaseKey = baseKey;
        this.Serialize = serialize;
        this.Deserialize = deserialize;
        this.SerializeBody = serializeBody;
        this.DeserializeBody = deserializeBody;
        this.Type = type;
      }

      int IComparable.CompareTo(object obj)
      {
        if (obj == null)
          throw new ArgumentException("obj");
        RuntimeTypeModel.SerializerPair serializerPair = (RuntimeTypeModel.SerializerPair) obj;
        if (this.BaseKey == this.MetaKey)
        {
          if (serializerPair.BaseKey == serializerPair.MetaKey)
            return this.MetaKey.CompareTo(serializerPair.MetaKey);
          else
            return 1;
        }
        else
        {
          if (serializerPair.BaseKey == serializerPair.MetaKey)
            return -1;
          int num = this.BaseKey.CompareTo(serializerPair.BaseKey);
          if (num == 0)
            num = this.MetaKey.CompareTo(serializerPair.MetaKey);
          return num;
        }
      }
    }

    public sealed class CompilerOptions
    {
      private string targetFrameworkName;
      private string targetFrameworkDisplayName;
      private string typeName;
      private string outputPath;
      private string imageRuntimeVersion;
      private int metaDataVersion;
      private RuntimeTypeModel.Accessibility accessibility;

      public string TargetFrameworkName
      {
        get
        {
          return this.targetFrameworkName;
        }
        set
        {
          this.targetFrameworkName = value;
        }
      }

      public string TargetFrameworkDisplayName
      {
        get
        {
          return this.targetFrameworkDisplayName;
        }
        set
        {
          this.targetFrameworkDisplayName = value;
        }
      }

      public string TypeName
      {
        get
        {
          return this.typeName;
        }
        set
        {
          this.typeName = value;
        }
      }

      public string OutputPath
      {
        get
        {
          return this.outputPath;
        }
        set
        {
          this.outputPath = value;
        }
      }

      public string ImageRuntimeVersion
      {
        get
        {
          return this.imageRuntimeVersion;
        }
        set
        {
          this.imageRuntimeVersion = value;
        }
      }

      public int MetaDataVersion
      {
        get
        {
          return this.metaDataVersion;
        }
        set
        {
          this.metaDataVersion = value;
        }
      }

      public RuntimeTypeModel.Accessibility Accessibility
      {
        get
        {
          return this.accessibility;
        }
        set
        {
          this.accessibility = value;
        }
      }

      public void SetFrameworkOptions(MetaType from)
      {
        foreach (AttributeMap attributeMap in AttributeMap.Create(from.Model, from.Type.Assembly))
        {
          if (attributeMap.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
          {
            object obj;
            if (attributeMap.TryGet("FrameworkName", out obj))
              this.TargetFrameworkName = (string) obj;
            if (!attributeMap.TryGet("FrameworkDisplayName", out obj))
              break;
            this.TargetFrameworkDisplayName = (string) obj;
            break;
          }
        }
      }
    }

    public enum Accessibility
    {
      Public,
      Internal,
    }

    private sealed class StringFinder : BasicList.IPredicate
    {
      private readonly string value;

      public StringFinder(string value)
      {
        this.value = value;
      }

      bool BasicList.IPredicate.IsMatch(object obj)
      {
        return this.value == (string) obj;
      }
    }
  }
}
