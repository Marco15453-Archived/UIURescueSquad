namespace UIURescueSquad.Commands.SubCommands
{
    using System;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using Respawning;
    using static API;

    /// <summary>
    /// A command which spawns an UIU team.
    /// </summary>
    public class SpawnTeam : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "spawnteam";

        /// <inheritdoc/>
        public string[] Aliases { get; } = { "st" };

        /// <inheritdoc/>
        public string Description { get; } = "Spawns UIU team.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("sh.spawnteam"))
            {
                response = "You don't have permission to execute this command. Required permission: sh.spawnteam";
                return false;
            }
            else
            {
                uint validPlayers = 0;
                foreach (Player player in Player.List.Where(x => x.Role.Team == Team.RIP && !x.IsOverwatchEnabled))
                {
                    validPlayers++;
                }

                if (arguments.Count == 0)
                {
                    uint maxSquad = UIURescueSquad.Instance.Config.SpawnManager.MaxSquad;

                    if (validPlayers >= maxSquad)
                    {
                        EventHandlers.IsSpawnable = true;
                        Respawn.ForceWave(SpawnableTeamType.NineTailedFox);
                        EventHandlers.IsSpawnable = false;

                        response = $"UIU Rescue Squad team has been successfully spawned with {maxSquad} players!";
                        return true;
                    }
                    else
                    {
                        response = $"There is not enough Spectators to spawn UIU Rescue Squad with {maxSquad} players. Required {maxSquad - validPlayers} more.";
                        return false;
                    }
                }
                else
                {
                    if (!uint.TryParse(arguments.At(0), out uint num) || num == 0)
                    {
                        response = $"\"{arguments.At(0)}\" is not a valid number.";
                        return false;
                    }

                    if (validPlayers >= num)
                    {
                        uint prevValue = EventHandlers.MaxPlayers;

                        EventHandlers.IsSpawnable = true;
                        EventHandlers.MaxPlayers = num;

                        Respawn.ForceWave(SpawnableTeamType.NineTailedFox);

                        EventHandlers.IsSpawnable = false;
                        EventHandlers.MaxPlayers = prevValue;

                        response = $"UIU Rescue Squad team with {num} players has been spawned.";
                        return true;
                    }
                    else
                    {
                        response = $"There is not enough Spectators to spawn UIU Rescue Squad with {num} players. Required {num - validPlayers} more.";
                        return false;
                    }
                }
            }
        }
    }
}
