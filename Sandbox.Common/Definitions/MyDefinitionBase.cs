// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyDefinitionBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.Definitions;
using VRage.Common;
using VRage.Common.Plugins;
using VRage.Common.Utils;

namespace Sandbox.Definitions
{
  [MyDefinitionType(typeof (MyObjectBuilder_DefinitionBase))]
  public class MyDefinitionBase
  {
    public bool Enabled = true;
    public bool Public = true;
    public MyDefinitionId Id;
    public MyStringId? DisplayNameEnum;
    public MyStringId? DescriptionEnum;
    public string DisplayNameString;
    public string DescriptionString;
    public string Icon;
    public MyModContext Context;
    private static MyObjectFactory<MyDefinitionTypeAttribute, MyDefinitionBase> m_definitionFactory;

    public virtual string DisplayNameText
    {
      get
      {
        if (!this.DisplayNameEnum.HasValue)
          return this.DisplayNameString;
        else
          return MyTexts.GetString(this.DisplayNameEnum.Value);
      }
    }

    public virtual string DescriptionText
    {
      get
      {
        if (!this.DescriptionEnum.HasValue)
          return this.DescriptionString;
        else
          return MyTexts.GetString(this.DescriptionEnum.Value);
      }
    }

    public void Init(MyObjectBuilder_DefinitionBase builder, MyModContext modContext)
    {
      this.Context = modContext;
      this.Init(builder);
    }

    protected virtual void Init(MyObjectBuilder_DefinitionBase builder)
    {
      this.Id = (MyDefinitionId) builder.Id;
      this.Public = builder.Public;
      this.Enabled = builder.Enabled;
      this.Icon = builder.Icon;
      if (builder.DisplayName != null && builder.DisplayName.StartsWith("DisplayName_"))
        this.DisplayNameEnum = new MyStringId?(MyStringId.GetOrCompute(builder.DisplayName));
      else
        this.DisplayNameString = builder.DisplayName;
      if (builder.Description != null && builder.Description.StartsWith("Description_"))
        this.DescriptionEnum = new MyStringId?(MyStringId.GetOrCompute(builder.Description));
      else
        this.DescriptionString = builder.Description;
    }

    public static MyObjectFactory<MyDefinitionTypeAttribute, MyDefinitionBase> GetObjectFactory()
    {
      if (MyDefinitionBase.m_definitionFactory == null)
      {
        MyDefinitionBase.m_definitionFactory = new MyObjectFactory<MyDefinitionTypeAttribute, MyDefinitionBase>();
        MyDefinitionBase.m_definitionFactory.RegisterFromCreatedObjectAssembly();
        MyDefinitionBase.m_definitionFactory.RegisterFromAssembly(MyPlugins.GameAssembly);
        MyDefinitionBase.m_definitionFactory.RegisterFromAssembly(MyPlugins.UserAssembly);
      }
      return MyDefinitionBase.m_definitionFactory;
    }

    public virtual MyObjectBuilder_DefinitionBase GetObjectBuilder()
    {
      MyObjectBuilder_DefinitionBase objectBuilder = MyDefinitionBase.m_definitionFactory.CreateObjectBuilder<MyObjectBuilder_DefinitionBase>(this);
      objectBuilder.Id = (SerializableDefinitionId) this.Id;
      objectBuilder.Description = this.DescriptionEnum.HasValue ? this.DescriptionEnum.Value.ToString() : ((object) this.DescriptionString).ToString();
      objectBuilder.DisplayName = this.DisplayNameEnum.HasValue ? this.DisplayNameEnum.Value.ToString() : ((object) this.DisplayNameString).ToString();
      objectBuilder.Icon = this.Icon;
      objectBuilder.Public = this.Public;
      return objectBuilder;
    }

    public override string ToString()
    {
      return this.Id.ToString();
    }
  }
}
