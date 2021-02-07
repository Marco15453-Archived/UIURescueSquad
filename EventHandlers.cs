using System.Text;
using System.Linq;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Respawning;
using Respawning.NamingRules;
using UnityEngine;

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
            if(!string.IsNullOrEmpty(plugin.Config.GuardUnitColor))
                ChangeUnitColor(0, plugin.Config.GuardUnitColor);
        }

        public void IsSpawnable()
        {
            randnums = rand.Next(1, 101);
            if (randnums <= plugin.Config.probability && respawns >= plugin.Config.respawns) isSpawnable = true;
            else isSpawnable = false;
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

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = plugin.Config.UIUSoldierRank;
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

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = plugin.Config.UIUAgentRank;
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

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = plugin.Config.UIULeaderRank;
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
            if(!isSpawnable)
            {
                if(!string.IsNullOrEmpty(plugin.Config.NtfUnitColor))
                    ChangeUnitColor(respawns, plugin.Config.NtfUnitColor);
            }

            if (isSpawnable && respawns >= plugin.Config.respawns + 1)
            {
                if(!string.IsNullOrEmpty(plugin.Config.UiuUnitColor))
                    ChangeUnitColor(respawns, plugin.Config.UiuUnitColor);

                ev.IsAllowed = false;

                if (plugin.Config.DisableNTFAnnounce)
                {
                    if (ev.ScpsLeft == 0)
                    {
                        Cassie.GlitchyMessage(plugin.Config.AnnouncmentCassieNoScp, 0.05f, 0.05f);
                    }
                    else
                    {
                        StringBuilder message = new StringBuilder();

                        message.Append(plugin.Config.AnnouncementCassie);
                        message.Replace("{scpnum}", $"{ev.ScpsLeft} scpsubject");
                        if (ev.ScpsLeft > 1) message.Replace("scpsubject", "scpsubjects");

                        Cassie.GlitchyMessage(message.ToString(), 0.05f, 0.05f);
                    }
                }
            }
        }

        public void OnLeft(DestroyingEventArgs ev)
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
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;

            uiuPlayers.Remove(player);
        }

        //Thank you sanyae2439 for helping me (me as Michal78900) in making this code below!
        public void ChangeUnitColor(int index, string color)
        {
            var Unit = RespawnManager.Singleton.NamingManager.AllUnitNames[index].UnitName;

            RespawnManager.Singleton.NamingManager.AllUnitNames.Remove(RespawnManager.Singleton.NamingManager.AllUnitNames[index]);
            UnitNamingRules.AllNamingRules[SpawnableTeamType.NineTailedFox].AddCombination($"<color={color}>{Unit}</color>", SpawnableTeamType.NineTailedFox);

            foreach (var ply in Player.List.Where(x => x.ReferenceHub.characterClassManager.CurUnitName == Unit))
            {
                ply.ReferenceHub.characterClassManager.NetworkCurUnitName = $"<color={color}>{Unit}</color>";
            }
        }
    }
}
