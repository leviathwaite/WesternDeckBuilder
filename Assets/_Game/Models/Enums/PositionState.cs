namespace WesternDeckBuilder.Models
{
    /// <summary>
    /// Player combat position state for MVP.
    /// Extended stance system (Stand / Crouch / Prone) is reserved for a future phase.
    /// </summary>
    public enum PositionState
    {
        /// <summary>Player is in the open — no cover bonus applied.</summary>
        Exposed,
        /// <summary>Player is behind cover — damage reduction and accuracy modifiers apply.</summary>
        Covered
    }
}
