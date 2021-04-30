namespace UIURescueSquad.Configs.SubConfigs
{
    using System.ComponentModel;

    /// <summary>
    /// Configs for CustomTeamColors.
    /// </summary>
    public class TeamColors
    {
        /// <summary>
        /// Gets custom color for guard unit.
        /// </summary>
        [Description("Custom guard color (leave empty for default color)")]
        public string GuardUnitColor { get; private set; } = "#797D7F";

        /// <summary>
        /// Gets custom color for NTF units.
        /// </summary>
        [Description("Custom NTF color (leave empty for default color)")]
        public string NtfUnitColor { get; private set; } = "#0887E5";

        /// <summary>
        /// Gets custom color for UIU units.
        /// </summary>
        [Description("Custom UIU color (leave empty for default color)")]
        public string UiuUnitColor { get; private set; } = "yellow";
    }
}
