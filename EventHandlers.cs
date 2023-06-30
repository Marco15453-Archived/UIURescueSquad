using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Server;
using Exiled.Loader;
using MEC;
using PlayerRoles;
using Respawning;
using System.Collections.Generic;

namespace UIURescueSquad
{
    internal sealed class EventHandlers
    {

        private UIURescueSquad plugin;
        public EventHandlers(UIURescueSquad plugin) => this.plugin = plugin;

        private int Respawns = 0;
        private int UIURespawns = 0;
        public bool UIUSpawns = false;

        public void OnRoundStarted()
        {
            Respawns = 0;
            UIURespawns = 0;
            UIUSpawns = false;
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if(Loader.Random.Next(100) <= plugin.Config.SpawnManager.Probability &&
                Respawns >= plugin.Config.SpawnManager.Respawns &&
                UIURespawns < plugin.Config.SpawnManager.MaxSpawns &&
                ev.NextKnownTeam == SpawnableTeamType.NineTailedFox)
            {
                UIUSpawns = true;
                List<Player> players = new List<Player>();
                if (ev.Players.Count > plugin.Config.SpawnManager.MaxSquad)
                    players = ev.Players.GetRange(0, plugin.Config.SpawnManager.MaxSquad);
                else
                    players = ev.Players.GetRange(0, ev.Players.Count);

                Queue<RoleTypeId> queue = ev.SpawnQueue;
                foreach (RoleTypeId role in queue)
                {
                    if (players.Count <= 0)
                        break;
                    Player player = players.RandomItem();
                    players.Remove(player);
                    switch (role)
                    {
                        case RoleTypeId.NtfCaptain:
                            plugin.Config.UiuLeader.AddRole(player);
                            break;
                        case RoleTypeId.NtfSergeant:
                            plugin.Config.UiuAgent.AddRole(player);
                            break;
                        case RoleTypeId.NtfPrivate:
                            plugin.Config.UiuSoldier.AddRole(player);
                            break;
                    }
                }
                UIURespawns++;

                ev.NextKnownTeam = SpawnableTeamType.None;
            }
            Respawns++;
        }

        public void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            string cassieMessage = string.Empty;
            if (!UIUSpawns)
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.SpawnManager.NtfAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;
                    cassieMessage = plugin.Config.SpawnManager.NtfAnnouncmentCassieNoScp;
                }
                else if (ev.ScpsLeft >= 1 && !string.IsNullOrEmpty(plugin.Config.SpawnManager.NtfAnnouncementCassie))
                {
                    ev.IsAllowed = false;
                    cassieMessage = plugin.Config.SpawnManager.NtfAnnouncementCassie;
                }
            }
            else
            {
                if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(plugin.Config.SpawnManager.UiuAnnouncmentCassieNoScp))
                {
                    ev.IsAllowed = false;
                    cassieMessage = plugin.Config.SpawnManager.UiuAnnouncmentCassieNoScp;
                }
                else if (ev.ScpsLeft >= 1 && !string.IsNullOrEmpty(plugin.Config.SpawnManager.UiuAnnouncementCassie))
                {
                    ev.IsAllowed = false;
                    cassieMessage = plugin.Config.SpawnManager.UiuAnnouncementCassie;
                }
                UIUSpawns = false;
            }

            cassieMessage = cassieMessage.Replace("{scpnum}", $"{ev.ScpsLeft} scpsubject");

            if (ev.ScpsLeft > 1)
                cassieMessage = cassieMessage.Replace("scpsubject", "scpsubjects");

            cassieMessage = cassieMessage.Replace("{designation}", $"nato_{ev.UnitName[0]} {ev.UnitNumber}");

            if (!string.IsNullOrEmpty(cassieMessage))
                Cassie.Message(cassieMessage, isSubtitles: plugin.Config.SpawnManager.Subtitles);
        }
    }
}
