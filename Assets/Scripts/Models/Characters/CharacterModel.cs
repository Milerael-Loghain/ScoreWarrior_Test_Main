using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models.Characters
{
    public class CharacterModel
    {
        private readonly CharacterView _view;
        private readonly CharacterDescriptor _descriptor;
        private readonly WeaponModel _weaponModel;
        private readonly BattlefieldModel _battlefieldModel;

        public CharacterView View => _view;
        public CharacterDescriptor Descriptor => _descriptor;
        public WeaponModel WeaponModel => _weaponModel;

        public CharacterModel CurrentTarget { get; set; }

        private CharacterState _state;
        private float _health;
        private float _armor;
        private uint _team;

        public CharacterModel(uint team, CharacterView view, WeaponModel weaponModel, BattlefieldModel battlefieldModel)
        {
            _view = view;
            _weaponModel = weaponModel;
            _battlefieldModel = battlefieldModel;
            _descriptor = _view.GetComponent<CharacterDescriptor>();
            _health = _descriptor.MaxHealth;
            _armor = _descriptor.MaxArmor;
            _team = team;

            view.SetTeam(_team);
            view.HealthBar.SetMaxValue(_descriptor.MaxHealth);
            view.ArmorBar.SetMaxValue(_descriptor.MaxArmor);

            view.SetHudActiveState(true);

            SetState(new IdleState(this));
        }

        public bool IsAlive => _health > 0 || _armor > 0;

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

        public void HandleDamage(float damage)
        {
            if (_armor > 0)
            {
                _armor -= damage;
                _armor = Mathf.Clamp(_armor, 0, _descriptor.MaxArmor);

                _view.ArmorBar.SetValue(_armor);
            }
            else if (_health > 0)
            {
                _health -= damage;
                _health = Mathf.Clamp(_health, 0, _descriptor.MaxHealth);

                _view.HealthBar.SetValue(_health);
            }

            if (IsAlive) return;

            _view.SetAnimatorTrigger(CharacterAnimationVariables.DIE);
            _view.SetHudActiveState(false);
        }
    }
}