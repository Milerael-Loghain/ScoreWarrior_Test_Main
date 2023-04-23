using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class WeaponView : MonoBehaviour
	{
		public Transform BarrelTransform;

		public BulletView BulletView => bulletView;

		[SerializeField]
		private BulletView bulletView;
	}
}