using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace UIURescueSquad
{
    /// <summary>
    /// API for Developers to interact with UIU.
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Checks if <see cref="Player"/> is UIU.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns><see langword="true"/> if player is UIU, <see langword="false"/> if not.</returns>
        public static bool IsUiu(Player player) => player.SessionVariables.ContainsKey("IsUIU");

        /// <summary>
        /// Gets a <see cref="UIUType"/> from provided <see cref="RoleType"/>.
        /// </summary>
        /// <param name="roleType">RoleType to check.</param>
        /// <returns>A valid <see cref="UiuType"/> or <see cref="UiuType.None"/> if the role couldn't be parsed.</returns>
        public static UIUType GetUIURole(RoleType roleType)
        {
            switch (roleType)
            {
                case RoleType.NtfPrivate:
                    return UIUType.Soldier;
                case RoleType.NtfSergeant:
                    return UIUType.Agent;
                case RoleType.NtfCaptain:
                    return UIUType.Leader;
                default:
                    return UIUType.None;
            }
        }

        /// <summary>
        /// Spawns a player as UIU.
        /// </summary>
        /// <param name="player">The player to spawn.</param>
        /// <param name="uiuType">The UIU role of the spawned player.</param>
        public static void SpawnPlayer(Player player, UIUType uiuType) => Extensions.SpawnPlayer(player, uiuType);

        /// <summary>
        /// Spawns UIU Rescue squad.
        /// </summary>
        /// <param name="playerList"> List of players to spawn.</param>
        public static void SpawnSquad(List<Player> players) => Extensions.SpawnSquad(players);

        /// <summary>
        /// Spawns UIU Rescue squad.
        /// </summary>
        public static void SpawnSquad() => Extensions.SpawnSquad();

        /// <summary>
        /// Gets all alive UIU players.
        /// </summary>
        /// <returns><see cref="List{Player}"/> of all alive UIU players.</returns>
        public static List<Player> GetUIUPlayers() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsUIU")).ToList();
    }

    /// <summary>
    /// Basically <see cref="RoleType"/>, but for UIU.
    /// </summary>
    public enum UIUType
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
        Leader = 2
    }
}
