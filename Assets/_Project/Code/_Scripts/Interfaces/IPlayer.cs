namespace PepegaAR.Interfaces
{
    using Data;

    public interface IPlayer : IEntity
    {
        PlayerData Data { get; } 
    }
}