using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Models.Characters;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models
{
    public class BulletModel
    {
        private readonly CharacterModel _target;
        private readonly WeaponDescriptor _weaponDescriptor;
        private readonly BulletView _bulletView;
        private readonly bool _hit;

        private bool _isDestroyed;

        public bool IsDestroyed => _isDestroyed;

        public BulletModel(WeaponDescriptor weaponDescriptor, BulletView bulletViewPrefab, Vector3 bulletSpawnPosition, CharacterModel target, bool hit)
        {
            _weaponDescriptor = weaponDescriptor;
            _target = target;
            _hit = hit;

            _bulletView = Object.Instantiate(bulletViewPrefab);
            _bulletView.transform.position = bulletSpawnPosition;

            _isDestroyed = false;
        }

        public void Update()
        {
            Vector3 targetPosition = _target.Position + Vector3.up * 2.0f;
            Vector3 direction = Vector3.Normalize(targetPosition - _bulletView.transform.position);
            _bulletView.transform.position += direction / 30;

            float distance = Vector3.Distance(targetPosition, _bulletView.transform.position);
            if (distance < 0.01)
            {
                if (_hit)
                {
                    float damage = _weaponDescriptor.Damage;
                    _target.HandleDamage(damage);
                }

                Object.Destroy(_bulletView.gameObject);
                _isDestroyed = true;
            }
        }
    }
}