using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace UIURescueSquad.Commands.Subcmds
{
    public class Spawn : ICommand
    {
        public string Command { get; } = "spawn";
        public string[] Aliases { get; } = { "s" };
        public string Description { get; } = "Makes player an UIU.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("uiu.spawn"))
            {
                response = "You don't have permission to execute this command. Required permission: uiu.spawn";
                return false;
            }

            Player player = Player.Get(sender);

            if (arguments.Count == 0)
            {
                if (API.IsUiu(player) && API.GetUIURole(player.Role) == UIUType.Leader)
                {
                    response = "YOu are already an UIU Leader";
                    return false;
                }
                API.SpawnPlayer(player, UIUType.Leader);
                response = "You are now a UIU Leader";
                return true;
            }
            else if (arguments.Count == 1)
            {
                if (!Enum.TryParse(arguments.At(0), false, out UIUType uiuType))
                {
                    response = $"{arguments.At(0)} cannot be parsed to any UiuType";
                    return false;
                }
                API.SpawnPlayer(player, uiuType);
                response = $"{player.Nickname} ({player.Id}) is now an UIU {uiuType}";
                return true;
            }
            else if (arguments.Count == 2)
            {
                player = Player.Get(arguments.At(1));
                if (player == null)
                {
                    response = "Provided player is invalid, Please give player's id or nickname";
                    return false;
                }

                if (!Enum.TryParse(arguments.At(0), true, out UIUType uiuType))
                {
                    response = $"{arguments.At(0)} cannot be parsed to any UiuType";
                    return false;
                }

                if (API.IsUiu(player) && API.GetUIURole(player.Role) == uiuType)
                {
                    response = $"{player.Nickname} ({player.Id}) is already a UIU";
                    return false;
                }

                API.SpawnPlayer(player, uiuType);
                response = $"{player.Nickname} ({player.Id}) is now an UIU {uiuType}";
                return true;
            }

            response = "Invalid number of arguments";
            return false;
        }
    }
}
