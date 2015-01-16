// Decompiled with JetBrains decompiler
// Type: VRage.Reflection.FullyQualifiedNameComparer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;

namespace VRage.Reflection
{
    public class FullyQualifiedNameComparer : IComparer<Type>
    {
        public static readonly FullyQualifiedNameComparer Default = new FullyQualifiedNameComparer();

        public int Compare(Type x, Type y)
        {
            return x.FullName.CompareTo(y.FullName);
        }
    }
}