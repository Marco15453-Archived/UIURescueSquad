using System.Text;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace UIURescueSquad.Handlers
{
    public class EventHandlers
    {
        public static bool isSpawnable;

        public static List<Player> uiuPlayers = new List<Player>();

        private int respawns = 0;
        private int randnums;

        private static System.Random rand = new System.Random();

        private static Vector3 SpawnPos = new Vector3(170, 985, 29);
        //NOTE: Make spawnpos configurable

        public void OnWaitingForPlayers()
        {
            uiuPlayers.Clear();
            respawns = 0;
        }

        public void IsSpawnable()
        {
            randnums = rand.Next(1, 101);
            if (randnums <= UIURescueSquad.Instance.Config.probability && respawns >= UIURescueSquad.Instance.Config.respawns) isSpawnable = true;
            else isSpawnable = false;
        }

        public void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if (ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
            {
                //randnums = rand.Next(1, 101);
                //Log.Info(randnums);
                //if (randnums <= UIURescueSquad.Instance.Config.probability & respawns >= UIURescueSquad.Instance.Config.respawns)
                if (isSpawnable)
                {
                    if (UIURescueSquad.Instance.Config.AnnouncementText != null)
                    {
                        if (UIURescueSquad.Instance.Config.AnnouncementText != null && UIURescueSquad.Instance.Config.AnnouncementText != null)
                        {
                            Map.ClearBroadcasts();
                            Map.Broadcast(UIURescueSquad.Instance.Config.AnnouncementTime, UIURescueSquad.Instance.Config.AnnouncementText);
                        }
                    }
                    //Cassie.Message("Attention, the U I U HasEntered please help the MtfUnit that are AwaitingRecontainment .g7 ScpSubjects", true, true);
                    //NOTE: disable MTF entrance message to allow cassie messages
                    foreach (Player player in ev.Players)
                    {
                        uiuPlayers.Add(player);

                        if (UIURescueSquad.Instance.Config.UIUBroadcast != null && UIURescueSquad.Instance.Config.UIUBroadcastTime.ToString() != null)
                        {
                            if (UIURescueSquad.Instance.Config.UseHintsHere)
                            {
                                player.ShowHint(UIURescueSquad.Instance.Config.UIUBroadcast, UIURescueSquad.Instance.Config.UIUBroadcastTime);
                            }
                            else
                            {
                                player.ClearBroadcasts();
                                player.Broadcast(UIURescueSquad.Instance.Config.UIUBroadcastTime, UIURescueSquad.Instance.Config.UIUBroadcast);
                            }
                        }

                        Timing.CallDelayed(0.01f, () =>
                        {
                            switch (player.Role)
                            {
                                case RoleType.NtfCadet:
                                    {
                                        player.Health = UIURescueSquad.Instance.Config.UIUSoldierLife;

                                        player.ResetInventory(UIURescueSquad.Instance.Config.UIUSoldierInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = UIURescueSquad.Instance.Config.UIUSoldierAmmo[i];
                                        }

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = UIURescueSquad.Instance.Config.UIUSoldierRank;
                                        break;
                                    }

                                case RoleType.NtfLieutenant:
                                    {
                                        player.Health = UIURescueSquad.Instance.Config.UIUAgentLife;

                                        player.ResetInventory(UIURescueSquad.Instance.Config.UIUAgentInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = UIURescueSquad.Instance.Config.UIUAgentAmmo[i];
                                        }

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = UIURescueSquad.Instance.Config.UIUAgentRank;
                                        break;
                                    }

                                case RoleType.NtfCommander:
                                    {
                                        player.Health = UIURescueSquad.Instance.Config.UIULeaderLife;

                                        player.ResetInventory(UIURescueSquad.Instance.Config.UIULeaderInventory);

                                        for (int i = 0; i < 3; i++)
                                        {
                                            player.Ammo[i] = UIURescueSquad.Instance.Config.UIULeaderAmmo[i];
                                        }

                                        player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                                        player.CustomInfo = UIURescueSquad.Instance.Config.UIULeaderRank;
                                        break;
                                    }
                            }
                            Timing.CallDelayed(0.4f, () => { player.Position = SpawnPos; });
                        });
                    }
                }
                respawns++;
            }
        }

        public void OnAnnouncingMTF(AnnouncingNtfEntranceEventArgs ev)
        {
            if (isSpawnable && respawns >= UIURescueSquad.Instance.Config.respawns + 1)
            {
                ev.IsAllowed = false;

                if (UIURescueSquad.Instance.Config.DisableNTFAnnounce)
                {
                    if (ev.ScpsLeft == 0)
                    {
                        Cassie.GlitchyMessage(UIURescueSquad.Instance.Config.AnnouncmentCassieNoScp, 0.05f, 0.05f);
                    }

                    else
                    {
                        StringBuilder message = new StringBuilder();

                        message.Append(UIURescueSquad.Instance.Config.AnnouncementCassie);
                        message.Replace("{scpnum}", ev.ScpsLeft.ToString());

                        Cassie.GlitchyMessage(message.ToString(), 0.05f, 0.05f);
                    }
                }
            }
        }




        public void OnLeft(LeftEventArgs ev)
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
    }

    //Added LazyInstance (this was neccesary for making your plugin compatibile with my RespawnTimer plugin, if you don't want that you can get rid of it)
    //Replaced badges with custominfo
    //Added ammo config for classes
    //Cleaned up code (in main class I registered events in a shorter way)
    //uiuPlayers holds actual players, not id numbers (requiered for Custom Player Info)
    //Added KillUIU function which make deleting uiuPlayers from list easier
    //Remade Cassie announcment message. There are 2 diffrent configurable cassie announcments (one for when SCPs are dead). Cassie be also be glitchy meaing there is by default 5 % chance of jamming or .g(num)
    //Get rid of unused UseHints option in Config and string Rank in EventHandlers
}
