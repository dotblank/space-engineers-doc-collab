// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyInventoryOwner
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Ingame;

namespace Sandbox.ModAPI.Interfaces
{
    public interface IMyInventoryOwner
    {
        /// <summary>
        ///     Gets the amount of inventories owned
        /// </summary>
        int InventoryCount { get; }

        long EntityId { get; }
        /// <summary>
        ///     Gets or sets a boolean value depicting whether the inventory owner uses the conveyor system.
        /// </summary>
        /// <remarks>
        ///     <note type="important">This property's <c>setter</c> is not implemented and using it will throw an exception.</note>
        /// </remarks>
        /// <exception cref="System.NotImplementedException">This exception is thrown if an attempt is made to use the setter.</exception>
        bool UseConveyorSystem { get; set; }
        /// <summary>
        ///     Returns the specified inventory
        /// </summary>
        /// <param name="index">Zero-based index of the inventory. See remarks.</param>
        /// <remarks>Some inventory blocks may have more than one inventory. For example, an <see cref="Sandbox.ModAPI.Ingame.IMyAssembler">assembler</see> has an input and output inventory, whereas a <see cref="Sandbox.ModAPI.Ingame.IMyCargoContainer">cargo container</see> only has one.</remarks>
        /// <example>
        ///     The following code example demonstrates getting the output inventory of an assembler and moving all of its contents to a cargo container.
        ///     <code source="Examples\Interfaces.IMyInventoryOwner.GetInventory.cs" lang="cs"></code>
        /// </example>
        /// <seealso cref="Sandbox.ModAPI.Ingame.IMyCargoContainer"/>
        /// <seealso cref="Sandbox.ModAPI.Ingame.IMyAssembler"/>
        IMyInventory GetInventory(int index);
    }
}