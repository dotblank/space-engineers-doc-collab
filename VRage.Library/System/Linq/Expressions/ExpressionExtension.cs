// Decompiled with JetBrains decompiler
// Type: System.Linq.Expressions.ExpressionExtension
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Linq.Expressions
{
    public static class ExpressionExtension
    {
        public static Func<T, TMember> CreateGetter<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            MemberExpression memberExpression = (MemberExpression) expression.Body;
            if (memberExpression.Member is PropertyInfo)
            {
                ParameterExpression parameterExpression = null;
                return
                    Expression.Lambda<Func<T, TMember>>(
                        (Expression)
                            Expression.Property((Expression) parameterExpression, (PropertyInfo) memberExpression.Member),
                        new ParameterExpression[1]
                        {
                            parameterExpression
                        }).Compile();
            }
            else
            {
                FieldInfo fieldInfo = (FieldInfo) memberExpression.Member;
                ParameterExpression parameterExpression = Expression.Parameter(fieldInfo.DeclaringType, "obj");
                return
                    Expression.Lambda<Func<T, TMember>>(
                        (Expression) Expression.Field((Expression) parameterExpression, fieldInfo.Name),
                        new ParameterExpression[1]
                        {
                            parameterExpression
                        }).Compile();
            }
        }

        public static Action<T, TMember> CreateSetter<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            // removed because trying to fix the errors is pointless
            return default(Action<T, TMember>);
        }

        public static Func<T, TProperty> CreateGetter<T, TProperty>(this PropertyInfo propertyInfo)
        {
            Type type1 = typeof (T);
            Type type2 = typeof (TProperty);
            ParameterExpression parameterExpression = Expression.Parameter(type1, "value");
            Expression expression =
                (Expression)
                    Expression.Property(
                        propertyInfo.DeclaringType == type1
                            ? (Expression) parameterExpression
                            : (Expression)
                                Expression.Convert((Expression) parameterExpression, propertyInfo.DeclaringType),
                        propertyInfo);
            if (type2 != propertyInfo.PropertyType)
                expression = (Expression) Expression.Convert(expression, type2);
            return Expression.Lambda<Func<T, TProperty>>(expression, new ParameterExpression[1]
            {
                parameterExpression
            }).Compile();
        }

        public static Action<T, TProperty> CreateSetter<T, TProperty>(this PropertyInfo propertyInfo)
        {
            // removed because trying to fix the errors is pointless
            return default(Action<T, TProperty>);
        }

        public static TDelegate StaticCall<TDelegate>(this MethodInfo info)
        {
            // removed because trying to fix the errors is pointless
            return default(TDelegate);
        }

        public static TDelegate InstanceCall<TDelegate>(this MethodInfo info)
        {
            ParameterInfo[] parameters1 = typeof (TDelegate).GetMethod("Invoke").GetParameters();
            ParameterInfo[] parameters2 = info.GetParameters();
            ParameterExpression[] parameterExpressionArray =
                Enumerable.ToArray<ParameterExpression>(
                    Enumerable.Select<ParameterInfo, ParameterExpression>((IEnumerable<ParameterInfo>) parameters1,
                        (Func<ParameterInfo, ParameterExpression>) (s => Expression.Parameter(s.ParameterType, s.Name))));
            Expression[] expressionArray = new Expression[parameters2.Length];
            for (int index = 0; index < parameters2.Length; ++index)
                expressionArray[index] = !(parameters2[index].ParameterType == parameters1[index].ParameterType)
                    ? (Expression)
                        Expression.Convert((Expression) parameterExpressionArray[index + 1],
                            parameters2[index].ParameterType)
                    : (Expression) parameterExpressionArray[index + 1];
            return
                Expression.Lambda<TDelegate>(
                    (Expression)
                        Expression.Call(
                            parameters1[0].ParameterType == info.DeclaringType
                                ? (Expression) parameterExpressionArray[0]
                                : (Expression)
                                    Expression.Convert((Expression) parameterExpressionArray[0], info.DeclaringType),
                            info, expressionArray), parameterExpressionArray).Compile();
        }
    }
}