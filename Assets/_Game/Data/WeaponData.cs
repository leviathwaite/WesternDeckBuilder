using UnityEngine;
using WesternDeckBuilder.Models;

namespace WesternDeckBuilder.Data
{
    /// <summary>
    /// ScriptableObject definition for an equippable weapon.
    /// Create via Assets → Create → WesternDeckBuilder → Data → Weapon.
    /// Place under Resources/Data/Weapons/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "WesternDeckBuilder/Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private string _description;
        [SerializeField] private WeaponSlotType _slotType;
        [SerializeField] private int _baseDamage;
        [SerializeField] private bool _isQuiet;
        [SerializeField] private int _reloadCost;
        [SerializeField] private string _startingCardPackId;

        /// <summary>Unique identifier used by loadout save data and DataRegistry lookups.</summary>
        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public WeaponSlotType SlotType => _slotType;
        public int BaseDamage => _baseDamage;
        /// <summary>Quiet weapons suppress alarm/heat gain in stealth-eligible encounters.</summary>
        public bool IsQuiet => _isQuiet;
        /// <summary>Grit cost to reload this weapon in combat.</summary>
        public int ReloadCost => _reloadCost;
        /// <summary>ID of the card pack injected into the deck when this weapon is equipped.</summary>
        public string StartingCardPackId => _startingCardPackId;
    }
}
