using System;
using System.Collections.Generic;
using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;
using Random = UnityEngine.Random;

namespace Scorewarrior.Test.Modifiers
{
    public static class CharacterModifierFactory
    {
        public static List<CharacterModifier> CreateModifiers(int amount, int minValue, int maxValue)
        {
            var modifiers = new List<CharacterModifier>();

            for (int i = 0; i < amount; i++)
            {
                var multipliers = new EnumDictionary<CharacterStats, float>();
                var stats = Enum.GetValues(typeof(CharacterStats));
                var modifierStat = (CharacterStats) stats.GetValue(Random.Range(0, stats.Length));

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

                var modifier = new CharacterModifier(multipliers);
                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}