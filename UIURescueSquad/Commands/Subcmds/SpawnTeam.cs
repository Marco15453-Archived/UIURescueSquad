using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Respawning;
using System;
using System.Linq;

namespace UIURescueSquad.Commands.Subcmds
{
    public class SpawnTeam : ICommand
    {
        public string Command { get; } = "spawnteam";
        public string[] Aliases { get; } = { "st" };
        public string Description { get; } = "Spawns UIU team.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(!sender.CheckPermission("uiu.spawnteam"))
            {
                response = "You don't have permission to execute this command. Required permission: uiu.spawnteam";
                return false;
            }

            int validPlayers = Player.List.Where(x => x.Team == Team.RIP && !x.IsOverwatchEnabled).Count();
            if(arguments.Count == 0)
            {
                uint maxSquad = UIURescueSquad.Singleton.Config.SpawnManager.MaxSquad;
                if(validPlayers >= maxSquad)
                {
                    API.SpawnSquad();
                    response = $"UIU Rescue Squad with {maxSquad} has been spawned.";
                    return true;
                }
                response = $"There is not enough Spectators to spawn UIU Rescue Squad with {maxSquad} players. Required {maxSquad - validPlayers} more.";
                return false;
            }

            if(!uint.TryParse(arguments.At(0), out uint num) || num == 0)
            {
                response = $"'{num}' is not a valid number.";
                return false;
            }

            if(validPlayers >= num)
            {
                API.SpawnSquad();
                response = $"UIU Rescue Squad with {num} players has been spawned.";
                return true;
            }

            response = $"There is not enough Spectators to spawn UIU Rescue Squad with {num} players. Required {num - validPlayers} more.";
            return false;
        }
    }
}
