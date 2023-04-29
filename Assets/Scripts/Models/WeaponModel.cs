using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models.Characters;
using Scorewarrior.Test.Views;
using UnityEngine;

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

		public WeaponModel(WeaponView view)
		{
			_view = view;
			_descriptor = view.GetComponent<WeaponDescriptor>();
			_ammo = _descriptor.ClipSize;
		}

		public bool IsReady => _ready;
		public bool HasAmmo => _ammo > 0;

		public WeaponView View => _view;

		public void Reload()
		{
			_ammo = _descriptor.ClipSize;
		}

		public void Fire(CharacterModel characterModel, bool hit)
		{
			if (_ammo > 0)
			{
				_ammo -= 1;

				var bulletObject = Object.Instantiate(_view.BulletPrefab);
				bulletObject.transform.position = _view.BarrelTransform.position;
				bulletObject.Init(this, characterModel, hit);

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
		}
	}
}