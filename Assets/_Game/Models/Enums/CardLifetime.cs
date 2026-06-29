namespace WesternDeckBuilder.Models
{
    /// <summary>
    /// Defines how long a card remains in the game before it is removed.
    /// Use these keywords on <see cref="WesternDeckBuilder.Data.CardData"/> to drive runtime card management.
    /// </summary>
    public enum CardLifetime
    {
        /// <summary>Standard deck card — stays in the deck across all battles.</summary>
        Permanent,
        /// <summary>Consumed and removed from the game on play.</summary>
        OneTimeUse,
        /// <summary>Expires at the end of its trigger window whether played or not.</summary>
        SingleOpportunity,
        /// <summary>Removed from the hand if not played during the current turn.</summary>
        UseItOrLoseIt,
        /// <summary>Removed from the deck at the end of the current battle.</summary>
        Battlebound,
        /// <summary>Discarded or exhausted the moment it leaves the player's hand.</summary>
        Handbound
    }
}
