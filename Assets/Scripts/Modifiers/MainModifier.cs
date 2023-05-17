using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;

namespace Scorewarrior.Test.Modifiers
{
    public class MainModifier
    {
        private readonly EnumDictionary<CharacterStats, float> CharacterMultipliers;
        private readonly EnumDictionary<WeaponStats, float> WeaponMultipliers;

        public MainModifier(EnumDictionary<CharacterStats, float> characterMultipliers, EnumDictionary<WeaponStats, float> weaponMultipliers)
        {
            CharacterMultipliers = characterMultipliers;
            WeaponMultipliers = weaponMultipliers;
        }

        public void Apply(EnumDictionary<CharacterStats, float> characterStats, EnumDictionary<WeaponStats, float> weaponStats)
        {
            foreach (var key in CharacterMultipliers.keys)
            {
                characterStats[key] *= CharacterMultipliers[key];
            }

            foreach (var key in WeaponMultipliers.keys)
            {
                weaponStats[key] *= WeaponMultipliers[key];
            }
        }
    }
}