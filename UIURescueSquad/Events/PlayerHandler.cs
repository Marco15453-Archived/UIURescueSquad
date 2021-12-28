using Exiled.API.Extensions;
using Exiled.Events.EventArgs;

namespace UIURescueSquad.Events
{
    internal sealed class PlayerHandler
    {
        private static UIURescueSquad plugin = UIURescueSquad.Singleton;
        private static Config config = UIURescueSquad.Singleton.Config;

        public void OnDestroying(DestroyingEventArgs ev)
        {
            if (API.IsUiu(ev.Player))
                Extensions.DestroyUIU(ev.Player);
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (API.IsUiu(ev.Target))
                Extensions.DestroyUIU(ev.Target);
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsUiu(ev.Player) && ev.NewRole.GetTeam() != Team.MTF)
                Extensions.DestroyUIU(ev.Player);
        }
    }
}
