using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIURescueSquad.Events
{
    internal sealed class ServerHandler
    {
        private static UIURescueSquad plugin = UIURescueSquad.Singleton;
        private static Config config = UIURescueSquad.Singleton.Config;

        public void OnWaitingForPlayers()
        {
            plugin.UIURespawnCount = 0;
            plugin.TeamRespawnCount = 0;
            plugin.MTFRespawnCount = 0;
        }

        public void OnRoundStarted()
        {
            if (!string.IsNullOrEmpty(config.TeamColors.GuardUnitColor))
                Map.ChangeUnitColor(0, config.TeamColors.GuardUnitColor);
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            plugin.TeamRespawnCount++;

            if (ev.NextKnownTeam != SpawnableTeamType.NineTailedFox)
                return;

            plugin.MTFRespawnCount++;

            if (!plugin.IsSpawnable)
            {
                if (!string.IsNullOrEmpty(config.TeamColors.NtfUnitColor))
                    Timing.CallDelayed(Timing.WaitUntilTrue(() => RespawnManager.Singleton.NamingManager.AllUnitNames.Count >= plugin.MTFRespawnCount), () => Map.ChangeUnitColor(plugin.MTFRespawnCount, config.TeamColors.NtfUnitColor));
                return;
            }

            bool prioritySpawn = RespawnManager.Singleton._prioritySpawn;

            if (prioritySpawn)
                ev.Players.OrderBy(x => x.ReferenceHub.characterClassManager.DeathTime);

            List<Player> UIUPlayers = new List<Player>();
            for (int i = 0; i < config.SpawnManager.MaxSquad && ev.Players.Count > 0; i++)
            {
                Player player = prioritySpawn ? ev.Players.First() : ev.Players[UnityEngine.Random.Range(0, ev.Players.Count)];
                UIUPlayers.Add(player);
                ev.Players.Remove(player);
            }

            if (config.SupplyDrop.DropEnabled)
            {
                foreach (var item in config.SupplyDrop.DropItems)
                {
                    Vector3 spawnPos = RoleType.NtfPrivate.GetRandomSpawnProperties().Item1;
                    if (Enum.TryParse(item.Key, out ItemType parsed))
                        new Item(parsed).Spawn(spawnPos, UnityEngine.Random.rotation);
                    else
                        CustomItem.TrySpawn(item.Key, spawnPos, out Pickup _);
                }
            }

            Timing.CallDelayed(0.1f, () => API.SpawnSquad(UIUPlayers));

            if (!string.IsNullOrEmpty(config.TeamColors.UiuUnitColor))
                Timing.CallDelayed(Timing.WaitUntilTrue(() => RespawnManager.Singleton.NamingManager.AllUnitNames.Count >= plugin.MTFRespawnCount), () => Map.ChangeUnitColor(plugin.MTFRespawnCount, config.TeamColors.UiuUnitColor));

            if (config.SpawnManager.MaxSpawns > 0)
                plugin.UIURespawnCount++;

            plugin.IsSpawnable = false;
            ev.NextKnownTeam = SpawnableTeamType.None;
        }
    }
}
