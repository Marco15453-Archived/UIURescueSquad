using Exiled.API.Features;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIURescueSquad
{
    public partial class EventHandlers
    {
        internal void GiveCustomInventory(List<string> Inventory, Player player)
        {
            player.ClearInventory();

            foreach (var item in Inventory)
            {
                try
                {
                    ItemType parsedItem = (ItemType)Enum.Parse(typeof(ItemType), item, true);
                    player.AddItem(parsedItem);
                }
                catch (Exception)
                {
                    if (!UIURescueSquad.IsCustomItems)
                    {
                        Log.Error($"\"{item}\" is not a valid item name.");
                        continue;
                    }
                    else
                    {
                        CustomItemHandler(player, item);
                    }
                }
            }
        }

        internal void CustomItemHandler(Vector3 spawnPos, string item)
        {
            if (!Exiled.CustomItems.API.Features.CustomItem.TrySpawn(item, spawnPos))
            {
                Log.Error($"\"{item}\" is not a valid item / customitem name.");
            }
        }

        internal void CustomItemHandler(Player player, string item)
        {
            if (!Exiled.CustomItems.API.Features.CustomItem.TryGive(player, item, false))
            {
                Log.Error($"\"{item}\" is not a valid item / customitem name.");
            }
        }

        internal void KillUIU(Player player)
        {
            player.MaxHealth = default;

            player.CustomInfo = string.Empty;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;

            uiuPlayers.Remove(player);
        }
    }
}
