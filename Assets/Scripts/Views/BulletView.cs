using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models;
using Scorewarrior.Test.Models.Characters;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class BulletView : MonoBehaviour
	{
		private CharacterModel _target;
		private WeaponModel _weaponModel;
		private bool _hit;

		public void Init(WeaponModel weaponModel, CharacterModel target, bool hit)
		{
			_weaponModel = weaponModel;
			_target = target;
			_hit = hit;
		}

		public void Update()
		{
			Vector3 targetPosition = _target.Position + Vector3.up * 2.0f;
			Vector3 direction = Vector3.Normalize(targetPosition - transform.position);
			transform.position += direction / 30;
			float distance = Vector3.Distance(targetPosition, transform.position);
			if (distance < 0.01)
			{
				if (_hit)
				{
					WeaponDescriptor weaponDescriptor = _weaponModel.Descriptor;
					CharacterDescriptor targetDescriptor = _target.Descriptor;
					float damage = weaponDescriptor.Damage;
					if (_target.Armor > 0)
					{
						_target.Armor -= damage;
					}
					else if (_target.Health > 0)
					{
						_target.Health -= damage;
					}
					if (_target.Armor <= 0 && _target.Health <= 0)
					{
						_target.View.Animator.SetTrigger("die");
					}
				}
				Destroy(gameObject);
			}
		}


	}
}