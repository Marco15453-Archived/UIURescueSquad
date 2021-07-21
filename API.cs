namespace UIURescueSquad
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;

    /// <summary>
    /// The UIU Rescue Sqaud API with <see langword="static"/> methods.
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Checks if <see cref="Player"/> is UIU.
        /// </summary>
        /// <param name="player"> The player to check.</param>
        /// <returns><see langword="true"/> if player is UIU, <see langword="false"/> if not.</returns>
        public static bool IsUiu(Player player)
        {
            return player.SessionVariables.ContainsKey("IsUIU");
        }

        /// <summary>
        /// Gets a <see cref="UiuType"/> from provided <see cref="RoleType"/>.
        /// </summary>
        /// <param name="roleType">RoleType to check.</param>
        /// <returns>A valid <see cref="UiuType"/> or <see cref="UiuType.None"/> if the role couldn't be parsed.</returns>
        public static UiuType GetUIURole(RoleType roleType)
        {
            switch (roleType)
            {
                case RoleType.NtfCadet: return UiuType.Soldier;
                case RoleType.NtfLieutenant: return UiuType.Agent;
                case RoleType.NtfCommander: return UiuType.Leader;

                default: return UiuType.None;
            }
        }

        /// <summary>
        /// Spawns a player as UIU.
        /// </summary>
        /// <param name="player">The player to spawn.</param>
        /// <param name="uiuType">The UIU role of the spawned player.</param>
        public static void SpawnPlayer(Player player, UiuType uiuType)
        {
            EventHandlers.SpawnPlayer(player, uiuType);
        }

        /// <summary>
        /// Gets all alive UIU players.
        /// </summary>
        /// <returns><see cref="List{Player}"/> of all alive UIU players.</returns>
        public static List<Player> GetUIUPlayers()
        {
            return Player.List.Where(x => x.SessionVariables.ContainsKey("IsUIU")).ToList();
        }
    }

    /// <summary>
    /// Basically <see cref="RoleType"/>, but for UIU.
    /// </summary>
    public enum UiuType
    {
        /// <summary>
        /// RoleType isn't a UIU role.
        /// </summary>
        None = -1,

        /// <summary>
        /// Equivalent of <see cref="RoleType.NtfCadet"/>
        /// </summary>
        Soldier = 0,

        /// <summary>
        /// Equivalent of <see cref="RoleType.NtfLieutenant"/>
        /// </summary>
        Agent = 1,

        /// <summary>
        /// Equivalent of <see cref="RoleType.NtfCommander"/>
        /// </summary>
        Leader = 2,
    }
}
