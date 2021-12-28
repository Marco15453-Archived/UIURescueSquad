using System.Collections.Generic;
using System.ComponentModel;

namespace UIURescueSquad.Configs
{
    public class SupplyDrop
    {
        [Description("Should a drop spawn with UIUs")]
        public bool DropEnabled { get; set; } = false;

        [Description("List of items that appears in a drop (supports CustomItems)")]
        public Dictionary<string, uint> DropItems { get; set; } = new Dictionary<string, uint>
        {
            { "Medkit", 1 },
            { "Ammo556", 2 }
        };
    }
}
