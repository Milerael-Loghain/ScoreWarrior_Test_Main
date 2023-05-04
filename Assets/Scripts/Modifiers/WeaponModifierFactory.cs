using System;
using System.Collections.Generic;
using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;
using Random = UnityEngine.Random;

namespace Scorewarrior.Test.Modifiers
{
    public static class WeaponModifierFactory
    {
        public static List<WeaponModifier> CreateModifiers(int amount, int minValue, int maxValue)
        {
            var modifiers = new List<WeaponModifier>();

            for (int i = 0; i < amount; i++)
            {
                var multipliers = new EnumDictionary<WeaponStats, float>();
                var stats = Enum.GetValues(typeof(WeaponStats));
                var modifierStat = (WeaponStats) stats.GetValue(Random.Range(0, stats.Length));

                foreach (var key in multipliers.keys)
                {
                    if (key == modifierStat)
                    {
                        multipliers[key] = Random.Range(minValue, maxValue);
                    }
                    else
                    {
                        multipliers[key] = 1;
                    }
                }

                var modifier = new WeaponModifier(multipliers);
                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}