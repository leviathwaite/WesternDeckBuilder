using System;
using System.Collections.Generic;
using UnityEngine;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// A single entry in a reward table. Defines what can be awarded and at what probability.
    /// </summary>
    [Serializable]
    public class RewardEntry
    {
        [SerializeField] private string _rewardId;
        [SerializeField] private string _rewardType;
        [SerializeField][Range(0f, 1f)] private float _weight;
        [SerializeField] private int _quantityMin = 1;
        [SerializeField] private int _quantityMax = 1;

        /// <summary>ID of the reward asset (card, keepsake, item, etc.).</summary>
        public string RewardId => _rewardId;
        /// <summary>
        /// Discriminator string for reward resolution.
        /// Expected values: "cash", "card", "keepsake", "trail_token", "badge", "item", "nothing".
        /// </summary>
        public string RewardType => _rewardType;
        /// <summary>Independent roll probability (0–1) for this entry.</summary>
        public float Weight => _weight;
        public int QuantityMin => _quantityMin;
        public int QuantityMax => _quantityMax;
    }

    /// <summary>
    /// ScriptableObject table of possible rewards for a node type or combat outcome.
    /// Create via Assets → Create → WesternDeckBuilder → Data → RewardTable.
    /// Place under Resources/Data/RewardTables/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewRewardTable", menuName = "WesternDeckBuilder/Data/RewardTable")]
    public class RewardTableData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<RewardEntry> _entries = new List<RewardEntry>();

        /// <summary>Unique identifier used by NodeData and EncounterDirector to look up this table.</summary>
        public string ID => _id;
        /// <summary>All possible reward entries in this table. Each entry rolls independently.</summary>
        public IReadOnlyList<RewardEntry> Entries => _entries;
    }
}
