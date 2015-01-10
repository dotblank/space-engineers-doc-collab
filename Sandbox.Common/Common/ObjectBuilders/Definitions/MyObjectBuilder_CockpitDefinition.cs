// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CockpitDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_CockpitDefinition : MyObjectBuilder_ShipControllerDefinition
  {
    [XmlIgnore]
    private string m_characterAnimation;
    [ModdableContentFile("mwm")]
    public string GlassModel;
    [ModdableContentFile("mwm")]
    public string InteriorModel;
    [XmlIgnore]
    [ModdableContentFile("mwm")]
    public string CharacterAnimationFile;

    public string CharacterAnimation
    {
      get
      {
        return this.m_characterAnimation;
      }
      set
      {
        if (Enumerable.Contains<char>((IEnumerable<char>) value, Path.AltDirectorySeparatorChar) || Enumerable.Contains<char>((IEnumerable<char>) value, Path.DirectorySeparatorChar))
          this.CharacterAnimationFile = value;
        else
          this.m_characterAnimation = value;
      }
    }
  }
}
