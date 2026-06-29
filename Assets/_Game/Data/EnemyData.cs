using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// ScriptableObject definition for an enemy unit.
    /// Create via Assets → Create → WesternDeckBuilder → Data → Enemy.
    /// Place under Resources/Data/Enemies/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "WesternDeckBuilder/Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _budgetCost;
        [SerializeField] private EncounterType _encounterType;
        [SerializeField] private string _intentPatternKey;
        [SerializeField] private string _prefabKey;

        /// <summary>Unique identifier used by encounter director and DataRegistry.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public int MaxHealth => _maxHealth;
        /// <summary>Points consumed from the encounter budget when this enemy is spawned.</summary>
        public int BudgetCost => _budgetCost;
        /// <summary>The encounter scenario this enemy belongs to.</summary>
        public EncounterType EncounterType => _encounterType;
        /// <summary>Key used to load the enemy's telegraphed intent pattern from the behaviour registry.</summary>
        public string IntentPatternKey => _intentPatternKey;
        /// <summary>Resources or Addressables key for the enemy prefab. Resolved at runtime — no drag-drop needed.</summary>
        public string PrefabKey => _prefabKey;
    }
}
