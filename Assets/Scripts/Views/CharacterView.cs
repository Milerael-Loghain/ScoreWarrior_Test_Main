using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class CharacterView : MonoBehaviour
	{
		public WeaponView Weapon;
		public Animator Animator;

		[SerializeField]
		private Transform _rightPalm;

		public void Update()
		{
			if (_rightPalm != null && Weapon != null)
			{
				Weapon.transform.position = _rightPalm.position;
				Weapon.transform.forward = _rightPalm.up;
			}
		}
	}
}