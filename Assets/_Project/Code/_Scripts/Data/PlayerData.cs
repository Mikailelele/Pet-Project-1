namespace PepegaAR.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct PlayerData
    {
        [field: SerializeField]
        public float Health { get; set; }

        public PlayerData(in float health)
        {
            Health = health;
        }
    }
}