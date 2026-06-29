using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Manages the player's current heat value and tier.
    /// Heat decays on node travel and resets fully at safe houses.
    /// </summary>
    public interface IHeatManager
    {
        /// <summary>Raw heat value clamped to [0, 150].</summary>
        int Heat { get; }
        /// <summary>Tier derived from the current heat value.</summary>
        HeatTier Tier { get; }
        void IncreaseHeat(int amount);
        void DecreaseHeat(int amount);
        /// <summary>Sets heat to zero. Call when the player reaches a safe house / hideout.</summary>
        void ResetHeat();
    }
}
