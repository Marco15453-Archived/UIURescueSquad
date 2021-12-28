using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using UIURescueSquad.Commands.Subcmds;

namespace UIURescueSquad.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class UIURescueSquadParentCommand : ParentCommand
    {
        public UIURescueSquadParentCommand() => LoadGeneratedCommands();

        public override string Command => "uiu";
        public override string[] Aliases => Array.Empty<string>();
        public override string Description => "Parent command for UIURescueSquad";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new List());
            RegisterCommand(new Spawn());
            RegisterCommand(new SpawnTeam());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "\nPlease enter a valid subcommand:\n";
            foreach (var command in AllCommands)
                if (sender.CheckPermission($"uiu.{command.Command}"))
                    response += $"- {command.Command} ({command.Aliases[0]})\n";

            return false;
        }
    }
}
