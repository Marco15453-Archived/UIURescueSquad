using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace UIURescueSquad.Handlers
{
    public partial class EventHandlers
    {
        public UIURescueSquad plugin;
        public EventHandlers(UIURescueSquad plugin) => this.plugin = plugin;

        public static List<int> uiuPlayers = new List<int>();

        private int respawns = 0;
        private int randnums;

        private static System.Random rand = new System.Random();

        private static Vector3 SpawnPos = new Vector3(170, 985, 29);
        //NOTE: Make spawnpos configurable
        private string rank;

        public void OnWaitingForPlayers()
        {
            uiuPlayers.Clear();
            respawns = 0;
        }

        public void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
            {
                randnums = rand.Next(1, 101);
                Log.Info(randnums);
                if (randnums <= plugin.Config.probability & respawns >= plugin.Config.respawns)
                {
                    if (plugin.Config.AnnouncementText != null)
                    {
                        if (plugin.Config.AnnouncementText != null && plugin.Config.AnnouncementText != null)
                        {
                            Map.ClearBroadcasts();
                            Map.Broadcast(plugin.Config.AnnouncementTime, plugin.Config.AnnouncementText);
                        }
                    }
                    //Cassie.Message("Attention, the U I U HasEntered please help the MtfUnit that are AwaitingRecontainment .g7 ScpSubjects", true, true);
                    //NOTE: disable MTF entrance message to allow cassie messages
                    foreach (Player player in ev.Players)
                    {
                        uiuPlayers.Add(player.Id);
                        if (plugin.Config.UIUBroadcast != null && plugin.Config.UIUBroadcastTime.ToString() != null)
                        {
                            if (plugin.Config.UseHintsHere)
                            {
                                player.ClearBroadcasts();
                                player.ShowHint(plugin.Config.UIUBroadcast, plugin.Config.UIUBroadcastTime);
                            }
                            else
                            {
                                player.ClearBroadcasts();
                                player.Broadcast(plugin.Config.UIUBroadcastTime, plugin.Config.UIUBroadcast);
                            }
                        }
                        Timing.CallDelayed(0.01f, () => {
                            switch (player.Role)
                            {
                                case RoleType.NtfCadet:
                                    player.Health = plugin.Config.UIUSoldierLife;
                                    Timing.CallDelayed(0.4f, () => { player.Position = SpawnPos; });
                                    player.ResetInventory(plugin.Config.UIUSoldierInventory);
                                    //NOTE: Add possibilities for the inventory system
                                    player.BadgeHidden = false;
                                    player.RankName = plugin.Config.UIUSoldierRank;
                                    player.RankColor = "yellow";
                                    break;
                                case RoleType.NtfLieutenant:
                                    player.Health = plugin.Config.UIUAgentLife;
                                    Timing.CallDelayed(0.4f, () => { player.Position = SpawnPos; });
                                    player.ResetInventory(plugin.Config.UIUAgentInventory);
                                    player.BadgeHidden = false;
                                    player.RankName = plugin.Config.UIUAgentRank;
                                    player.RankColor = "yellow";
                                    break;
                                case RoleType.NtfCommander:
                                    player.Health = plugin.Config.UIULeaderLife;
                                    Timing.CallDelayed(0.4f, () => { player.Position = SpawnPos; });
                                    player.ResetInventory(plugin.Config.UIULeaderInventory);
                                    player.BadgeHidden = false;
                                    player.RankName = plugin.Config.UIULeaderRank;
                                    player.RankColor = "yellow";
                                    break;
                            }
                        });
                    }
                }
            respawns++;
            }
        }

        public void OnAnnouncingMTF(AnnouncingNtfEntranceEventArgs ev)
        {
            if (randnums <= plugin.Config.probability & respawns >= plugin.Config.respawns + 1)
            {
                ev.IsAllowed = false;
                if (plugin.Config.DisableNTFAnnounce)
                {
                    Cassie.Message(plugin.Config.AnnouncementCassie);
                }
            }
        }

        public void OnDying(DiedEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Target.Id))
            {
                //NOTE: Fix this shit fix
                //Block users from using hidetag/showtag beeing UIU
                uiuPlayers.Remove(ev.Target.Id);
                ev.Target.ReferenceHub.serverRoles.NetworkMyText = rank;
                ev.Target.ReferenceHub.serverRoles.NetworkMyColor = "default";
                ev.Target.BadgeHidden = false;
            }
        }
        public void OnChanging(ChangingRoleEventArgs ev)
        {
            if (uiuPlayers.Contains(ev.Player.Id))
            {
                //NOTE: Fix this shit fix
                uiuPlayers.Remove(ev.Player.Id);
                ev.Player.ReferenceHub.serverRoles.NetworkMyText = rank;
                ev.Player.ReferenceHub.serverRoles.NetworkMyColor = "default";
                ev.Player.BadgeHidden = false;
            }
        }
    }
}
