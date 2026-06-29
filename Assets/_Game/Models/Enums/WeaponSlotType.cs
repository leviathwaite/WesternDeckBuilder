namespace WesternDeckBuilder.Models
{
    /// <summary>Equipment slots available on the player's loadout.</summary>
    public enum WeaponSlotType
    {
        /// <summary>Melee weapon sheath (knife by default).</summary>
        Sheath,
        /// <summary>Sidearm pistol slot.</summary>
        Pistol,
        /// <summary>Long weapon slot — rifle, shotgun, or long melee (max 1 equipped).</summary>
        LongWeapon,
        /// <summary>Optional bow slot with stealth/hunting bonuses (reserved for future phase).</summary>
        Bow
    }
}
