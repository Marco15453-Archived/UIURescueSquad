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
        private static readonly Lazy<UIURescueSquad> LazyInstance = new Lazy<UIURescueSquad>(() => new UIURescueSquad());
        public static UIURescueSquad Instance => LazyInstance.Value;

        private Harmony hInstance;

        public override string Name { get; } = "UIU Rescue Squad";
        public override string Author { get; } = "JesusQC";
        public override string Prefix { get; } = "UIURescueSquad";

        private UIURescueSquad() { }

        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            base.OnEnabled();

            hInstance = new Harmony("jesus.uiurescuesquad");
            hInstance.PatchAll();

            EventHandlers = new EventHandlers();

            MapEvent.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingMTF;

            ServerEvent.RespawningTeam += EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;

            PlayerEvent.Left += EventHandlers.OnLeft;
            PlayerEvent.ChangingRole += EventHandlers.OnChanging;
            PlayerEvent.Died += EventHandlers.OnDying;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();

            MapEvent.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingMTF;

            ServerEvent.RespawningTeam -= EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;

            PlayerEvent.Left -= EventHandlers.OnLeft;
            PlayerEvent.ChangingRole -= EventHandlers.OnChanging;
            PlayerEvent.Died -= EventHandlers.OnDying;

            hInstance.UnpatchAll();
            EventHandlers = null;
        }
    }
}
