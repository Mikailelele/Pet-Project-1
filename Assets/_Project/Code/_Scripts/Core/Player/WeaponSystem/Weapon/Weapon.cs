namespace PepegaAR.Core.Player.WeaponSystem
{
    using Cysharp.Threading.Tasks;
    using NTC.Global.Pool;
    using Data;
    using Interfaces;
    using Sirenix.OdinInspector;
    using System;
    using UnityEngine;

    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private WeaponData _data = default;

        private bool _isShooting;

        [Button]
        public void StartShooting()
        {
            if (_isShooting) return;

            _isShooting = true;

            Shoot().Forget();
        }

        [Button]
        public void StopShooting()
        {
            _isShooting = false;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private async UniTaskVoid Shoot()
        {
            while(_isShooting)
            {
                var bullet = NightPool.Spawn(_data.Bullet, _data.ShootPosition.position, _data.ShootPosition.rotation);

                if (!bullet.IsInisialized)
                    bullet.Init(_data.Damage);

                bullet.Rigidbody.AddForce(_data.Force);

                await UniTask.Delay(TimeSpan.FromMilliseconds(_data.ShootDelay));
            }
        }
    }
}