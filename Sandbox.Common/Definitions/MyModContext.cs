// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyModContext
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System.IO;
using System.Xml.Serialization;
using VRage.Common.Utils;

namespace Sandbox.Definitions
{
  public class MyModContext
  {
    private static MyModContext m_baseContext;
    public string CurrentFile;

    public static MyModContext BaseGame
    {
      get
      {
        if (MyModContext.m_baseContext == null)
          MyModContext.InitBaseModContext();
        return MyModContext.m_baseContext;
      }
    }

    [XmlIgnore]
    public string ModName { get; private set; }

    [XmlIgnore]
    public string ModPath { get; private set; }

    [XmlIgnore]
    public string ModPathData { get; private set; }

    public bool IsBaseGame
    {
      get
      {
        if (this.ModName == MyModContext.m_baseContext.ModName && this.ModPath == MyModContext.m_baseContext.ModPath)
          return this.ModPathData == MyModContext.m_baseContext.ModPathData;
        else
          return false;
      }
    }

    public void Init(MyObjectBuilder_Checkpoint.ModItem modItem)
    {
      this.ModName = modItem.FriendlyName;
      this.ModPath = Path.Combine(MyFileSystem.ModsPath, modItem.Name);
      this.ModPathData = Path.Combine(this.ModPath, "Data");
    }

    public void Init(MyModContext context)
    {
      this.ModName = context.ModName;
      this.ModPath = context.ModPath;
      this.ModPathData = context.ModPathData;
      this.CurrentFile = context.CurrentFile;
    }

    public void Init(string modName, string fileName)
    {
      this.ModName = modName;
      this.ModPath = (string) null;
      this.ModPathData = (string) null;
      this.CurrentFile = fileName;
    }

    private static void InitBaseModContext()
    {
      MyModContext.m_baseContext = new MyModContext();
      MyModContext.m_baseContext.ModName = (string) null;
      MyModContext.m_baseContext.ModPath = MyFileSystem.ContentPath;
      MyModContext.m_baseContext.ModPathData = Path.Combine(MyModContext.m_baseContext.ModPath, "Data");
    }
  }
}
