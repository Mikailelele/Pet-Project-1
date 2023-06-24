namespace PepegaAR.Interfaces
{
    using UnityEngine;

    public interface IEntity 
    {
        Transform CachedTransform { get; }
    }
}