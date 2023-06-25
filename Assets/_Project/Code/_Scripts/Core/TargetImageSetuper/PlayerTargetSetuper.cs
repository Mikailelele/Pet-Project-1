namespace PepegaAR.Core.TargetImageSetuper
{
    using Interfaces;
    using UnityEngine;
    using Utils;
    using Vuforia;

    public readonly struct OnPlayerDetectingStatusChangedMessage : ITargetStatusChangedMessage
    {
        public readonly Status Status { get; }

        public OnPlayerDetectingStatusChangedMessage(Status status)
        {
            Status = status;
        }
    }

    public sealed class PlayerTargetSetuper : BaseTargetSetuper<IPlayer, OnPlayerDetectingStatusChangedMessage>
    {
        protected override string TargetPath => Constants.Targets.PlayerTargetTexturePath;

        protected override void OnDetectStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
        {
            OnDetectingStatusChangedPublisher.Publish(new OnPlayerDetectingStatusChangedMessage(targetStatus.Status));
            ChildTransform.localPosition = Vector3.zero;
        }
    }
}