using Exiled.API.Features;
using HarmonyLib;
using System;
using UIURescueSquad.Events;
using Map = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace UIURescueSquad
{
    public class UIURescueSquad : Plugin<Config>
    {
        public static UIURescueSquad Singleton;

        public override string Name => "UIU Rescue Squad";
        public override string Author => "JesusQC, Michal78900 and Marco15453";
        public override Version Version => new Version(4, 0, 0);
        public override Version RequiredExiledVersion => new Version(4, 1, 7);

        public int TeamRespawnCount;
        public int MTFRespawnCount;
        public int UIURespawnCount;
        public bool IsSpawnable;

        private Harmony harmony;

        private MapHandler mapHandler;
        private PlayerHandler playerHandler;
        private ServerHandler serverHandler;

        public override void OnEnabled()
        {
            Singleton = this;

            harmony = new Harmony($"marco15453.uiurescuesquad-{DateTime.Now.Ticks}");
            harmony.PatchAll();

            registerEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll();
            unregisterEvents();
            base.OnDisabled();
        }

        private void registerEvents()
        {
            mapHandler = new MapHandler();
            playerHandler = new PlayerHandler();
            serverHandler = new ServerHandler();

            // Map
            Map.AnnouncingNtfEntrance += mapHandler.OnAnnouncingNtfEntrance;

            // Player
            Player.Destroying += playerHandler.OnDestroying;
            Player.Died += playerHandler.OnDied;
            Player.ChangingRole += playerHandler.OnChangingRole;

            // Server
            Server.WaitingForPlayers += serverHandler.OnWaitingForPlayers;
            Server.RoundStarted += serverHandler.OnRoundStarted;
            Server.RespawningTeam += serverHandler.OnRespawningTeam;
        }

        private void unregisterEvents()
        {
            // Map
            Map.AnnouncingNtfEntrance -= mapHandler.OnAnnouncingNtfEntrance;

            // Player
            Player.Destroying -= playerHandler.OnDestroying;
            Player.Died -= playerHandler.OnDied;
            Player.ChangingRole -= playerHandler.OnChangingRole;

            // Server
            Server.WaitingForPlayers -= serverHandler.OnWaitingForPlayers;
            Server.RoundStarted -= serverHandler.OnRoundStarted;
            Server.RespawningTeam -= serverHandler.OnRespawningTeam;

            mapHandler = null;
            playerHandler = null;
            serverHandler = null;
        }
    }
}
