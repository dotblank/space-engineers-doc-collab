// Decompiled with JetBrains decompiler
// Type: System.MethodInfoExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System
{
    public static class MethodInfoExtensions
    {
        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method, object instance)
            where TDelegate : class
        {
            // removed because trying to fix the errors is pointless
            return null;
        }

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method) where TDelegate : class
        {
            return MethodInfoExtensions.CreateDelegate<TDelegate>((MethodBase) method,
                (Func<Type[], ParameterExpression[], MethodCallExpression>)
                    ((typeArguments, parameterExpressions) =>
                        Expression.Call(method,
                            MethodInfoExtensions.ProvideStrongArgumentsFor(method, parameterExpressions))));
        }

        private static TDelegate CreateDelegate<TDelegate>(MethodBase method,
            Func<Type[], ParameterExpression[], MethodCallExpression> getCallExpression)
        {
            ParameterExpression[] parameterExpressionsFrom =
                MethodInfoExtensions.ExtractParameterExpressionsFrom<TDelegate>();
            MethodInfoExtensions.CheckParameterCountsAreEqual(
                (IEnumerable<ParameterExpression>) parameterExpressionsFrom,
                (IEnumerable<ParameterInfo>) method.GetParameters());
            return
                Expression.Lambda<TDelegate>(
                    (Expression)
                        getCallExpression(MethodInfoExtensions.GetTypeArgumentsFor(method), parameterExpressionsFrom),
                    parameterExpressionsFrom).Compile();
        }

        private static ParameterExpression[] ExtractParameterExpressionsFrom<TDelegate>()
        {
            return
                Enumerable.ToArray<ParameterExpression>(
                    Enumerable.Select<ParameterInfo, ParameterExpression>(
                        (IEnumerable<ParameterInfo>) typeof (TDelegate).GetMethod("Invoke").GetParameters(),
                        (Func<ParameterInfo, ParameterExpression>) (s => Expression.Parameter(s.ParameterType))));
        }

        private static void CheckParameterCountsAreEqual(IEnumerable<ParameterExpression> delegateParameters,
            IEnumerable<ParameterInfo> methodParameters)
        {
            if (Enumerable.Count<ParameterExpression>(delegateParameters) !=
                Enumerable.Count<ParameterInfo>(methodParameters))
                throw new InvalidOperationException(
                    "The number of parameters of the requested delegate does not match the number parameters of the specified method.");
        }

        private static Type[] GetTypeArgumentsFor(MethodBase method)
        {
            return (Type[]) null;
        }

        private static Expression[] ProvideStrongArgumentsFor(MethodInfo method,
            ParameterExpression[] parameterExpressions)
        {
            return
                (Expression[])
                    Enumerable.ToArray<UnaryExpression>(
                        Enumerable.Select<ParameterInfo, UnaryExpression>(
                            (IEnumerable<ParameterInfo>) method.GetParameters(),
                            (Func<ParameterInfo, int, UnaryExpression>)
                                ((parameter, index) =>
                                    Expression.Convert((Expression) parameterExpressions[index], parameter.ParameterType))));
        }
    }
}