using Scorewarrior.Test.Descriptors;
using UnityEditor;

namespace Scorewarrior.Test.Utility
{
    [CustomPropertyDrawer(typeof(EnumDictionary<WeaponStats, float>))]
    public class WeaponStatsDictionaryDrawer : EnumDictionaryDrawer<WeaponStats, float>
    {
    }
}