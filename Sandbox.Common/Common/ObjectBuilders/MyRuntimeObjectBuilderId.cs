// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyRuntimeObjectBuilderId
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public struct MyRuntimeObjectBuilderId
    {
        public static readonly MyRuntimeObjectBuilderId.IdComparerType Comparer =
            new MyRuntimeObjectBuilderId.IdComparerType();

        [ProtoMember(1)] internal readonly ushort Value;

        public MyRuntimeObjectBuilderId(ushort value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", (object) this.Value, (object) (MyObjectBuilderType) this);
        }

        public class IdComparerType : IComparer<MyRuntimeObjectBuilderId>, IEqualityComparer<MyRuntimeObjectBuilderId>
        {
            public int Compare(MyRuntimeObjectBuilderId x, MyRuntimeObjectBuilderId y)
            {
                return MyRuntimeObjectBuilderId.IdComparerType.CompareInternal(ref x, ref y);
            }

            public bool Equals(MyRuntimeObjectBuilderId x, MyRuntimeObjectBuilderId y)
            {
                return MyRuntimeObjectBuilderId.IdComparerType.CompareInternal(ref x, ref y) == 0;
            }

            public int GetHashCode(MyRuntimeObjectBuilderId obj)
            {
                return obj.Value.GetHashCode();
            }

            private static int CompareInternal(ref MyRuntimeObjectBuilderId x, ref MyRuntimeObjectBuilderId y)
            {
                return (int) x.Value - (int) y.Value;
            }
        }
    }
}