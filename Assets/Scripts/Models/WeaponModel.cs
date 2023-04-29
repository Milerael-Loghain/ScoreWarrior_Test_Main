using System.Collections.Generic;
using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models.Characters;
using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models
{
	public class WeaponModel
	{
		private readonly WeaponView _view;
		private readonly WeaponDescriptor _descriptor;

		public WeaponDescriptor Descriptor => _descriptor;

		private uint _ammo;

		private bool _ready;
		private float _time;

		private readonly HashSet<BulletModel> _bulletModels;

		public WeaponModel(WeaponView view)
		{
			_view = view;
			_descriptor = view.GetComponent<WeaponDescriptor>();
			_ammo = _descriptor.ClipSize;

			_bulletModels = new HashSet<BulletModel>();
		}

		public bool IsReady => _ready;
		public bool HasAmmo => _ammo > 0;

		public void Reload()
		{
			_ammo = _descriptor.ClipSize;
		}

		public void Fire(CharacterModel target, bool hit)
		{
			if (_ammo > 0)
			{
				_ammo -= 1;

				var bulletModel = new BulletModel(_descriptor, _view.BulletPrefab, _view.BarrelTransform.position, target, hit);
				_bulletModels.Add(bulletModel);

				_time = 1.0f / _descriptor.FireRate;
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