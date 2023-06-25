namespace PepegaAR.Interfaces
{
    using Data;
    using UnityEngine;

    public interface IEnemy : IEntity
    {
        EnemyData Data { get; }

        Rigidbody Rigidbody { get; }
    }
}