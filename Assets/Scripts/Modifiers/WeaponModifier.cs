using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;

namespace Scorewarrior.Test.Modifiers
{
    public class WeaponModifier : Modifier<WeaponStats>
    {
        public WeaponModifier(EnumDictionary<WeaponStats, float> multipliers) : base(multipliers)
        {
        }
    }
}