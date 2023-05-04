using Scorewarrior.Test.Views;
using UnityEditor;

namespace Scorewarrior.Test.Utility
{
    [CustomPropertyDrawer(typeof(EnumDictionary<CharacterAnimationVariables, string>))]
    public class CharacterAnimationVariableStringDictionaryDrawer : EnumDictionaryDrawer<CharacterAnimationVariables, string>
    {
    }
}