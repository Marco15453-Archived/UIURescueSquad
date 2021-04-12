using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Respawning;
using UnityEngine;

namespace UIURescueSquad
{
    public partial class EventHandlers
    {
        private readonly UIURescueSquad plugin;
        public EventHandlers(UIURescueSquad plugin) => this.plugin = plugin;

        public static bool IsSpawnable;

        public static List<Player> uiuPlayers = new List<Player>();

        private int respawns = 0;
        private int randnums;

        private static System.Random rand = new System.Random();

        public void OnWaitingForPlayers()
        {
            uiuPlayers.Clear();
            respawns = 0;
        }

        public void OnRoundStart()
        {
            if (!string.IsNullOrEmpty(plugin.Config.GuardUnitColor))
            {
                try
                {
                    Map.ChangeUnitColor(0, plugin.Config.GuardUnitColor);
                }
                catch (Exception) {}
            }
        }

        public void CalculateChance()
        {
            randnums = rand.Next(1, 101);
            if (randnums <= plugin.Config.Probability &&
                respawns >= plugin.Config.Respawns)
            {
                IsSpawnable = true;
            }
            else
            {
                IsSpawnable = false;
            }
        }

        public void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == SpawnableTeamType.NineTailedFox)
            {
                respawns++;

                if (IsSpawnable)
                {
                    if (plugin.Config.AnnouncementText != null)
                    {
                        Map.ClearBroadcasts();
                        Map.Broadcast(plugin.Config.AnnouncementTime, plugin.Config.AnnouncementText);
                    }

                    if (plugin.Config.DropEnabled)
                    {
                        foreach (string item in plugin.Config.dropItems)
                        {
                            Vector3 spawnPos = Map.GetRandomSpawnPoint(RoleType.NtfCadet);

                            try
                            {
                                ItemType parsedItem = (ItemType)Enum.Parse(typeof(ItemType), item, true);

                                Exiled.API.Extensions.Item.Spawn(parsedItem, Exiled.API.Extensions.Item.GetDefaultDurability(parsedItem), spawnPos);
                            }
                            catch (Exception)
                            {
                                if (!UIURescueSquad.IsCustomItems)
                                    Log.Error($"\"{item}\" is not a valid item name.");
                                else
                                    CustomItemHandler(spawnPos, item);
                            }
                        }
                    }

                    List<Player> ntfPlayers = new List<Player>(ev.Players);

                    ev.Players.Clear();

                    for (int i = 0; i < plugin.Config.MaxSquad && ntfPlayers.Count > 0; i++)
                    {
                        Player player = ntfPlayers[rand.Next(ntfPlayers.Count)];
                        ntfPlayers.Remove(player);
                        ev.Players.Add(player);
                    }

                    foreach (Player player in ev.Players)
                    {
                        uiuPlayers.Add(player);

                        if (plugin.Config.UiuBroadcast != null && plugin.Config.UiuBroadcastTime != null)
                        {
                            if (plugin.Config.UseHintsHere)
                            {
                                player.ShowHint(plugin.Config.UiuBroadcast, plugin.Config.UiuBroadcastTime);
                            }
                            else
                            {
                                player.ClearBroadcasts();
                                player.Broadcast(plugin.Config.UiuBroadcastTime, plugin.Config.UiuBroadcast);
                            }
                        }

                        Timing.CallDelayed(0.01f, () =>
                        {
                            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
                            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;

                            switch (player.Role)
                            {
                                case RoleType.NtfCadet:
                                    {
                                        player.MaxHealth = plugin.Config.UiuSoldierLife;
                                        player.Health = plugin.Config.UiuSoldierLife;

                                        if (plugin.Config.UiuSoldierInventory.Count > 0)
                                            GiveCustomInventory(plugin.Config.UiuSoldierInventory, player);

                                        foreach (var ammo in plugin.Config.UiuSoldierAmmo)
                                        {
                                            player.Ammo[(int)ammo.Key] = ammo.Value;
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UiuSoldierRank}";
                                        break;
                                    }

                                case RoleType.NtfLieutenant:
                                    {
                                        player.MaxHealth = plugin.Config.UiuAgentLife;
                                        player.Health = plugin.Config.UiuAgentLife;

                                        if (plugin.Config.UiuAgentInventory.Count > 0)
                                            GiveCustomInventory(plugin.Config.UiuAgentInventory, player);

                                        foreach (var ammo in plugin.Config.UIUAgentAmmo)
                                        {
                                            player.Ammo[(int)ammo.Key] = ammo.Value;
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UiuAgentRank}";
                                        break;
                                    }

                                case RoleType.NtfCommander:
                                    {
                                        player.MaxHealth = plugin.Config.UiuLeaderLife;
                                        player.Health = plugin.Config.UiuLeaderLife;

                                        if (plugin.Config.UiuLeaderInventory.Count > 1)
                                            GiveCustomInventory(plugin.Config.UiuLeaderInventory, player);

                                        foreach (var ammo in plugin.Config.UiuLeaderAmmo)
                                        {
                                            player.Ammo[(int)ammo.Key] = ammo.Value;
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UiuLeaderRank}";
                                        break;
                                    }
                            }
                            Timing.CallDelayed(0.4f, () => { player.Position = new Vector3(plugin.Config.spawnPosX, plugin.Config.spawnPosY, plugin.Config.spawnPosZ); });
                        });
                    }
                }
            }
        }

        public void OnAnnouncingMTF(AnnouncingNtfEntranceEventArgs ev)
        {
            string cassieMessage = string.Empty;

            if (!IsSpawnable)
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.NtfAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.NtfAnnouncmentCassieNoScp;
                }

                else

                if (!string.IsNullOrEmpty(plugin.Config.NtfAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.NtfAnnouncementCassie;
                }
                
                if (!string.IsNullOrEmpty(plugin.Config.NtfUnitColor))
                {
                    try
                    {
                        Map.ChangeUnitColor(respawns, plugin.Config.NtfUnitColor);
                    }
                    catch (Exception) { }
                }

            }

            if (IsSpawnable && respawns >= plugin.Config.Respawns + 1)
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.UiuAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.UiuAnnouncmentCassieNoScp;
                }

                else

                if (!string.IsNullOrEmpty(plugin.Config.UiuAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.UiuAnnouncementCassie;
                }


                // HIGHLY EXPERIMENTAL FIX. NEEDS TESTING.
                if (!string.IsNullOrEmpty(plugin.Config.UiuUnitColor))
                {
                TryAgain:

                    try
                    {
                        Map.ChangeUnitColor(respawns, plugin.Config.UiuUnitColor);
                    }
                    catch (Exception)
                    {
                        goto TryAgain;
                    }
                }

            }

            cassieMessage = cassieMessage.Replace("{scpnum}", $"{ev.ScpsLeft} scpsubject");
            if (ev.ScpsLeft > 1) cassieMessage = cassieMessage.Replace("scpsubject", "scpsubjects");

            cassieMessage = cassieMessage.Replace("{designation}", $"nato_{ev.UnitName[0]} {ev.UnitNumber}");

            if (!string.IsNullOrEmpty(cassieMessage))
                Cassie.GlitchyMessage(cassieMessage, 0.05f, 0.05f);
        }

        public void OnDestroy(DestroyingEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Player))
            {
                KillUIU(ev.Player);
            }
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Target))
            {
                KillUIU(ev.Target);
            }
        }

        public void OnChanging(ChangingRoleEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Player) && ev.Player.Role != RoleType.Spectator && ev.Player.Role != RoleType.None)
            {
                KillUIU(ev.Player);
            }
        }
    }
}
