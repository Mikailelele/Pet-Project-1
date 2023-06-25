namespace PepegaAR.Interfaces
{
    using Vuforia;

    public interface ITargetStatusChangedMessage
    {
        Status Status { get; }
    }
}