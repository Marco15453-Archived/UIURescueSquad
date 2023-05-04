namespace UIURescueSquad
{
    using Exiled.API.Features;
    using Exiled.CustomItems.API;
    using MEC;
    using PlayerRoles;
    using static API;

    /// <summary>
    /// EventHandlers and Methods which UIURescueSquad uses.
    /// </summary>
    public partial class EventHandlers
    {
        /// <summary>
        /// Spawns a player as UIU.
        /// </summary>
        /// <param name="player">The player to spawn.</param>
        /// <param name="uiuType">The UIU role of the spawned player.</param>
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

            player.SessionVariables.Add("IsUIU", uiuType);

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

            Timing.CallDelayed(0.05f, () =>
            {
                player.IsGodModeEnabled = true;

                switch (uiuType)
                {
                    case UiuType.Soldier:
                        {
                            if (player.Role.Type != RoleTypeId.NtfPrivate)
                                player.Role.Set(RoleTypeId.NtfPrivate);

                            player.Health = Config.UiuSoldier.Health;

                            player.ResetInventory(Config.UiuSoldier.Inventory);

                            foreach (var ammo in Config.UiuSoldier.Ammo)
                                player.SetAmmo(ammo.Key, ammo.Value);

                            player.CustomInfo = $"{player.Nickname}\n{Config.UiuSoldier.Rank}";
                            break;
                        }

                    case UiuType.Agent:
                        {
                            if (player.Role.Type != RoleTypeId.NtfSergeant)
                                player.Role.Set(RoleTypeId.NtfSergeant);

                            player.Health = Config.UiuAgent.Health;

                            player.ResetInventory(Config.UiuAgent.Inventory);

                            foreach (var ammo in Config.UiuAgent.Ammo)
                                player.SetAmmo(ammo.Key, ammo.Value);

                            player.CustomInfo = $"{player.Nickname}\n{Config.UiuAgent.Rank}";
                            break;
                        }

                    case UiuType.Leader:
                        {
                            if (player.Role.Type != RoleTypeId.NtfCaptain)
                                player.Role.Set(RoleTypeId.NtfCaptain);

                            player.Health = Config.UiuLeader.Health;

                            player.ResetInventory(Config.UiuLeader.Inventory);

                            foreach (var ammo in Config.UiuLeader.Ammo)
                                player.SetAmmo(ammo.Key, ammo.Value);

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
                    if (Config.SpawnManager.UseMTFSpawnPosition)
                        player.Position = player.Role.RandomSpawnLocation.Position;
                    else
                        player.Position = Config.SpawnManager.SpawnPos;

                    player.IsGodModeEnabled = false;
                });
            });
        }

        /// <summary>
        /// Removes a UIU role from a <see cref="Player"/>.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        internal static void DestroyUIU(Player player)
        {
            player.SessionVariables.Remove("IsUIU");

            player.MaxHealth = default;

            player.CustomInfo = string.Empty;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
        }
    }
}
