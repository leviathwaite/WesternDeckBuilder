using System.Collections.Generic;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Stub reward service. Rolls a named reward table and returns IDs of rewards to grant.
    /// Full reward-granting and inventory integration is reserved for a future phase.
    /// </summary>
    public interface IRewardService
    {
        /// <summary>
        /// Rolls all entries in the reward table identified by <paramref name="tableId"/>
        /// and returns the IDs of rewards that should be granted to the player.
        /// </summary>
        IReadOnlyList<string> RollRewards(string tableId);
    }
}
