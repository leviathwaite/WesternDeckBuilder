namespace WesternDeckBuilder.Models
{
    /// <summary>
    /// Converts raw heat integer values to <see cref="HeatTier"/> and provides
    /// tier-derived game parameters used by HeatManager and encounter resolution.
    /// </summary>
    public static class HeatTierUtility
    {
        private const int MaxHeat = 150;

        /// <summary>Returns the <see cref="HeatTier"/> for a given raw heat value.</summary>
        public static HeatTier GetTier(int heat)
        {
            if (heat <= 0)   return HeatTier.H0_Cold;
            if (heat <= 24)  return HeatTier.H1_Watched;
            if (heat <= 49)  return HeatTier.H2_Wanted;
            if (heat <= 74)  return HeatTier.H3_Hunted;
            if (heat <= 99)  return HeatTier.H4_Manhunt;
            return HeatTier.H5_DeadOrAlive;
        }

        /// <summary>
        /// Returns the lawmen spawn probability (0–1) for the given tier.
        /// Used by map travel resolution to roll for ambush encounters.
        /// </summary>
        public static float GetLawSpawnChance(HeatTier tier)
        {
            switch (tier)
            {
                case HeatTier.H0_Cold:        return 0.00f;
                case HeatTier.H1_Watched:     return 0.10f;
                case HeatTier.H2_Wanted:      return 0.22f;
                case HeatTier.H3_Hunted:      return 0.35f;
                case HeatTier.H4_Manhunt:     return 0.50f;
                case HeatTier.H5_DeadOrAlive: return 0.65f;
                default:                      return 0.00f;
            }
        }

        /// <summary>
        /// Returns the (min, max) enemy count range for lawmen encounters at this tier.
        /// Enemy composition is further refined by the encounter budget table.
        /// </summary>
        public static (int min, int max) GetEncounterRange(HeatTier tier)
        {
            switch (tier)
            {
                case HeatTier.H0_Cold:        return (0, 0);
                case HeatTier.H1_Watched:     return (1, 2);
                case HeatTier.H2_Wanted:      return (2, 3);
                case HeatTier.H3_Hunted:      return (3, 4);
                case HeatTier.H4_Manhunt:     return (4, 5);
                case HeatTier.H5_DeadOrAlive: return (5, 6);
                default:                      return (0, 0);
            }
        }

        /// <summary>Returns the elite enemy chance (0–1) for the given tier.</summary>
        public static float GetEliteChance(HeatTier tier)
        {
            switch (tier)
            {
                case HeatTier.H0_Cold:        return 0.00f;
                case HeatTier.H1_Watched:     return 0.00f;
                case HeatTier.H2_Wanted:      return 0.10f;
                case HeatTier.H3_Hunted:      return 0.20f;
                case HeatTier.H4_Manhunt:     return 0.35f;
                case HeatTier.H5_DeadOrAlive: return 0.50f;
                default:                      return 0.00f;
            }
        }

        /// <summary>Clamps a heat value to the valid range [0, MaxHeat].</summary>
        public static int Clamp(int heat)
        {
            return heat < 0 ? 0 : heat > MaxHeat ? MaxHeat : heat;
        }
    }
}
