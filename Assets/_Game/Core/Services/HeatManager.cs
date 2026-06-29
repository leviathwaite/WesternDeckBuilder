using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Manages the player's heat value, enforcing clamping and tier resolution.
    /// Registered by <see cref="GameBootstrap"/> — do not instantiate directly.
    /// </summary>
    public sealed class HeatManager : IHeatManager
    {
        private const int MaxHeat = 150;
        private int _heat;

        public int Heat => _heat;
        public HeatTier Tier => HeatTierUtility.GetTier(_heat);

        public void IncreaseHeat(int amount)
        {
            if (amount <= 0) return;
            _heat = HeatTierUtility.Clamp(_heat + amount);
            Debug.Log($"[HeatManager] +{amount} heat → {_heat} (Tier: {Tier})");
        }

        public void DecreaseHeat(int amount)
        {
            if (amount <= 0) return;
            _heat = HeatTierUtility.Clamp(_heat - amount);
            Debug.Log($"[HeatManager] -{amount} heat → {_heat} (Tier: {Tier})");
        }

        public void ResetHeat()
        {
            _heat = 0;
            Debug.Log("[HeatManager] Heat reset to 0 (Tier: H0_Cold).");
        }
    }
}
