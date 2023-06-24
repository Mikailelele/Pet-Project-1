namespace PepegaAR.Data
{
    using WeaponSystem.Weapon;
    using UnityEngine;

    [System.Serializable]
    public struct WeaponData
    {
        [field: SerializeField]
        public Transform ShootPosition { get; private set; }

        [field: SerializeField]
        public int Damage { get; private set; }

        [field: SerializeField]
        public Bullet Bullet { get; private set; }

        [field: SerializeField]
        public float ShootDelay { get; private set; }

        [field: SerializeField]
        public Vector3 Force { get; private set; }
    }
}