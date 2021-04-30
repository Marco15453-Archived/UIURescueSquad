namespace UIURescueSquad.Configs.SubConfigs
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;

    /// <summary>
    /// Configs for UIU Soldier, equivalent of <see cref="RoleType.NtfCadet"/>.
    /// </summary>
    public class UiuSoldier
    {
        /// <summary>
        /// Gets UIU Soldier health.
        /// </summary>
        [Description("UIU Soldier health.")]
        public float Health { get; private set; } = 160f;

        /// <summary>
        /// Gets UIU Soldier inventory.
        /// </summary>
        [Description("UIU Soldier inventory:")]
        public List<string> Inventory { get; private set; } = new List<string>
        {
            "KeycardNTFLieutenant",
            "GunProject90",
            "GunUSP",
            "Disarmer",
            "Medkit",
            "Adrenaline",
            "Radio",
            "GrenadeFrag",
        };

        /// <summary>
        /// Gets UIU Soldier ammo.
        /// </summary>
        [Description("UIU Soldier ammo:")]
        public Dictionary<AmmoType, uint> Ammo { get; private set; } = new Dictionary<AmmoType, uint>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };

        /// <summary>
        /// Gets UIU Soldier rank seen in-game instead of standard NTF role.
        /// </summary>
        [Description("UIU Soldier rank seen in-game.")]
        public string Rank { get; private set; } = "UIU Soldier";
    }
}