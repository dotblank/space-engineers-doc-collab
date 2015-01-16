// Decompiled with JetBrains decompiler
// Type: VRage.MemberHelper`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace VRage
{
    public static class MemberHelper<T>
    {
        public static MemberInfo GetMember<TValue>(Expression<Func<T, TValue>> selector)
        {
            Exceptions.ThrowIf<ArgumentNullException>(selector == null, "selector");
            MemberExpression memberExpression = selector.Body as MemberExpression;
            Exceptions.ThrowIf<ArgumentNullException>(memberExpression == null,
                "Selector must be a member access expression", "selector");
            return memberExpression.Member;
        }
    }
}