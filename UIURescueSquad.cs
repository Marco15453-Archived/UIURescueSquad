namespace UIURescueSquad
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;

    using MapEvent = Exiled.Events.Handlers.Map;
    using PlayerEvent = Exiled.Events.Handlers.Player;
    using ServerEvent = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main class which inherits <see cref="Plugin{TConfig}"/>.
    /// </summary>
    public class UIURescueSquad : Plugin<Configs.Config>
    {
        /// <inheritdoc/>
        public static UIURescueSquad Instance;

        private Harmony hInstance;

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            hInstance = new Harmony($"jesus.uiurescuesquad-{DateTime.Now.Ticks}");
            hInstance.PatchAll();

            MapEvent.AnnouncingNtfEntrance += EventHandlers.OnAnnouncingNTF;

            ServerEvent.RespawningTeam += EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            ServerEvent.RoundStarted += EventHandlers.OnRoundStart;

            PlayerEvent.Destroying += EventHandlers.OnDestroy;
            PlayerEvent.ChangingRole += EventHandlers.OnChanging;
            PlayerEvent.Died += EventHandlers.OnDied;

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            MapEvent.AnnouncingNtfEntrance -= EventHandlers.OnAnnouncingNTF;

            ServerEvent.RespawningTeam -= EventHandlers.OnTeamRespawn;
            ServerEvent.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            ServerEvent.RoundStarted -= EventHandlers.OnRoundStart;

            PlayerEvent.Destroying -= EventHandlers.OnDestroy;
            PlayerEvent.ChangingRole -= EventHandlers.OnChanging;
            PlayerEvent.Died -= EventHandlers.OnDied;

            hInstance.UnpatchAll();
            Instance = null;

            base.OnDisabled();
        }

        /// <inheritdoc/>
        public override string Name { get; } = "UIURescueSquad";

        /// <inheritdoc/>
        public override string Author { get; } = "JesusQC, Michal78900, maintained by Marco15453";

        /// <inheritdoc/>
        public override string Prefix { get; } = "UIURescueSquad";

        /// <inheritdoc/>
        public override Version Version { get; } = new Version(3, 1, 0);

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(5, 0, 0);
    }
}
