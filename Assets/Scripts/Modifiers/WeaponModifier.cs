using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;

namespace Scorewarrior.Test.Modifiers
{
    public class WeaponModifier
    {
        private readonly EnumDictionary<WeaponStats, float> WeaponMultipliers;

        public WeaponModifier(EnumDictionary<WeaponStats, float> weaponMultipliers)
        {
            WeaponMultipliers = weaponMultipliers;
        }

        public void Apply(EnumDictionary<WeaponStats, float> weaponStats)
        {
            foreach (var key in WeaponMultipliers.keys)
            {
                weaponStats[key] *= WeaponMultipliers[key];
            }
        }
    }
}