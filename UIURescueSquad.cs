using System;
using Exiled.API.Features;
using HarmonyLib;
using Exiled.Loader;

using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using MapEvent = Exiled.Events.Handlers.Map;

namespace UIURescueSquad
{
    public class UIURescueSquad : Plugin<Config>
    {
        public static UIURescueSquad Singleton;

        private Harmony hInstance;

        public override string Name { get; } = "UIU Rescue Squad";
        public override string Author { get; } = "JesusQC";
        public override string Prefix { get; } = "UIURescueSquad";
        public override Version Version { get; } = new Version(2, 2, 0);
        public override Version RequiredExiledVersion => new Version(2, 8, 0);

        public EventHandlers EventHandlers;

        public static bool IsCustomItems = false;

        public override void OnEnabled()
        {
            Singleton = this;
            EventHandlers = new EventHandlers(this);

            hInstance = new Harmony($"jesus.uiurescuesquad-{DateTime.Now.Ticks}");
            hInstance.PatchAll();

            foreach (var plugin in Loader.Plugins)
            {
                if (plugin.Name.ToLower() == "customitems" && plugin.Config.IsEnabled)
                {
                    IsCustomItems = true;
                    Log.Debug("CustomItems plugin detected!", Config.Debug);
                    break;
                }
            }

            MapEvent.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingMTF;

            ServerEvent.RespawningTeam += EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            ServerEvent.RoundStarted += EventHandlers.OnRoundStart;

            PlayerEvent.Destroying += EventHandlers.OnDestroy;
            PlayerEvent.ChangingRole += EventHandlers.OnChanging;
            PlayerEvent.Died += EventHandlers.OnDied;

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
            PlayerEvent.Died -= EventHandlers.OnDied;

            hInstance.UnpatchAll();
            EventHandlers = null;
            Singleton = null;

            base.OnDisabled();
        }
    }
}
