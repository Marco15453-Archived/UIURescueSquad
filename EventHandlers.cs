namespace UIURescueSquad
{
#pragma warning disable SA1202

    using System;
    using System.Linq;
    using Configs;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Pickups;
    using Exiled.API.Features.Roles;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Server;
    using MEC;
    using PlayerRoles;
    using Respawning;
    using UnityEngine;
    using static API;

    using Random = UnityEngine.Random;

    /// <summary>
    /// EventHandlers and Methods which UIURescueSquad uses.
    /// </summary>
    public partial class EventHandlers
    {
        /// <summary>
        /// Is UIU spawnable in <see cref="Exiled.Events.Handlers.Server.OnRespawningTeam(RespawningTeamEventArgs)"/>.
        /// </summary>
        public static bool IsSpawnable;

        /// <summary>
        /// The maximum number of UIU players in next the respawn.
        /// </summary>
        public static uint MaxPlayers;

        private static int respawns = 0;

        /// <summary>
        /// Handles UIU spawn chance with all other conditions.
        /// </summary>
        internal static void CalculateChance()
        {
            IsSpawnable = Random.Range(1, 101) <= Config.SpawnManager.Probability &&
                respawns >= Config.SpawnManager.Respawns;

            Log.Debug($"Is UIU spawnable: {IsSpawnable}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnWaitingForPlayers"/>
        internal static void OnWaitingForPlayers()
        {
            respawns = 0;
            MaxPlayers = Config.SpawnManager.MaxSquad;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRespawningTeam(RespawningTeamEventArgs)"/>
        internal static void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == SpawnableTeamType.NineTailedFox)
            {
                respawns++;

                CalculateChance();

                if (IsSpawnable)
                {
                    bool prioritySpawn = RespawnManager.Singleton._prioritySpawn;

                    if (prioritySpawn)
                    {
                        ev.Players.OrderBy(x => (x.Role as SpectatorRole).DeathTime);
                    }

                    for (int i = ev.Players.Count; i > MaxPlayers; i--)
                    {
                        Player player = prioritySpawn ? ev.Players.Last() : ev.Players[Random.Range(0, ev.Players.Count)];
                        ev.Players.Remove(player);
                    }

                    Timing.CallDelayed(1.0f, () =>
                        {
                        foreach (Player player in ev.Players)
                        {
                            SpawnPlayer(player);
                        }

                        if (Config.SpawnManager.AnnouncementText != null)
                        {
                            Map.ClearBroadcasts();
                            Timing.CallDelayed(0.5f, () => Map.Broadcast(Config.SpawnManager.AnnouncementTime, Config.SpawnManager.AnnouncementText));
                        }

                        if (Config.SupplyDrop.DropEnabled)
                        {
                            foreach (var item in Config.SupplyDrop.DropItems)
                            {
                                Vector3 spawnPos = RoleTypeId.NtfPrivate.GetRandomSpawnLocation().Position;

                                if (Enum.TryParse(item.Key, out ItemType parsedItem))
                                {
                                    Item item1 = Item.Create(parsedItem);
                                    item1.CreatePickup(spawnPos, Random.rotation);
                                }
                                else
                                {
                                    CustomItem.TrySpawn(item.Key, spawnPos, out Pickup _);
                                }
                            }
                        }
                    });

                    Timing.CallDelayed(1f, () => IsSpawnable = false);
                }
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Map.OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs) />
        internal static void OnAnnouncingNTF(AnnouncingNtfEntranceEventArgs ev)
        {
            string cassieMessage = string.Empty;

            if (!IsSpawnable)
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(Config.SpawnManager.NtfAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = Config.SpawnManager.NtfAnnouncmentCassieNoScp;
                }
                else if (!string.IsNullOrEmpty(Config.SpawnManager.NtfAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = Config.SpawnManager.NtfAnnouncementCassie;
                }
            }
            else
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(Config.SpawnManager.UiuAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = Config.SpawnManager.UiuAnnouncmentCassieNoScp;
                }
                else if (ev.ScpsLeft > 1 && !string.IsNullOrEmpty(Config.SpawnManager.UiuAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = Config.SpawnManager.UiuAnnouncementCassie;
                }
            }

            cassieMessage = cassieMessage.Replace("{scpnum}", $"{ev.ScpsLeft} scpsubject");

            if (ev.ScpsLeft > 1)
            {
                cassieMessage = cassieMessage.Replace("scpsubject", "scpsubjects");
            }

            cassieMessage = cassieMessage.Replace("{designation}", $"nato_{ev.UnitName[0]} {ev.UnitNumber}");

            if (!string.IsNullOrEmpty(cassieMessage))
                Cassie.GlitchyMessage(cassieMessage, Config.SpawnManager.GlitchChance, Config.SpawnManager.JamChance);
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDestroying(DestroyingEventArgs)"/>
        internal static void OnDestroy(DestroyingEventArgs ev)
        {
            if (IsUiu(ev.Player))
                DestroyUIU(ev.Player);
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDied(DiedEventArgs)"/>
        internal static void OnDied(DiedEventArgs ev)
        {
            if (IsUiu(ev.Player))
                DestroyUIU(ev.Player);
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingRole(ChangingRoleEventArgs)"/>
        internal static void OnChanging(ChangingRoleEventArgs ev)
        {
            Team team = Exiled.API.Extensions.RoleExtensions.GetTeam(ev.NewRole);
            if (IsUiu(ev.Player) && team != Team.FoundationForces)
                DestroyUIU(ev.Player);
        }

        private static readonly Config Config = UIURescueSquad.Instance.Config;
    }
}
