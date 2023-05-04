using Scorewarrior.Test.Utility;

namespace Scorewarrior.Test.Modifiers
{
    public abstract class Modifier<Tkey>
    {
        public readonly EnumDictionary<Tkey, float> Multipliers;

        protected Modifier(EnumDictionary<Tkey, float> multipliers)
        {
            Multipliers = multipliers;
        }

        public void Apply(EnumDictionary<Tkey, float> currentStats)
        {
            foreach (var key in Multipliers.keys)
            {
                currentStats[key] *= Multipliers[key];
            }
        }
    }
}