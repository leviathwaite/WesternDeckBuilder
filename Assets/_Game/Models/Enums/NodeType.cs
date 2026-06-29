namespace WesternDeckBuilder.Models
{
    /// <summary>Types of nodes that can appear on the overworld county map.</summary>
    public enum NodeType
    {
        /// <summary>Safe rest point. Heat resets here. Allows loadout swapping.</summary>
        CampHideout,
        /// <summary>Settlement with NPCs, rumours, and law presence scaling with heat.</summary>
        Town,
        /// <summary>Merchant node for buying and selling items.</summary>
        Store,
        /// <summary>Train station. Mission prep hook and higher law patrol chance.</summary>
        RailStation,
        /// <summary>Travel path between major nodes. Random encounter rolls apply.</summary>
        Path,
        /// <summary>Wilderness discovery node. MVP placeholder yields a fur pelt.</summary>
        WildernessEvent,
        /// <summary>Named point of interest with unique events or guarded loot.</summary>
        Landmark
    }
}
