using System.Collections.Generic;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Descriptors;

namespace Scorewarrior.Test.Modifiers
{
    public static class MainModifierFactory
    {
        public static List<MainModifier> CreateModifiers(ModifiersConfig modifiersConfig)
        {
            var modifiers = new List<MainModifier>();

            for (int i = 0; i < modifiersConfig.CharacterModifiersAmount; i++)
            {
                var characterMultipliers =
                    ModifierUtils.GenerateRandomMultipliers<CharacterStats>(modifiersConfig.MinCharacterValueModifier, modifiersConfig.MaxCharacterValueModifier);
                var weaponMultipliers =
                    ModifierUtils.GenerateRandomMultipliers<WeaponStats>(modifiersConfig.MinWeaponValueModifier, modifiersConfig.MaxWeaponValueModifier);

                var modifier = new MainModifier(characterMultipliers, weaponMultipliers);
                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}