namespace UIURescueSquad.Configs.SubConfigs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Configs for UIU supply drop.
    /// </summary>
    public class SupplyDrop
    {
        /// <summary>
        /// Gets a value indicating whether supply drops for UIU are enabled.
        /// </summary>
        [Description("Should a drop spawn with UIUs")]
        public bool DropEnabled { get; private set; } = false;

        /// <summary>
        /// Gets a items for a UIU supply drop.
        /// </summary>
        [Description("List of items that appears in a drop (supports CustomItems)")]
        public Dictionary<string, uint> DropItems { get; private set; } = new Dictionary<string, uint>
        {
            { "Medkit", 1 },
            { "Ammo556", 2 },
            { "Disarmer", 1 },
        };
    }
}
