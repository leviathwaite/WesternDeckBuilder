using System.Collections.Generic;
using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// ScriptableObject definition for a single playable card.
    /// Create via Assets → Create → WesternDeckBuilder → Data → Card.
    /// Place under Resources/Data/Cards/ so DataRegistry can discover it automatically.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCard", menuName = "WesternDeckBuilder/Data/Card")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private string _description;
        [SerializeField] private int _gritCost;
        [SerializeField] private CardLifetime _lifetime;
        [SerializeField] private int _damage;
        [SerializeField] private List<string> _tags = new List<string>();
        [SerializeField] private string _effectKey;

        /// <summary>Unique identifier referenced by deck lists and save data.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        /// <summary>Grit (energy) cost to play this card.</summary>
        public int GritCost => _gritCost;
        /// <summary>Governs how long the card exists before it is removed from the game.</summary>
        public CardLifetime Lifetime => _lifetime;
        public int Damage => _damage;
        /// <summary>
        /// Synergy tags for runtime modifier resolution.
        /// Examples: "pistol", "quiet", "cover", "crouch", "loud", "blade".
        /// </summary>
        public IReadOnlyList<string> Tags => _tags;
        /// <summary>Key resolved by the card effect registry to find the runtime effect handler.</summary>
        public string EffectKey => _effectKey;
    }
}
