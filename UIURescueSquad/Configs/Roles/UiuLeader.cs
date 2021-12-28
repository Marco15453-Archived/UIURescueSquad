using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad.Configs.Roles
{
    public class UiuLeader
    {
        [Description("UIU Leader health.")]
        public float Health { get; private set; } = 215f;

        [Description("UIU Leader inventory:")]
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

        [Description("UIU Leader ammo:")]
        public Dictionary<AmmoType, ushort> Ammo { get; private set; } = new Dictionary<AmmoType, ushort>
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Nato9, 100 },
        };

        [Description("UIU Leader rank seen in-game.")]
        public string Rank { get; private set; } = "UIU Leader";
    }
}
