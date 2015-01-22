// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.ITerminalAction
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Ingame;
using System.Text;

namespace Sandbox.ModAPI.Interfaces
{
    /// <summary>
    /// Represents an terminal action, equivalent to those accessible via the block's control panel
    /// </summary>
    public interface ITerminalAction
    {
        /// <summary>
        /// Returns the action's identifier
        /// </summary>
        /// <remarks>All terminal blocks, which implement <see cref="IMyFunctionalBlock"/> have the actions named <c>OnOff</c>, <c>OnOff_On</c> and <c>OnOff_Off</c> by default.</remarks>
        string Id { get; }
        /// <summary>
        /// Gets the relative resource path to the action's icon
        /// </summary>
        /// <example>The following code demonstrates example usage of this property:
        /// <code source="Examples\Interfaces.ITerminalAction.Icon.cs" lang="cs"></code>
        /// This would output the following:
        /// <c>Caught exception during execution of script:Textures\GUI\Icons\ActionsToggle.dds</c></example>
        string Icon { get; }
        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <remarks>This value is shown, when configuring toolbar elements inside a ship.</remarks>
        /// <example>The following code demonstrates example usage of this property:
        /// <code source="Examples\Interfaces.ITerminalAction.Name.cs" lang="cs"></code>
        /// This would output the following:
        /// <c>Caught exception during execution of script:Toggle block On/Off</c></example>
        StringBuilder Name { get; }

        void Apply(IMyCubeBlock block);
        /// <summary>
        /// Appends the action's current value.
        /// </summary>
        /// <param name="block">The block whose current action value to get</param>
        /// <param name="appendTo">StringBuilder to which the value will be appended to.</param>
        /// <example>The following code demonstrates example usage of this property:
        /// <code source="Examples\Interfaces.ITerminalAction.WriteValue.cs" lang="cs"></code>
        /// If the block named <c>Assembler 1</c> is enabled when this program is ran, <c>sb.ToString()</c> will have the value <c>On</c></example>
        void WriteValue(IMyCubeBlock block, StringBuilder appendTo);

        bool IsEnabled(IMyCubeBlock block);
    }
}