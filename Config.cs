using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad
{
    public sealed class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug messages be shown in a server console.")]
        public bool Debug { get; set; } = false;

        [Description("How many mtfs respawns must have happened to spawn UIU")]
        public int respawns { get; set; } = 1;
        [Description("Probability of a UIU Squad replacing a MTF spawn")]
        public int probability { get; set; } = 50;
        [Description("Spawn position")]
        public float spawnPosX { get; set; } = 170.0f;
        public float spawnPosY { get; set; } = 985.0f;
        public float spawnPosZ { get; set; } = 29.0f;

        [Description("Entrance broadcast announcement message (null to disable it)")]
        public string AnnouncementText { get; set; } = "<b>The <color=#FFFA4B>UIU Rescue Squad</color> has arrived to the facility</b>";
        [Description("Entrance broadcast announcement message time")]
        public ushort AnnouncementTime { get; set; } = 10;

        
        [Description("UIU entrance Cassie Message")]
        public string uiuAnnouncementCassie { get; set; } = "The U I U Squad HasEntered AwaitingRecontainment {scpnum}";
        public string uiuAnnouncmentCassieNoScp { get; set; } = "The U I U Squad HasEntered NoSCPsLeft";

        [Description("NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)")]
        public string ntfAnnouncementCassie { get; set; } = "";
        public string ntfAnnouncmentCassieNoScp { get; set; } = "";




        [Description("Use hints instead of broadcasts?")]
        public bool UseHintsHere { get; set; } = false;
        [Description("UIU Player broadcast (null to disable it)")]
        public string UIUBroadcast { get; set; } = "<i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>";
        [Description("UIU Player broadcast (null to disable it)")]
        public ushort UIUBroadcastTime { get; set; } = 10;




        [Description("UIU Soldier life (NTF CADET)")]
        public int UIUSoldierLife { get; set; } = 160;
        [Description("The items UIUs soldiers spawn with")]
        public List<ItemType> UIUSoldierInventory { get; set; } = new List<ItemType>() { ItemType.KeycardNTFLieutenant, ItemType.GunProject90, ItemType.GunUSP, ItemType.Disarmer, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.GrenadeFrag };

        [Description("Ammo UIUs soldiers spawn with. (5.56, 7.62, 9mm)")]
        public List<uint> UIUSoldierAmmo { get; set; } = new List<uint>
        {
            80,0,100
        };
        [Description("UIU Soldier Rank (instead of Nine-Tailed Fox Cadet role)")]
        public string UIUSoldierRank { get; set; } = "UIU Soldier";



        [Description("UIU Agent life (NTF LIEUTENANT)")]
        public int UIUAgentLife { get; set; } = 175;
        [Description("The items UIUs agents spawn with")]
        public List<ItemType> UIUAgentInventory { get; set; } = new List<ItemType>() { ItemType.KeycardNTFLieutenant, ItemType.GunProject90, ItemType.GunUSP, ItemType.Disarmer, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.GrenadeFrag };

        [Description("Ammo UIUs agents spawn with (5.56, 7.62, 9mm)")]
        public List<uint> UIUAgentAmmo { get; set; } = new List<uint>
        {
            80,0,100
        };
        [Description("UIU Agent Rank (instead of Nine-Tailed Fox Lieutenant role)")]
        public string UIUAgentRank { get; set; } = "UIU Agent";



        [Description("UIU Leader life (NTF COMMANDER)")]
        public int UIULeaderLife { get; set; } = 215;
        [Description("The items UIU leader spawn with.")]
        public List<ItemType> UIULeaderInventory { get; set; } = new List<ItemType>() { ItemType.KeycardNTFLieutenant, ItemType.GunProject90, ItemType.GunUSP, ItemType.Disarmer, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.GrenadeFrag };

        [Description("Ammo UIUs leader spawn with. (5.56, 7.62, 9mm)")]
        public List<uint> UIULeaderAmmo { get; set; } = new List<uint>
        {
            80,0,100
        };
        [Description("UIU Leader Rank (instead of Nine-Tailed Fox Commander role)")]
        public string UIULeaderRank { get; set; } = "UIU Leader";

        [Description("Should plugin change colors for units? (leave empty for default color)")]
        public string GuardUnitColor { get; set; } = "#797D7F";
        public string NtfUnitColor { get; set; } = "#0887E5";
        public string UiuUnitColor { get; set; } = "yellow";

    }
}