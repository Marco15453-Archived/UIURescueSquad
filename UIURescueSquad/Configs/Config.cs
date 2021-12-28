using Exiled.API.Interfaces;
using System.ComponentModel;
using UIURescueSquad.Configs;
using UIURescueSquad.Configs.Roles;

namespace UIURescueSquad
{
    public sealed class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages should be shown.")]
        public bool Debug { get; set; } = false;

        [Description("Options for UIU spawn")]
        public SpawnManager SpawnManager { get; set; } = new SpawnManager();

        [Description("Options for custom team colors")]
        public TeamColors TeamColors { get; set; } = new TeamColors();

        [Description("Option for UIU supply drop")]
        public SupplyDrop SupplyDrop { get; set; } = new SupplyDrop();

        [Description("Options for UIU Leader")]
        public UiuLeader UiuLeader { get; set; } = new UiuLeader();

        [Description("Options for UIU Agent")]
        public UiuAgent UiuAgent { get; set; } = new UiuAgent();

        [Description("Options for UIU Soldier")]
        public UiuSoldier UiuSoldier { get; set; } = new UiuSoldier();
    }
}
