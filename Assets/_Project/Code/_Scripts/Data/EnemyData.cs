namespace PepegaAR.Data
{
    using UnityEngine;

    [System.Serializable]
    public struct EnemyData 
    {
        [field: SerializeField]
        public float Health { get; private set; }

        [field: SerializeField]
        public int MovindSpeed { get; private set; }

        [field: SerializeField]
        public float BaseDamage { get; private set; }

        public EnemyData(float health, int movementSpeed, float baseDamage)
        {
            Health = health;
            MovindSpeed = movementSpeed;
            BaseDamage = baseDamage;
        }
    }
}