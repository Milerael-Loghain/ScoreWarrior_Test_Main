using System.Collections.Generic;
using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models.Characters;
using Scorewarrior.Test.Modifiers;
using Scorewarrior.Test.Utility;
using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models
{
	public class WeaponModel
	{
		private readonly WeaponView _view;
		private readonly EnumDictionary<WeaponStats, float> _currentStats;
		private readonly HashSet<BulletModel> _bulletModels;

		public EnumDictionary<WeaponStats, float>  CurrentStats => _currentStats;

		private bool _ready;
		private float _time;
		private uint _ammo;

		public WeaponModel(WeaponView view, List<WeaponModifier> weaponModifiers)
		{
			_view = view;
			var descriptor = view.GetComponent<WeaponDescriptor>();
			_currentStats = descriptor.Stats.Clone();

			foreach (var weaponModifier in weaponModifiers)
			{
				weaponModifier.Apply(_currentStats);
			}

			_bulletModels = new HashSet<BulletModel>();
		}

		public bool IsReady => _ready;
		public bool HasAmmo => _ammo  > 0;

		public void Reload()
		{
			_ammo = (uint)_currentStats[WeaponStats.CLIPSIZE];
		}

		public void Fire(CharacterModel target, bool hit)
		{
			if (_ammo  > 0)
			{
				_ammo  -= 1;

				var bulletModel = new BulletModel(_currentStats, _view.BulletPrefab, _view.BarrelTransform.position, target, hit);
				_bulletModels.Add(bulletModel);

				_time = 1.0f / _currentStats[WeaponStats.FIRERATE];
				_ready = false;
			}
		}

		public void Update(float deltaTime)
		{
			if (!_ready)
			{
				if (_time > 0)
				{
					_time -= deltaTime;
				}
				else
				{
					_ready = true;
				}
			}

			UpdateBulletModels();
		}

		private void UpdateBulletModels()
		{
			_bulletModels.RemoveWhere(bulletModel => bulletModel.IsDestroyed);

			foreach (var bulletModel in _bulletModels)
			{
				bulletModel.Update();
			}
		}
	}
}