using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using MEC;
using Respawning;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIURescueSquad
{
    internal static class Extensions
    {
        private static UIURescueSquad plugin = UIURescueSquad.Singleton;
        private static Config config = UIURescueSquad.Singleton.Config;

        public static void CalculateChance()
        {
            plugin.IsSpawnable = Random.Range(0, 101) <= config.SpawnManager.SpawnChance &&
                plugin.TeamRespawnCount >= config.SpawnManager.RespawnDelay &&
                plugin.UIURespawnCount < config.SpawnManager.MaxSpawns;
            Log.Debug($"Is UIU spawnable: {plugin.IsSpawnable}", config.Debug);
        }

        public static void SpawnPlayer(Player player, UIUType uiuType = UIUType.None)
        {
            if (uiuType == UIUType.None)
                uiuType = API.GetUIURole(player.Role);

            if (uiuType == UIUType.None)
            {
                Log.Error($"The player's role can not be converted into uiuRole.");
                return;
            }

            player.SessionVariables.Add("IsUIU", uiuType);
            player.Broadcast(config.SpawnManager.SpawnBroadcast);

            switch (uiuType)
            {
                case UIUType.Soldier:
                    player.Role = RoleType.NtfPrivate;
                    player.Health = config.UiuSoldier.Health;
                    player.MaxHealth = (int)config.UiuSoldier.Health;
                    player.CustomInfo = $"{player.Nickname}\n{config.UiuSoldier.Rank}";
                    player.ResetInventory(config.UiuSoldier.Inventory);
                    foreach (var ammo in config.UiuSoldier.Ammo)
                        player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                    break;
                case UIUType.Agent:
                    player.Role = RoleType.NtfSergeant;
                    player.Health = config.UiuAgent.Health;
                    player.MaxHealth = (int)config.UiuAgent.Health;
                    player.CustomInfo = $"{player.Nickname}\n{config.UiuAgent.Rank}";
                    player.ResetInventory(config.UiuAgent.Inventory);
                    foreach (var ammo in config.UiuAgent.Ammo)
                        player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                    break;
                case UIUType.Leader:
                    player.Role = RoleType.NtfCaptain;
                    player.Health = config.UiuLeader.Health;
                    player.MaxHealth = (int)config.UiuLeader.Health;
                    player.CustomInfo = $"{player.Nickname}\n{config.UiuLeader.Rank}";
                    player.ResetInventory(config.UiuLeader.Inventory);
                    foreach (var ammo in config.UiuLeader.Ammo)
                        player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                    break;
                default:
                    return;
            }
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            Timing.CallDelayed(0.5f, () => player.Position = config.SpawnManager.SpawnPos);
        }

        public static void SpawnSquad(List<Player> players)
        {
            foreach (Player player in players)
                SpawnPlayer(player);
        }

        public static void SpawnSquad()
        {
            plugin.IsSpawnable = true;
            Respawn.ForceWave(SpawnableTeamType.NineTailedFox);
        }

        public static void DestroyUIU(Player player)
        {
            player.SessionVariables.Remove("IsUIU");
            player.Health = default;
            player.MaxHealth = default;
            player.CustomInfo = string.Empty;

            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
        }
    }
}
