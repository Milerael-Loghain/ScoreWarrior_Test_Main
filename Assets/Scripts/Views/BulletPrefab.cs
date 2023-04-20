using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class BulletPrefab : MonoBehaviour
	{
		private Character _target;
		private WeaponPrefab _weapon;
		private bool _hit;

		public void Init(WeaponPrefab weapon, Character target, bool hit)
		{
			_weapon = weapon;
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
					WeaponDescriptor weaponDescriptor = _weapon.GetComponent<WeaponDescriptor>();
					CharacterDescriptor targetDescriptor = _target.Prefab.GetComponent<CharacterDescriptor>();
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
						_target.Prefab.Animator.SetTrigger("die");
					}
				}
				Destroy(gameObject);
			}
		}


	}
}