namespace UIURescueSquad.Commands.SubCommands
{
    using System;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using RemoteAdmin;
    using static API;

    /// <summary>
    /// A command which spawns a single UIU.
    /// </summary>
    public class Spawn : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "spawn";

        /// <inheritdoc/>
        public string[] Aliases { get; } = { "s" };

        /// <inheritdoc/>
        public string Description { get; } = "Makes player an UIU.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("uiu.spawn"))
            {
                response = "You don't have permission to execute this command. Required permission: uiu.spawn";
                return false;
            }

            Player player = Player.Get((sender as PlayerCommandSender).ReferenceHub);

            switch (arguments.Count)
            {
                case 0:
                    {
                        if (IsUiu(player) && GetUIURole(player.Role) == UiuType.Leader)
                        {
                            response = "You are already an UIU Leader.";
                            return false;
                        }

                        SpawnPlayer(player, UiuType.Leader);
                        response = "You are now a UIU Leader";
                        return true;
                    }

                case 1:
                    {
                        if (!Enum.TryParse(arguments.At(0), false, out UiuType uiuType))
                        {
                            response = $"{arguments.At(0)} cannot be parsed to any UiuType.";
                            return false;
                        }
                        else
                        {
                            SpawnPlayer(player, uiuType);

                            response = $"{player.Nickname} is now an UIU {uiuType}.";
                            return true;
                        }
                    }

                case 2:
                    {
                        player = Player.Get(arguments.At(1));
                        if (player == null)
                        {
                            response = "Provided player is invalid. Please give player's id or nickname.";
                            return false;
                        }

                        if (!Enum.TryParse(arguments.At(0), true, out UiuType uiuType))
                        {
                            response = $"{arguments.At(0)} cannot be parsed to any UiuType.";
                            return false;
                        }
                        else
                        {
                            if (IsUiu(player) && GetUIURole(player.Role) == uiuType)
                            {
                                response = $"{player.Nickname} is already a UIU";
                                return false;
                            }

                            SpawnPlayer(player, uiuType);

                            response = $"{player.Nickname} is now an UIU {uiuType}.";
                            return true;
                        }
                    }

                default: response = "Invalid number of arguments.";
                    return false;
            }
        }
    }
}
