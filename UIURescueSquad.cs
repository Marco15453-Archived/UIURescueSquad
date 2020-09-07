using Exiled.API.Features;
using UIURescueSquad.Handlers;

namespace UIURescueSquad
{
    public class UIURescueSquad : Plugin<Config>
    {
        public EventHandlers EventHandlers;

        public override string Name { get; } = "UIU Rescue Squad";
        public override string Author { get; } = "JesusQC";
        public override string Prefix { get; } = "UIURescueSquad";

        public override void OnEnabled()
        {
            base.OnEnabled();

            EventHandlers = new EventHandlers(this);

            Exiled.Events.Handlers.Server.RespawningTeam += EventHandlers.OnTeamRespawn;
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ChangingRole += EventHandlers.OnChanging;
            Exiled.Events.Handlers.Player.Died += EventHandlers.OnDying;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();

            Exiled.Events.Handlers.Server.RespawningTeam -= EventHandlers.OnTeamRespawn;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ChangingRole -= EventHandlers.OnChanging;
            Exiled.Events.Handlers.Player.Died -= EventHandlers.OnDying;

            EventHandlers = null;
        }
    }
}
