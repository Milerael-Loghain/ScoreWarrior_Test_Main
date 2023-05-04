using UnityEngine;

namespace Scorewarrior.Test.Data
{
    [CreateAssetMenu]
    public class ModifiersConfig : ScriptableObject
    {
        public int CharacterModifiersAmount => _characterModifiersAmount;
        public float MinCharacterValueModifier => _minCharacterValueModifier;
        public float MaxCharacterValueModifier => _maxCharacterValueModifier;

        public int WeaponModifiersAmount => _weaponModifiersAmount;
        public float MinWeaponValueModifier => _minWeaponValueModifier;
        public float MaxWeaponValueModifier => _maxWeaponValueModifier;

        [Header("Characters")]
        [SerializeField]
        private int _characterModifiersAmount;
        [SerializeField]
        private float _minCharacterValueModifier;
        [SerializeField]
        private float _maxCharacterValueModifier;

        [Header("Weapons")]
        [SerializeField]
        private int _weaponModifiersAmount;
        [SerializeField]
        private float _minWeaponValueModifier;
        [SerializeField]
        private float _maxWeaponValueModifier;
    }
}