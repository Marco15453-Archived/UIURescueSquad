<<<<<<< HEAD:UIURescueSquad/Commands/UIURescueSquadParentCommand.cs
﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using UIURescueSquad.Commands.Subcmds;

namespace UIURescueSquad.Commands
=======
﻿namespace UIURescueSquad.Commands
>>>>>>> parent of 516c8b8 (Big Rework):Commands/UIURescueSquadParentCommand.cs
{
    using System;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using RemoteAdmin;
    using SubCommands;

    /// <summary>
    /// The base parent command.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class UIURescueSquadParentCommand : ParentCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIURescueSquadParentCommand"/> class.
        /// </summary>
        public UIURescueSquadParentCommand() => LoadGeneratedCommands();

        /// <inheritdoc/>
        public override string Command => "uiu";

        /// <inheritdoc/>
        public override string[] Aliases => Array.Empty<string>();

        /// <inheritdoc/>
        public override string Description => "Parent command for UIURescueSquad";

        /// <inheritdoc/>
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new List());
            RegisterCommand(new Spawn());
            RegisterCommand(new SpawnTeam());
        }

        /// <inheritdoc/>
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as PlayerCommandSender).ReferenceHub);

            response = "\nPlease enter a valid subcommand:\n";

            foreach (var command in AllCommands)
            {
                if (player.CheckPermission($"sh.{command.Command}"))
                {
                    response += $"- {command.Command} ({command.Aliases[0]})\n";
                }
            }

            return false;
        }
    }
}
