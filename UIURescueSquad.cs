using System;
using Exiled.API.Features;
using UIURescueSquad.Handlers;
using HarmonyLib;

using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using MapEvent = Exiled.Events.Handlers.Map;

namespace UIURescueSquad
{
    public class UIURescueSquad : Plugin<Config>
    {
        public static UIURescueSquad Singleton;

        private Harmony hInstance;

        public static bool assemblySH = false;

        public override string Name { get; } = "UIU Rescue Squad";
        public override string Author { get; } = "JesusQC";
        public override string Prefix { get; } = "UIURescueSquad";
        public override Version Version { get; } = new Version(2, 1, 2);
        public override Version RequiredExiledVersion => new Version(2, 3, 4);

        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            Singleton = this;

            hInstance = new Harmony($"jesus.uiurescuesquad-{DateTime.Now.Ticks}");
            hInstance.PatchAll();

            EventHandlers = new EventHandlers(this);

            MapEvent.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingMTF;

            ServerEvent.RespawningTeam += EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            ServerEvent.RoundStarted += EventHandlers.OnRoundStart;

            PlayerEvent.Destroying += EventHandlers.OnDestroy;
            PlayerEvent.ChangingRole += EventHandlers.OnChanging;
            PlayerEvent.Died += EventHandlers.OnDying;

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            MapEvent.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingMTF;

            ServerEvent.RespawningTeam -= EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            ServerEvent.RoundStarted -= EventHandlers.OnRoundStart;

            PlayerEvent.Destroying -= EventHandlers.OnDestroy;
            PlayerEvent.ChangingRole -= EventHandlers.OnChanging;
            PlayerEvent.Died -= EventHandlers.OnDying;

            hInstance.UnpatchAll();
            EventHandlers = null;
            Singleton = null;

            base.OnDisabled();
        }
    }
}
