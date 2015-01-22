// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyDefinitionId
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;
using VRage.Common.Utils;

namespace Sandbox.Definitions
{
    public struct MyDefinitionId : IEquatable<MyDefinitionId>
    {
        public static readonly MyDefinitionId.DefinitionIdComparerType Comparer =
            new MyDefinitionId.DefinitionIdComparerType();

        public readonly MyObjectBuilderType TypeId;
        public readonly MyStringId SubtypeId;

        public string SubtypeName
        {
            get { return this.SubtypeId.ToString(); }
        }

        public MyDefinitionId(MyObjectBuilderType type)
        {
            this = new MyDefinitionId(type, MyStringId.GetOrCompute((string) null));
        }

        public MyDefinitionId(MyObjectBuilderType type, string subtypeName)
        {
            this = new MyDefinitionId(type, MyStringId.GetOrCompute(subtypeName));
        }

        public MyDefinitionId(MyObjectBuilderType type, MyStringId subtypeId)
        {
            this.TypeId = type;
            this.SubtypeId = subtypeId;
        }

        public static implicit operator MyDefinitionId(SerializableDefinitionId v)
        {
            return new MyDefinitionId(v.TypeId, v.SubtypeName);
        }

        public static implicit operator SerializableDefinitionId(MyDefinitionId v)
        {
            return new SerializableDefinitionId(v.TypeId, v.SubtypeName);
        }

        public static bool operator ==(MyDefinitionId l, MyDefinitionId r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(MyDefinitionId l, MyDefinitionId r)
        {
            return !l.Equals(r);
        }

        public override int GetHashCode()
        {
            return this.TypeId.GetHashCode() << 16 ^ this.SubtypeId.GetHashCode();
        }

        public long GetHashCodeLong()
        {
            return (long) this.TypeId.GetHashCode() << 32 ^ (long) this.SubtypeId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MyDefinitionId)
                return this.Equals((MyDefinitionId) obj);
            else
                return false;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", !this.TypeId.IsNull ? (object) this.TypeId.ToString() : (object) "(null)",
                !string.IsNullOrEmpty(this.SubtypeName) ? (object) this.SubtypeName : (object) "(null)");
        }

        public bool Equals(MyDefinitionId other)
        {
            if (this.TypeId == other.TypeId)
                return this.SubtypeId == other.SubtypeId;
            else
                return false;
        }

        public class DefinitionIdComparerType : IEqualityComparer<MyDefinitionId>
        {
            public bool Equals(MyDefinitionId x, MyDefinitionId y)
            {
                if (x.TypeId == y.TypeId)
                    return x.SubtypeId == y.SubtypeId;
                else
                    return false;
            }

            public int GetHashCode(MyDefinitionId obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}