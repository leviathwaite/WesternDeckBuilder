namespace WesternDeckBuilder.Models
{
    /// <summary>
    /// Heat tier labels mapping to heat integer ranges.
    /// Use <see cref="HeatTierUtility"/> to convert a raw heat value to its tier.
    /// </summary>
    public enum HeatTier
    {
        /// <summary>Heat = 0. No law response.</summary>
        H0_Cold,
        /// <summary>Heat 1–24. Watched — occasional flavour warnings in town.</summary>
        H1_Watched,
        /// <summary>Heat 25–49. Wanted — store prices rise slightly.</summary>
        H2_Wanted,
        /// <summary>Heat 50–74. Hunted — path ambush bias increases.</summary>
        H3_Hunted,
        /// <summary>Heat 75–99. Manhunt — camp rest penalties apply.</summary>
        H4_Manhunt,
        /// <summary>Heat 100+. Dead or Alive — frequent law patrol encounters.</summary>
        H5_DeadOrAlive
    }
}
