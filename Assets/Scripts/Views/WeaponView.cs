using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class WeaponView : MonoBehaviour
	{
		public Transform BarrelTransform;

		public BulletView BulletPrefab => bulletPrefab;

		[SerializeField]
		private BulletView bulletPrefab;
	}
}