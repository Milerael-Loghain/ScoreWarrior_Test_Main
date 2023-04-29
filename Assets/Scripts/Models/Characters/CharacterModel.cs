using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models.Characters
{
    public class CharacterModel
    {
        private CharacterState _state;

        private readonly CharacterView _view;
        private readonly CharacterDescriptor _descriptor;
        private readonly WeaponModel _weaponModel;

        public CharacterView View => _view;
        public CharacterDescriptor Descriptor => _descriptor;
        public WeaponModel WeaponModel => _weaponModel;

        private readonly BattlefieldModel _battlefieldModel;

        private float _health;
        private float _armor;


        public CharacterModel CurrentTarget { get; set; }

        private float _time;

        public CharacterModel(CharacterView view, WeaponModel weaponModel, BattlefieldModel battlefieldModel)
        {
            _view = view;
            _weaponModel = weaponModel;
            _battlefieldModel = battlefieldModel;
            _descriptor = _view.GetComponent<CharacterDescriptor>();
            _health = _descriptor.MaxHealth;
            _armor = _descriptor.MaxArmor;

            SetState(new IdleState(this));
        }

        public bool IsAlive => _health > 0 || _armor > 0;

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public float Armor
        {
            get => _armor;
            set => _armor = value;
        }

        public Vector3 Position => _view.transform.position;

        public void Update(float deltaTime)
        {
            if (!IsAlive) return;

            _state.Update(deltaTime);
        }

        public void SetState(CharacterState state)
        {
            _state?.Exit();
            _state = state;
            _state?.Enter();
        }

        public void AimAt(CharacterModel target)
        {
            _view.transform.LookAt(target.Position);
        }

        public void FireAt(CharacterModel target, bool hit)
        {
            _weaponModel.Fire(target, hit);
            _view.SetAnimatorTrigger(CharacterAnimationVariables.SHOOT);
        }

        public void Reload()
        {
            _weaponModel.Reload();
        }

        public bool HasAmmo()
        {
            return _weaponModel.HasAmmo;
        }

        public bool IsReady()
        {
            return _weaponModel.IsReady;
        }

        public bool TryGetNearestAliveEnemy(out CharacterModel target)
        {
            return _battlefieldModel.TryGetNearestAliveEnemy(this, out target);
        }
    }
}