using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace UIURescueSquad.Roles
{
    [CustomRole(RoleTypeId.NtfPrivate)]
    public class UiuSoldier : CustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.NtfPrivate;
        public override int MaxHealth { get; set; } = 120;
        public override string Name { get; set; } = "UIU Soldier";
        public override string Description { get; set; } = "Help MTFs to finish their job";
        public override string CustomInfo { get; set; } = "UIU Soldier";

        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.KeycardNTFLieutenant}",
            $"{ItemType.GunCrossvec}",
            $"{ItemType.GunCOM18}",
            $"{ItemType.Medkit}",
            $"{ItemType.Adrenaline}",
            $"{ItemType.Radio}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.ArmorCombat}"
        };

        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            { AmmoType.Nato556, 80 },
            { AmmoType.Nato9, 100 },
        };

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new()
                {
                    Role = RoleTypeId.NtfPrivate,
                    Chance = 100
                }
            }
        };
    }
}
