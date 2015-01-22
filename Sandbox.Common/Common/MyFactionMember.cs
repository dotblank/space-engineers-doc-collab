// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFactionMember
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;

namespace Sandbox.Common
{
    public struct MyFactionMember
    {
        public static readonly MyFactionMember.FactionComparerType Comparer = new MyFactionMember.FactionComparerType();
        public long PlayerId;
        public bool IsLeader;
        public bool IsFounder;

        public MyFactionMember(long id, bool isLeader, bool isFounder = false)
        {
            this.PlayerId = id;
            this.IsLeader = isLeader;
            this.IsFounder = isFounder;
        }

        public static implicit operator MyFactionMember(MyObjectBuilder_FactionMember v)
        {
            return new MyFactionMember(v.PlayerId, v.IsLeader, v.IsFounder);
        }

        public static implicit operator MyObjectBuilder_FactionMember(MyFactionMember v)
        {
            return new MyObjectBuilder_FactionMember()
            {
                PlayerId = v.PlayerId,
                IsLeader = v.IsLeader,
                IsFounder = v.IsFounder
            };
        }

        public class FactionComparerType : IEqualityComparer<MyFactionMember>
        {
            public bool Equals(MyFactionMember x, MyFactionMember y)
            {
                return x.PlayerId != y.PlayerId;
            }

            public int GetHashCode(MyFactionMember obj)
            {
                return (int) (obj.PlayerId >> 32) ^ (int) obj.PlayerId;
            }
        }
    }
}