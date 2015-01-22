// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.TerminalBlockExtentions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Interfaces;

namespace Sandbox.ModAPI.Ingame
{
    public static class TerminalBlockExtentions
    {
        public static void ApplyAction(this IMyTerminalBlock block, string actionName)
        {
            block.GetActionWithName(actionName).Apply((Sandbox.ModAPI.IMyCubeBlock) block);
        }

        public static bool HasAction(this IMyTerminalBlock block, string Action)
        {
            return block.GetActionWithName(Action) != null;
        }

        public static bool HasInventory(this IMyTerminalBlock block)
        {
            return block is IMyInventoryOwner;
        }

        public static IMyInventory GetInventory(this IMyTerminalBlock block, int index)
        {
            throw new System.NotImplementedException();
        }

        public static int GetInventoryCount(this IMyTerminalBlock block)
        {
            if (TerminalBlockExtentions.HasInventory(block))
                return ((IMyInventoryOwner) block).InventoryCount;
            else
                return 0;
        }

        public static bool GetUseConveyorSystem(this IMyTerminalBlock block)
        {
            if (TerminalBlockExtentions.HasInventory(block))
                return ((IMyInventoryOwner) block).UseConveyorSystem;
            else
                return false;
        }

        public static void SetUseConveyorSystem(this IMyTerminalBlock block, bool use)
        {
            if (!TerminalBlockExtentions.HasInventory(block))
                return;
            ((IMyInventoryOwner) block).UseConveyorSystem = use;
        }
    }
}