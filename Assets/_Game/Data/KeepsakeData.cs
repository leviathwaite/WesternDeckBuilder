using UnityEngine;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// Subtype flavor label for <see cref="KeepsakeData"/>.
    /// Trail Tokens and Badges are thematic families within the Keepsake item class.
    /// </summary>
    public enum KeepsakeSubtype
    {
        /// <summary>Standard keepsake with no special subtype label.</summary>
        Generic,
        /// <summary>Trail Token — commemorates a journey milestone.</summary>
        TrailToken,
        /// <summary>Badge — sheriff or scout motif; often tied to law/reputation effects.</summary>
        Badge
    }

    /// <summary>
    /// ScriptableObject definition for a Keepsake (relic-equivalent item).
    /// Create via Assets → Create → WesternDeckBuilder → Data → Keepsake.
    /// Place under Resources/Data/Keepsakes/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewKeepsake", menuName = "WesternDeckBuilder/Data/Keepsake")]
    public class KeepsakeData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private string _description;
        [SerializeField] private KeepsakeSubtype _subtype;
        [SerializeField] private string _passiveEffectKey;
        [SerializeField] private bool _isPersistent;

        /// <summary>Unique identifier used by inventory save data and DataRegistry lookups.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public KeepsakeSubtype Subtype => _subtype;
        /// <summary>Key resolved by the passive effect registry at runtime.</summary>
        public string PassiveEffectKey => _passiveEffectKey;
        /// <summary>If true this keepsake carries over to the next run (meta progression).</summary>
        public bool IsPersistent => _isPersistent;
    }
}
