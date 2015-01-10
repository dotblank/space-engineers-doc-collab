// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PhysicalObject
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;
using VRage.Common.Utils;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_PhysicalObject : MyObjectBuilder_Base
  {
    [DefaultValue(MyItemFlags.None)]
    [ProtoMember(1)]
    public MyItemFlags Flags;

    public MyObjectBuilder_PhysicalObject()
      : this(MyItemFlags.None)
    {
    }

    public MyObjectBuilder_PhysicalObject(MyItemFlags flags)
    {
      this.Flags = flags;
    }

    public virtual bool CanStack(MyObjectBuilder_PhysicalObject a)
    {
      return this.CanStack(a.TypeId, a.SubtypeId, a.Flags);
    }

    public virtual bool CanStack(MyObjectBuilderType typeId, MyStringId subtypeId, MyItemFlags flags)
    {
      return flags == this.Flags && typeId == this.TypeId && subtypeId == this.SubtypeId;
    }
  }
}
