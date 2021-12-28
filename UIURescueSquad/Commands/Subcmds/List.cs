using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace UIURescueSquad.Commands.Subcmds
{
    public class List : ICommand
    {
        public string Command { get; } = "list";
        public string[] Aliases { get; } = { "l" };
        public string Description { get; } = "Shows a list with players that are UIU.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("uiu.list"))
            {
                response = "You don't have permission to execute this command. Required permission: uiu.list";
                return false;
            }
            response = "\nList of players that are UIU:\n";

            foreach (Player player in API.GetUIUPlayers())
                response += $"- ({player.Id}) {player.Nickname} {API.GetUIURole(player.Role)} {player.UnitName}\n";
            return true;
        }
    }
}
