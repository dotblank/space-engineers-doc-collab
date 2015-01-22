// Decompiled with JetBrains decompiler
// Type: VRage.MemberHelper`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
      Exceptions.ThrowIf<ArgumentNullException>(memberExpression == null, "Selector must be a member access expression", "selector");
      return memberExpression.Member;
    }
  }
}
