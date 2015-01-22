// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoPartialIgnoreAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class ProtoPartialIgnoreAttribute : ProtoIgnoreAttribute
  {
    private readonly string memberName;

    public string MemberName
    {
      get
      {
        return this.memberName;
      }
    }

    public ProtoPartialIgnoreAttribute(string memberName)
    {
      if (Helpers.IsNullOrEmpty(memberName))
        throw new ArgumentNullException("memberName");
      this.memberName = memberName;
    }
  }
}
