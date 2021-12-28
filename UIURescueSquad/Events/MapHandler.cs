using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace UIURescueSquad.Events
{
    internal sealed class MapHandler
    {
        private static UIURescueSquad plugin = UIURescueSquad.Singleton;
        private static Config config = UIURescueSquad.Singleton.Config;

        public void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            if (!plugin.IsSpawnable)
                return;

            string cassieMessage = string.Empty;

            if (ev.ScpsLeft == 0 && !string.IsNullOrEmpty(config.SpawnManager.EntryAnnoucementNoScp))
                cassieMessage = config.SpawnManager.EntryAnnoucementNoScp;
            else if (ev.ScpsLeft > 0 && !string.IsNullOrEmpty(config.SpawnManager.EntryAnnoucement))
                cassieMessage = config.SpawnManager.EntryAnnoucement;

            ev.IsAllowed = false;

            cassieMessage = cassieMessage.Replace("{scpnum}", $"{ev.ScpsLeft} scpsubject").Replace("{designation}", $"nato_{ev.UnitName[0]} {ev.UnitNumber}");

            if (ev.ScpsLeft > 1)
                cassieMessage = cassieMessage.Replace("scpsubject", "scpsubjects");

            Cassie.GlitchyMessage(cassieMessage, 0.05f, 0.05f);
        }
    }
}
