namespace UIURescueSquad.Configs.SubConfigs
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;

    /// <summary>
    /// Configs for UIU Agent, equivalent of <see cref="RoleType.NtfSergeant"/>.
    /// </summary>
    public class UiuAgent
    {
        /// <summary>
        /// Gets UIU Agent health.
        /// </summary>
        [Description("UIU Agent health.")]
        public float Health { get; private set; } = 175f;

        /// <summary>
        /// Gets UIU Agent inventory.
        /// </summary>
        [Description("UIU Agent inventory:")]
        public List<string> Inventory { get; private set; } = new List<string>
        {
            "KeycardNTFLieutenant",
            "GunCrossvec",
            "GunCOM18",
            "Medkit",
            "Adrenaline",
            "Radio",
            "GrenadeHE",
            "ArmorCombat",
        };

        /// <summary>
        /// Gets UIU Agent ammo.
        /// </summary>
        [Description("UIU Agent ammo:")]
        public Dictionary<AmmoType, ushort> Ammo { get; private set; } = new Dictionary<AmmoType, ushort>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };

        /// <summary>
        /// Gets UIU Agent rank seen in-game instead of standard NTF role.
        /// </summary>
        [Description("UIU Agent rank seen in-game.")]
        public string Rank { get; private set; } = "UIU Agent";
    }
}
