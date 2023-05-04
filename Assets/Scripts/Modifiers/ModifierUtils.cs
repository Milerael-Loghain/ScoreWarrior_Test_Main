using System;
using Scorewarrior.Test.Utility;
using Random = UnityEngine.Random;

namespace Scorewarrior.Test.Modifiers
{
    public static class ModifierUtils
    {
        public static EnumDictionary<TEnum, float> GenerateRandomMultipliers<TEnum>(float minValue, float maxValue) where TEnum : Enum
        {
            var multipliers = new EnumDictionary<TEnum, float>();
            var stats = Enum.GetValues(typeof(TEnum));
            var modifiedStat = (TEnum) stats.GetValue(Random.Range(0, stats.Length));

            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var key = (TEnum) value;

                if (key.Equals(modifiedStat))
                {
                    multipliers[key] = Random.Range(minValue, maxValue);
                }
                else
                {
                    multipliers[key] = 1;
                }
            }

            return multipliers;
        }
    }
}