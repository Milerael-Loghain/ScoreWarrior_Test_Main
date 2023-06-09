﻿using System.Collections.Generic;
using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Modifiers;
using Scorewarrior.Test.Utility;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models.Characters
{
    public class CharacterModel
    {
        private readonly CharacterView _view;
        private readonly WeaponModel _weaponModel;
        private readonly BattlefieldModel _battlefieldModel;
        private readonly EnumDictionary<CharacterStats, float> _currentStats;

        public CharacterView View => _view;
        public EnumDictionary<CharacterStats, float> CurrentStats => _currentStats;
        public WeaponModel WeaponModel => _weaponModel;
        public CharacterModel CurrentTarget { get; set; }

        public bool IsAlive => _health > 0 || _armor > 0;
        public Vector3 Position => _view.transform.position;

        private CharacterState _state;
        private float _health;
        private float _armor;
        private uint _team;

        public CharacterModel(uint team, CharacterView view, List<MainModifier> characterModifiers, WeaponModel weaponModel, BattlefieldModel battlefieldModel)
        {
            _view = view;
            _weaponModel = weaponModel;
            _battlefieldModel = battlefieldModel;
            var descriptor = _view.GetComponent<CharacterDescriptor>();
            _currentStats = descriptor.Stats.Clone();

            foreach (var characterModifier in characterModifiers)
            {
                characterModifier.Apply(_currentStats, weaponModel.CurrentStats);
            }

            _health = _currentStats[CharacterStats.MAXHEALTH];
            _armor = _currentStats[CharacterStats.MAXARMOR];
            _team = team;

            view.SetTeam(_team);
            view.HealthBar.SetMaxValue(_health);
            view.ArmorBar.SetMaxValue(_armor);

            view.SetHudActiveState(true);

            SetState(new IdleState(this));
        }

        public void Dispose()
        {
            if (_view != null)
            {
                Object.Destroy(_view.gameObject);
            }
        }

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
                _armor = Mathf.Clamp(_armor, 0, _currentStats[CharacterStats.MAXARMOR]);

                _view.ArmorBar.SetValue(_armor);
            }
            else if (_health > 0)
            {
                _health -= damage;
                _health = Mathf.Clamp(_health, 0, _currentStats[CharacterStats.MAXHEALTH]);

                _view.HealthBar.SetValue(_health);
            }

            if (IsAlive) return;

            _view.SetAnimatorTrigger(CharacterAnimationVariables.DIE);
            _view.SetHudActiveState(false);
        }
    }
}