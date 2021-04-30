namespace UIURescueSquad.Commands.SubCommands
{
    using System;
    using System.Collections.Generic;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using static API;

    /// <summary>
    /// A command which shows a list of players that are currently an UIU.
    /// </summary>
    public class List : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "list";

        /// <inheritdoc/>
        public string[] Aliases { get; } = { "l" };

        /// <inheritdoc/>
        public string Description { get; } = "Shows a list with players that are UIU.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("uiu.list"))
            {
                response = "You don't have permission to execute this command. Required permission: uiu.list";
                return false;
            }

            string message = "\nList of players that are UIU:\n";

            List<Player> uiuPlayers = GetUIUPlayers();

            foreach (var uiuPly in uiuPlayers)
            {
                message += $"- ({uiuPly.Id}) {uiuPly.Nickname} UIU {GetUIURole(uiuPly.Role)} {uiuPly.ReferenceHub.characterClassManager.NetworkCurUnitName}\n";
            }

            response = message;
            return true;
        }
    }
}
