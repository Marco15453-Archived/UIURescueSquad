using Exiled.API.Features;
using HarmonyLib;
using System;

namespace UIURescueSquad.Patches
{
    [HarmonyPatch(typeof(Respawning.RespawnTickets), nameof(Respawning.RespawnTickets.DrawRandomTeam))]
    public class UIUSpawn
    {
        public static void Postfix(ref Respawning.SpawnableTeamType __result)
        {
            try
            {
                if (__result == Respawning.SpawnableTeamType.NineTailedFox)
                {
                    UIURescueSquad.Singleton.EventHandlers.IsSpawnable();
                }
            }
            catch(Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
