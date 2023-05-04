using System.Collections.Generic;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Descriptors;

namespace Scorewarrior.Test.Modifiers
{
    public static class WeaponModifierFactory
    {
        public static List<WeaponModifier> CreateModifiers(ModifiersConfig modifiersConfig)
        {
            var modifiers = new List<WeaponModifier>();

            for (int i = 0; i < modifiersConfig.WeaponModifiersAmount; i++)
            {
                var multipliers =
                    ModifierUtils.GenerateRandomMultipliers<WeaponStats>(modifiersConfig.MinWeaponValueModifier, modifiersConfig.MaxWeaponValueModifier);
                var modifier = new WeaponModifier(multipliers);
                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}