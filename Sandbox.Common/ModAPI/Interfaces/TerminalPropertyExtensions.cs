// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.TerminalPropertyExtensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Ingame;
using System;
using VRageMath;

namespace Sandbox.ModAPI.Interfaces
{
    public static class TerminalPropertyExtensions
    {
        public static ITerminalProperty<TValue> As<TValue>(this ITerminalProperty property)
        {
            return property as ITerminalProperty<TValue>;
        }

        public static ITerminalProperty<TValue> Cast<TValue>(this ITerminalProperty property)
        {
            ITerminalProperty<TValue> terminalProperty = TerminalPropertyExtensions.As<TValue>(property);
            if (terminalProperty == null)
                throw new InvalidOperationException(string.Format(
                    "Property {0} is not of type {1}, correct type is {2}", (object) property.Id,
                    (object) typeof (TValue).Name, (object) property.TypeName));
            else
                return terminalProperty;
        }

        public static bool Is<TValue>(this ITerminalProperty property)
        {
            return property is ITerminalProperty<TValue>;
        }

        public static ITerminalProperty<float> AsFloat(this ITerminalProperty property)
        {
            return TerminalPropertyExtensions.As<float>(property);
        }

        public static ITerminalProperty<Color> AsColor(this ITerminalProperty property)
        {
            return TerminalPropertyExtensions.As<Color>(property);
        }

        public static float GetValueFloat(this IMyTerminalBlock block, string propertyId)
        {
            return TerminalPropertyExtensions.GetValue<float>(block, propertyId);
        }

        public static void SetValueFloat(this IMyTerminalBlock block, string propertyId, float value)
        {
            TerminalPropertyExtensions.SetValue<float>(block, propertyId, value);
        }

        public static void SetValue<T>(this IMyTerminalBlock block, string propertyId, T value)
        {
            TerminalPropertyExtensions.Cast<T>(block.GetProperty(propertyId)).SetValue((IMyCubeBlock) block, value);
        }

        public static T GetValue<T>(this IMyTerminalBlock block, string propertyId)
        {
            return TerminalPropertyExtensions.Cast<T>(block.GetProperty(propertyId)).GetValue((IMyCubeBlock) block);
        }

        public static T GetDefaultValue<T>(this IMyTerminalBlock block, string propertyId)
        {
            return
                TerminalPropertyExtensions.Cast<T>(block.GetProperty(propertyId)).GetDefaultValue((IMyCubeBlock) block);
        }

        public static T GetMininum<T>(this IMyTerminalBlock block, string propertyId)
        {
            return TerminalPropertyExtensions.Cast<T>(block.GetProperty(propertyId)).GetMininum((IMyCubeBlock) block);
        }

        public static T GetMaximum<T>(this IMyTerminalBlock block, string propertyId)
        {
            return TerminalPropertyExtensions.Cast<T>(block.GetProperty(propertyId)).GetMaximum((IMyCubeBlock) block);
        }
    }
}