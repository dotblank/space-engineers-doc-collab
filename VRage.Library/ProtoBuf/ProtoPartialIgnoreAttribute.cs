// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoPartialIgnoreAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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
            get { return this.memberName; }
        }

        public ProtoPartialIgnoreAttribute(string memberName)
        {
            if (Helpers.IsNullOrEmpty(memberName))
                throw new ArgumentNullException("memberName");
            this.memberName = memberName;
        }
    }
}