namespace UIURescueSquad.Patches
{
    #pragma warning disable SA1313

    using System;
    using Exiled.API.Features;
    using HarmonyLib;
    using Respawning;

    /// <summary>
    /// Handles calling <see cref="EventHandlers.CalculateChance"/> when <see cref="SpawnableTeamType"/> is choosed.
    /// </summary>
    [HarmonyPatch(typeof(RespawnTickets), nameof(RespawnTickets.DrawRandomTeam))]
    public class UIUSpawn
    {
        /// <inheritdoc/>
        public static void Postfix(ref SpawnableTeamType __result)
        {
            try
            {
                if (__result == SpawnableTeamType.NineTailedFox)
                {
                    EventHandlers.CalculateChance();
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
