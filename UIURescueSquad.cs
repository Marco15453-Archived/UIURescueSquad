using System;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

using MapEvent = Exiled.Events.Handlers.Map;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace UIURescueSquad
{
    public class UIURescueSquad : Plugin<Configs.Config>
    {

        public override string Name { get; } = "UIURescueSquad";
        public override string Author { get; } = "JesusQC, Michal78900, maintained by Marco15453";
        public override string Prefix { get; } = "UIURescueSquad";
        public override Version Version { get; } = new Version(5, 0, 0);
        public override Version RequiredExiledVersion => new Version(7, 0, 0);

        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Config.UiuSoldier.Register();
            Config.UiuAgent.Register();
            Config.UiuLeader.Register();

            eventHandlers = new EventHandlers(this);

            ServerEvent.RoundStarted += eventHandlers.OnRoundStarted;
            ServerEvent.RespawningTeam += eventHandlers.OnRespawningTeam;
            MapEvent.AnnouncingNtfEntrance += eventHandlers.OnAnnouncingNtfEntrance;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();

            ServerEvent.RoundStarted -= eventHandlers.OnRoundStarted;
            ServerEvent.RespawningTeam -= eventHandlers.OnRespawningTeam;
            MapEvent.AnnouncingNtfEntrance -= eventHandlers.OnAnnouncingNtfEntrance;

            eventHandlers = null;
            base.OnDisabled();
        }
    }
}
