namespace UIURescueSquad.Configs
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    using SubConfigs;

    /// <inheritdoc cref="IConfig"/>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        [Description("Is the plugin enabled.")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug mode is enabled.
        /// </summary>
        [Description("Should debug messages be shown in a server console.")]
        public bool Debug { get; set; } = false;

        /// <summary>
        /// Gets a <see cref="SpawnManager"/> configs.
        /// </summary>
        [Description("Options for UIU spawn:")]
        public SpawnManager SpawnManager { get; private set; } = new SpawnManager();

        /// <summary>
        /// Gets a <see cref="UiuLeader"/> configs.
        /// </summary>
        [Description("Options for UIU Leader:")]
        public UiuLeader UiuLeader { get; private set; } = new UiuLeader();

        /// <summary>
        /// Gets a <see cref="UiuAgent"/> configs.
        /// </summary>
        [Description("Options for UIU Agent:")]
        public UiuAgent UiuAgent { get; private set; } = new UiuAgent();

        /// <summary>
        /// Gets a <see cref="UiuSoldier"/> configs.
        /// </summary>
        [Description("Options for UIU Soldier:")]
        public UiuSoldier UiuSoldier { get; private set; } = new UiuSoldier();

        /// <summary>
        /// Gets a <see cref="TeamColors"/> configs.
        /// </summary>
        [Description("Options for custom team colors:")]
        public TeamColors TeamColors { get; private set; } = new TeamColors();

        /// <summary>
        /// Gets a <see cref="SupplyDrop"/> configs.
        /// </summary>
        [Description("Option for UIU supply drop:")]
        public SupplyDrop SupplyDrop { get; private set; } = new SupplyDrop();
    }
}