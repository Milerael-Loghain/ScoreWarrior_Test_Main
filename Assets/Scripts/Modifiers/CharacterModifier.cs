using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Utility;

namespace Scorewarrior.Test.Modifiers
{
    public class CharacterModifier : Modifier<CharacterStats>
    {
        public CharacterModifier(EnumDictionary<CharacterStats, float> multipliers) : base(multipliers)
        {
        }
    }
}