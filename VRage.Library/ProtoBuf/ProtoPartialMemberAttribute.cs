// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoPartialMemberAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class ProtoPartialMemberAttribute : ProtoMemberAttribute
  {
    private readonly string memberName;

    public string MemberName
    {
      get
      {
        return this.memberName;
      }
    }

    public ProtoPartialMemberAttribute(int tag, string memberName)
      : base(tag)
    {
      if (Helpers.IsNullOrEmpty(memberName))
        throw new ArgumentNullException("memberName");
      this.memberName = memberName;
    }
  }
}
