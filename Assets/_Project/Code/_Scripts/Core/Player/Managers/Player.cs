namespace PepegaAR.Core.Player
{
    using Data;
    using Interfaces;
    using UnityEngine;

    public class Player : MonoBehaviour, IPlayer
    {
        [field: SerializeField]
        public PlayerData Data { get; private set; }

        public Transform CachedTransform { get; private set; }

        private void Awake()
        {
            CachedTransform = transform;
        }
    }
}