using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad
{
    public sealed class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug messages be shown in a server console")]
        public bool Debug { get; set; } = false;

        [Description("How many mtfs respawns must have happened to spawn UIU")]
        public int Respawns { get; set; } = 1;

        [Description("Probability of a UIU Squad replacing a MTF spawn")]
        public int Probability { get; set; } = 50;

        [Description("The maximum size of a UIU squad")]
        public int MaxSquad { get; set; } = 8;

        [Description("Should a drop spawn with UIUs")]
        public bool DropEnabled { get; set; } = true;

        [Description("List of items that appears in a drop")]
        public List<string> dropItems { get; set; } = new List<string>
        {
            "Medkit", "Painkillers", "Radio", "Ammo556", "Disarmer"
        };

        [Description("UIU Rescue squad spawn position:")]
        public float spawnPosX { get; set; } = 170.0f;
        public float spawnPosY { get; set; } = 985.0f;
        public float spawnPosZ { get; set; } = 29.0f;

        [Description("Entrance broadcast announcement message (null to disable it)")]
        public string AnnouncementText { get; set; } = "<b>The <color=#FFFA4B>UIU Rescue Squad</color> has arrived to the facility</b>";
        [Description("Entrance broadcast announcement message time")]
        public ushort AnnouncementTime { get; set; } = 10;

        [Description("UIU entrance Cassie Message")]
        public string UiuAnnouncementCassie { get; set; } = "The U I U Squad HasEntered AwaitingRecontainment {scpnum}";
        public string UiuAnnouncmentCassieNoScp { get; set; } = "The U I U Squad HasEntered NoSCPsLeft";

        [Description("NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)")]
        public string NtfAnnouncementCassie { get; set; } = "";
        public string NtfAnnouncmentCassieNoScp { get; set; } = "";

        [Description("Use hints instead of broadcasts?")]
        public bool UseHintsHere { get; set; } = false;

        [Description("UIU Player broadcast (null to disable it)")]
        public string UiuBroadcast { get; set; } = "<i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>";

        [Description("UIU Player broadcast (null to disable it)")]
        public ushort UiuBroadcastTime { get; set; } = 10;

        [Description("UIU Soldier life (NTF CADET)")]
        public int UiuSoldierLife { get; set; } = 160;

        [Description("The items UIUs soldiers spawn with")]
        public List<string> UiuSoldierInventory { get; set; } = new List<string>() { "KeycardNTFLieutenant", "GunProject90", "GunUSP", "Disarmer", "Medkit", "Adrenaline", "Radio", "GrenadeFrag" };

        [Description("Ammo UIUs soldiers spawn with.")]
        public Dictionary<AmmoType, uint> UiuSoldierAmmo { get; set; } = new Dictionary<AmmoType, uint>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };

        [Description("UIU Soldier Rank (instead of Nine-Tailed Fox Cadet role)")]
        public string UiuSoldierRank { get; set; } = "UIU Soldier";


        [Description("UIU Agent life (NTF LIEUTENANT)")]
        public int UiuAgentLife { get; set; } = 175;

        [Description("The items UIUs agents spawn with")]
        public List<string> UiuAgentInventory { get; set; } = new List<string>() { "KeycardNTFLieutenant", "GunProject90", "GunUSP", "Disarmer", "Medkit", "Adrenaline", "Radio", "GrenadeFrag" };

        [Description("Ammo UIUs agents spawn with.")]
        public Dictionary<AmmoType, uint> UIUAgentAmmo { get; set; } = new Dictionary<AmmoType, uint>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };
        [Description("UIU Agent Rank (instead of Nine-Tailed Fox Lieutenant role)")]
        public string UiuAgentRank { get; set; } = "UIU Agent";

        [Description("UIU Leader life (NTF COMMANDER)")]
        public int UiuLeaderLife { get; set; } = 215;

        [Description("The items UIU leader spawn with.")]
        public List<string> UiuLeaderInventory { get; set; } = new List<string>() { "KeycardNTFLieutenant", "GunProject90", "GunUSP", "Disarmer", "Medkit", "Adrenaline", "Radio", "GrenadeFrag" };

        [Description("Ammo UIUs leaders spawn with.")]
        public Dictionary<AmmoType, uint> UiuLeaderAmmo { get; set; } = new Dictionary<AmmoType, uint>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };
        [Description("UIU Leader Rank (instead of Nine-Tailed Fox Commander role)")]
        public string UiuLeaderRank { get; set; } = "UIU Leader";


        [Description("Should plugin change colors for units? (leave empty for default color)")]
        public string GuardUnitColor { get; set; } = "#797D7F";
        public string NtfUnitColor { get; set; } = "#0887E5";
        public string UiuUnitColor { get; set; } = "yellow";
    }
}