namespace PepegaAR.WeaponSystem.Weapon
{
    using NTC.Global.Pool;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public sealed class Bullet : MonoBehaviour, IPoolItem
    {
        public Rigidbody Rigidbody { get; private set; } = default;

        [SerializeField]
        private float _despawnTime = 5f;

        public bool IsInisialized { get; private set; }

        public int Damage { get; private set; }

        public void OnDespawn()
        {
        }

        public void OnSpawn()
        {
            NightPool.Despawn(gameObject, _despawnTime);
        }

        public void Init(int damage)
        {
            Rigidbody = GetComponent<Rigidbody>();

            Damage = damage;

            IsInisialized = true;
        }
    }
}