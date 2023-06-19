using System.ComponentModel;
using Exiled.API.Interfaces;
using UIURescueSquad.Roles;

namespace UIURescueSquad.Configs
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug messages be shown in a server console.")]
        public bool Debug { get; set; } = false;

        [Description("Options for UIU spawn:")]
        public SpawnManager SpawnManager { get; private set; } = new SpawnManager();

        [Description("Options for UIU Leader:")]
        public UiuLeader UiuLeader { get; private set; } = new UiuLeader();

        [Description("Options for UIU Agent:")]
        public UiuAgent UiuAgent { get; private set; } = new UiuAgent();

        [Description("Options for UIU Soldier:")]
        public UiuSoldier UiuSoldier { get; private set; } = new UiuSoldier();

    }
}