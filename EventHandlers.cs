using System.Text;
using System.Linq;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Respawning;
using Respawning.NamingRules;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace UIURescueSquad.Handlers
{
    public class EventHandlers
    {
        private readonly UIURescueSquad plugin;
        public EventHandlers(UIURescueSquad plugin) => this.plugin = plugin;

        public static bool isSpawnable;

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
                Map.ChangeUnitColor(0, plugin.Config.GuardUnitColor);
        }

        public void IsSpawnable()
        {
            randnums = rand.Next(1, 101);
            if (randnums <= plugin.Config.probability && respawns >= plugin.Config.respawns)
                isSpawnable = true;
            else
                isSpawnable = false;

            if (UIURescueSquad.assemblySH != null)
                SerpentsHandTeam();
        }

        public void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == SpawnableTeamType.NineTailedFox)
            {
                if (isSpawnable)
                {
                    if (plugin.Config.AnnouncementText != null)
                    {
                        if (plugin.Config.AnnouncementText != null && plugin.Config.AnnouncementText != null)
                        {
                            Map.ClearBroadcasts();
                            Map.Broadcast(plugin.Config.AnnouncementTime, plugin.Config.AnnouncementText);
                        }
                    }

                    List<Player> NTFPlayers = new List<Player>(ev.Players);

                    ev.Players.Clear();
                    for (int i = 0; i < plugin.Config.MaxSquad && NTFPlayers.Count > 0; i++)
                    {
                        Player player = NTFPlayers[rand.Next(NTFPlayers.Count)];
                        NTFPlayers.Remove(player);
                        ev.Players.Add(player);
                    }

                    foreach (Player player in ev.Players)
                    {
                        uiuPlayers.Add(player);

                        if (plugin.Config.UIUBroadcast != null && plugin.Config.UIUBroadcastTime.ToString() != null)
                        {
                            if (plugin.Config.UseHintsHere)
                            {
                                player.ShowHint(plugin.Config.UIUBroadcast, plugin.Config.UIUBroadcastTime);
                            }
                            else
                            {
                                player.ClearBroadcasts();
                                player.Broadcast(plugin.Config.UIUBroadcastTime, plugin.Config.UIUBroadcast);
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
                                        player.Health = plugin.Config.UIUSoldierLife;

                                        player.ResetInventory(plugin.Config.UIUSoldierInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = plugin.Config.UIUSoldierAmmo[i];
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UIUSoldierRank}";
                                        break;
                                    }

                                case RoleType.NtfLieutenant:
                                    {
                                        player.Health = plugin.Config.UIUAgentLife;

                                        player.ResetInventory(plugin.Config.UIUAgentInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = plugin.Config.UIUAgentAmmo[i];
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UIUAgentRank}";
                                        break;
                                    }

                                case RoleType.NtfCommander:
                                    {
                                        player.Health = plugin.Config.UIULeaderLife;

                                        player.ResetInventory(plugin.Config.UIULeaderInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = plugin.Config.UIULeaderAmmo[i];
                                        }

                                        player.CustomInfo = $"{player.Nickname}\n{plugin.Config.UIULeaderRank}";
                                        break;
                                    }
                            }
                            Timing.CallDelayed(0.4f, () => { player.Position = new Vector3(plugin.Config.spawnPosX, plugin.Config.spawnPosY, plugin.Config.spawnPosZ); });
                        });
                    }
                }
                respawns++;
            }
        }

        public void OnAnnouncingMTF(AnnouncingNtfEntranceEventArgs ev)
        {
            string cassieMessage = string.Empty;


            if (!isSpawnable) //NTF Spawn
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.ntfAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.ntfAnnouncmentCassieNoScp;
                }

                else

                if (!string.IsNullOrEmpty(plugin.Config.ntfAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.ntfAnnouncementCassie;
                }

                if (!string.IsNullOrEmpty(plugin.Config.NtfUnitColor))
                    Map.ChangeUnitColor(respawns, plugin.Config.NtfUnitColor);
            }


            if (isSpawnable && respawns >= plugin.Config.respawns + 1) //UIU spawn
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.uiuAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.uiuAnnouncmentCassieNoScp;
                }

                else

                if (!string.IsNullOrEmpty(plugin.Config.uiuAnnouncementCassie))
                {
                    ev.IsAllowed = false;

                    cassieMessage = plugin.Config.uiuAnnouncementCassie;
                }

                if (!string.IsNullOrEmpty(plugin.Config.UiuUnitColor))
                    Map.ChangeUnitColor(respawns, plugin.Config.UiuUnitColor);
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

        public void OnDying(DiedEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Target))
            {
                KillUIU(ev.Target);
            }
        }
        public void OnChanging(ChangingRoleEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Player) && ev.Player.Role != RoleType.Spectator)
            {
                KillUIU(ev.Player);
            }
        }

        public void KillUIU(Player player)
        {
            player.CustomInfo = string.Empty;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;

            uiuPlayers.Remove(player);
        }

        public void SerpentsHandTeam()
        {
            if (!isSpawnable)
            {
                Log.Debug("UIU is not spawnable right now. Returning...", plugin.Config.Debug);
                Timing.CallDelayed(1f, () =>
                {
                    isSpawnable = false;
                    return;
                });
            }

            if (!SerpentsHand.EventHandlers.isSpawnable)
            {
                Log.Debug("Serpents Hand is not spawnable right now. Returning...", plugin.Config.Debug);
                Timing.CallDelayed(1f, () =>
                {
                    SerpentsHand.EventHandlers.isSpawnable = false;
                    return;
                });
            }


            if (rand.Next(0, 2) == 0)
            {
                SerpentsHand.EventHandlers.isSpawnable = true;
                isSpawnable = false;
            }

            else
            {
                SerpentsHand.EventHandlers.isSpawnable = false;
                isSpawnable = true;
            }
            Log.Debug($"Serpents Hand is spawnable: {SerpentsHand.EventHandlers.isSpawnable}", plugin.Config.Debug);
            Log.Debug($"UIU is spawnable: {isSpawnable}", plugin.Config.Debug);
        }

        //There was a piece of code, but now it is EXILED feature
    }
}
