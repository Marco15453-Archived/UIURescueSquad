using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad.Configs.Roles
{
    public class UiuSoldier
    {
        [Description("UIU Soldier health.")]
        public float Health { get; private set; } = 160f;

        [Description("UIU Soldier inventory:")]
        public List<string> Inventory { get; private set; } = new List<string>
        {
            "KeycardNTFLieutenant",
            "GunCrossvec",
            "GunCOM18",
            "Medkit",
            "Adrenaline",
            "Radio",
            "GrenadeFrag",
            "ArmorCombat",
        };

        [Description("UIU Soldier ammo:")]
        public Dictionary<AmmoType, ushort> Ammo { get; private set; } = new Dictionary<AmmoType, ushort>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };

        [Description("UIU Soldier rank seen in-game.")]
        public string Rank { get; private set; } = "UIU Soldier";
    }
}
