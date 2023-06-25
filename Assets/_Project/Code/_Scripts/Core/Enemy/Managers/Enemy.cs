namespace PepegaAR.Core.Enemy
{
    using Data;
    using Interfaces;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public Rigidbody Rigidbody { get; private set; }
        public Transform CachedTransform { get; private set; }

        [field: SerializeField]
        public EnemyData Data { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            CachedTransform = transform;
        }
    }
}