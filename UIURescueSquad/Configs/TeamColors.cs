using System.ComponentModel;

namespace UIURescueSquad.Configs
{
    public class TeamColors
    {
        [Description("Custom guard color (Empty = Default)")]
        public string GuardUnitColor { get; set; } = "#797D7F";

        [Description("Custom NTF color (Empty = Default)")]
        public string NtfUnitColor { get; set; } = "#0887E5";

        [Description("Custom UIU color (Empty = Default)")]
        public string UiuUnitColor { get; set; } = "yellow";
    }
}
