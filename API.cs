namespace UIURescueSquad
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using PlayerRoles;

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
        public static UiuType GetUIURole(RoleTypeId roleType)
        {
            switch (roleType)
            {
                case RoleTypeId.NtfPrivate:
                    return UiuType.Soldier;

                case RoleTypeId.NtfSergeant:
                    return UiuType.Agent;

                case RoleTypeId.NtfCaptain:
                    return UiuType.Leader;

                default:
                    return UiuType.None;
            }
        }

        /// <inheritdoc cref="EventHandlers.SpawnPlayer(Player, UiuType)"/>
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
        /// Equivalent of <see cref="RoleType.NtfPrivate"/>
        /// </summary>
        Soldier = 0,

        /// <summary>
        /// Equivalent of <see cref="RoleType.NtfSergeant"/>
        /// </summary>
        Agent = 1,

        /// <summary>
        /// Equivalent of <see cref="RoleType.NtfCaptain"/>
        /// </summary>
        Leader = 2,
    }
}