using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad.Configs.Roles
{
    public class UiuAgent
    {
        [Description("UIU Agent health.")]
        public float Health { get; set; } = 175f;

        [Description("UIU Agent inventory:")]
        public List<string> Inventory { get; set; } = new List<string>
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

        [Description("UIU Agent ammo:")]
        public Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 }
        };

        [Description("UIU Agent rank seen in-game.")]
        public string Rank { get; set; } = "UIU Agent";
    }
}
