namespace UIURescueSquad
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;
    using MEC;
    using static API;

    /// <summary>
    /// EventHandlers and Methods which UIURescueSquad uses.
    /// </summary>
    public partial class EventHandlers
    {
        internal static void SpawnPlayer(Player player, UiuType uiuType = UiuType.None)
        {
            if (uiuType == UiuType.None)
            {
                uiuType = GetUIURole(player.Role);

                if (uiuType == UiuType.None)
                {
                    Log.Error("The player's role can not be converted into uiuRole.");
                    return;
                }
            }

            if (!UiuPlayers.Contains(player))
                UiuPlayers.Add(player);

            if (!string.IsNullOrEmpty(Config.SpawnManager.UiuBroadcast))
            {
                if (Config.SpawnManager.UseHints)
                {
                    player.ShowHint(Config.SpawnManager.UiuBroadcast, Config.SpawnManager.UiuBroadcastTime);
                }
                else
                {
                    player.ClearBroadcasts();
                    player.Broadcast(Config.SpawnManager.UiuBroadcastTime, Config.SpawnManager.UiuBroadcast);
                }
            }

            Timing.CallDelayed(0.01f, () =>
            {
                player.IsGodModeEnabled = true;

                switch (uiuType)
                {
                    case UiuType.Soldier:
                        {
                            if (player.Role != RoleType.NtfCadet)
                                player.Role = RoleType.NtfCadet;

                            player.Health = Config.UiuSoldier.Health;

                            if (Config.UiuSoldier.Inventory.Count > 0)
                                GiveCustomInventory(Config.UiuSoldier.Inventory, player);

                            foreach (var ammo in Config.UiuSoldier.Ammo)
                            {
                                player.Ammo[(int)ammo.Key] = ammo.Value;
                            }

                            player.CustomInfo = $"{player.Nickname}\n{Config.UiuSoldier.Rank}";
                            break;
                        }

                    case UiuType.Agent:
                        {
                            if (player.Role != RoleType.NtfLieutenant)
                                player.Role = RoleType.NtfLieutenant;

                            player.Health = Config.UiuAgent.Health;

                            if (Config.UiuAgent.Inventory.Count > 0)
                                GiveCustomInventory(Config.UiuAgent.Inventory, player);

                            foreach (var ammo in Config.UiuAgent.Ammo)
                            {
                                player.Ammo[(int)ammo.Key] = ammo.Value;
                            }

                            player.CustomInfo = $"{player.Nickname}\n{Config.UiuAgent.Rank}";
                            break;
                        }

                    case UiuType.Leader:
                        {
                            if (player.Role != RoleType.NtfCommander)
                                player.Role = RoleType.NtfCommander;

                            player.Health = Config.UiuLeader.Health;

                            if (Config.UiuLeader.Inventory.Count > 0)
                                GiveCustomInventory(Config.UiuLeader.Inventory, player);

                            foreach (var ammo in Config.UiuLeader.Ammo)
                            {
                                player.Ammo[(int)ammo.Key] = ammo.Value;
                            }

                            player.CustomInfo = $"{player.Nickname}\n{Config.UiuLeader.Rank}";
                            break;
                        }

                    default:
                        {
                            return;
                        }
                }

                player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
                player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;

                Timing.CallDelayed(0.4f, () =>
                {
                    player.Position = Config.SpawnManager.SpawnPos.ToVector3();
                    player.IsGodModeEnabled = false;
                });
            });
        }

        // TEMP!!!
        internal static void GiveCustomInventory(List<string> inventory, Player player)
        {
            player.ClearInventory();

            foreach (string item in inventory)
            {
                if (Enum.TryParse(item, out ItemType parsedItem))
                {
                    player.AddItem(parsedItem);
                }
                else
                {
                    CustomItem.TryGive(player, item, false);
                }
            }
        }

        /// <summary>
        /// Removes a UIU role from a <see cref="Player"/>.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        internal static void DestroyUIU(Player player)
        {
            player.MaxHealth = default;

            player.CustomInfo = string.Empty;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;

            UiuPlayers.Remove(player);
        }
    }
}
