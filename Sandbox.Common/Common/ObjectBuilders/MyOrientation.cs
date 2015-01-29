// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyOrientation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  public struct MyOrientation
  {
    [ProtoMember(1)]
    [XmlAttribute]
    public float Yaw;
    [ProtoMember(2)]
    [XmlAttribute]
    public float Pitch;
    [XmlAttribute]
    [ProtoMember(3)]
    public float Roll;

    public MyOrientation(float yaw, float pitch, float roll)
    {
      this.Yaw = yaw;
      this.Pitch = pitch;
      this.Roll = roll;
    }
  }
}
