namespace WesternDeckBuilder.Models
{
    /// <summary>Battle scenario types. Drives enemy composition and environment selection.</summary>
    public enum EncounterType
    {
        /// <summary>1–3 outlaw enemies in wilderness or town environments.</summary>
        Bandit,
        /// <summary>Heat-scaled lawmen/bounty hunter encounter (up to 6 enemies).</summary>
        Lawmen,
        /// <summary>Train Job Stage 1 — Secure entry while guards defend the exterior.</summary>
        TrainBoarding,
        /// <summary>Train Job Stage 2 — Clear interior and reach the vault car.</summary>
        TrainInfiltration,
        /// <summary>Train Job Stage 3 — Defend while cracking the safe with dynamite.</summary>
        TrainSafeDefense,
        /// <summary>Train Job Stage 4 — Escape route encounter or event chain.</summary>
        TrainEscape
    }
}
