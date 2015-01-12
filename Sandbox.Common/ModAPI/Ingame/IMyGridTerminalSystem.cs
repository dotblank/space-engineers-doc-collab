// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyGridTerminalSystem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI.Ingame
{
    /// <seealso cref="IMyCubeGrid"/>
    public interface IMyGridTerminalSystem
    {
        /// <summary>
        ///     Gets a list of all of the blocks connected to the grid terminal system.
        /// </summary>
        /// <value>List of all terminal blocks on the grid</value>
        /// <remarks>If two grids are connected with a <see cref="IMyShipConnector">connector</see> block, their terminal systems will be able to access each other if both are owned by the same player.</remarks>
        /// <seealso cref="IMyCubeGrid"/>
        List<IMyTerminalBlock> Blocks { get; }
        /// <summary>
        ///  Gets a list of all named block groups on the grid
        /// </summary>
        List<IMyBlockGroup> BlockGroups { get; }
        /// <summary>
        ///     Gets all blocks of type <typeparamref name="T"/> connected to the grid terminal system and adds them to the list <paramref name="blocks"/>.
        /// </summary>
        /// <remarks>
        ///     <paramref name="collect"/> can be used to further filter the results. LINQ and anonymous methods are currently unsupported by the in-game compiler, which is why a named method must be used.
        ///     When filtering the results, the parameter of type <see cref="IMyTerminalBlock"/> represents the block being filtered and the returned <see cref="bool"/> depicts whether the block passed the filter or not.
        /// </remarks>
        /// <example>
        ///     The following example demonstrates how to use the <paramref name="collect"/> parameter to only get disabled blocks:
        /// <code lang="cs" source="Examples\Ingame.IMyGridTerminalSystem.GetBlocksOfType.cs"/>
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="blocks"><see langword="null"/> if no blocks of specified type <typeparamref name="T"/> were found, otherwise a list containing all terminal blocks which are of the specified type</param>
        /// <param name="collect">A delegate used for filtering the returned blocks</param>
        void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);
        /// <summary>
        ///     Gets all blocks whose names contain the string provided by the <paramref name="name"/> parameter and adds them to the list <paramref name="blocks"/>.
        /// </summary>
        /// <param name="name">String to search the blocks with</param>
        /// <param name="blocks"><see langword="null"/> if no blocks were found with the search string, otherwise a list containing all matching <see cref="IMyTerminalBlock">terminal blocks</see>.</param>
        /// <param name="collect">A Func&lt;IMyTerminalBlock, bool&gt; delegate used for filtering the returned blocks</param>
        /// <remarks>
        ///     This method will return all blocks whose names contain the search string. To get a block with an exact name, see <see cref="GetBlockWithName"/>
        ///     The performed search is case insensitive.
        /// </remarks>
        /// <example>
        /// The following example demonstrates the usage of the method with a filtering method:
        /// <code lang="cs" source="Examples\Ingame.IMyGridTerminalSystem.SearchBlocksOfName.cs"/>
        /// </example>
        /// <seealso cref="GetBlockWithName"/>
        void SearchBlocksOfName(string name, List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);
        /// <summary>
        ///     Gets a terminal block whose name is an exact match with the search string <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of the block to find</param>
        /// <returns>This method performs a case sensitive search with the search string provided by the <paramref name="name"/> parameter and returns the first matching block. To get all blocks with a matching name, see <see cref="SearchBlocksOfName"/></returns>
        /// <seealso cref="SearchBlocksOfName"/>
        IMyTerminalBlock GetBlockWithName(string name);
    }
}