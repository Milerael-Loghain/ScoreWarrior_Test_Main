using Scorewarrior.Test.Descriptors;
using UnityEditor;

namespace Scorewarrior.Test.Utility
{
    [CustomPropertyDrawer(typeof(EnumDictionary<CharacterStats, float>))]
    public class CharacterStatsDictionaryDrawer : EnumDictionaryDrawer<CharacterStats, float>
    {
    }
}