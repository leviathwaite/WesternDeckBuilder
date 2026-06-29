using System;
using System.Collections.Generic;
using UnityEngine;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Stub reward service. Rolls a named reward table and returns IDs of the rewards to grant.
    /// Full inventory-granting and UI flow is reserved for a future phase.
    /// </summary>
    public sealed class RewardService : IRewardService
    {
        /// <summary>
        /// Rolls all entries in the table identified by <paramref name="tableId"/> independently
        /// and returns the <c>RewardId</c> for each entry that passes its weight check.
        /// Returns an empty list if the table is not found.
        /// </summary>
        public IReadOnlyList<string> RollRewards(string tableId)
        {
            var dataRegistry = ServiceRegistry.Instance.Resolve<IDataRegistry>();
            if (dataRegistry == null)
                return Array.Empty<string>();

            var table = dataRegistry.GetRewardTable(tableId);
            if (table == null)
            {
                Debug.LogError($"[RewardService] Reward table '{tableId}' not found.");
                return Array.Empty<string>();
            }

            var results = new List<string>();
            foreach (var entry in table.Entries)
            {
                if (UnityEngine.Random.value <= entry.Weight)
                    results.Add(entry.RewardId);
            }

            Debug.Log($"[RewardService] Rolled table '{tableId}': {results.Count} rewards granted.");
            return results;
        }
    }
}
