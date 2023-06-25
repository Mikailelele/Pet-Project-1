namespace PepegaAR.Core.TargetImageSetuper
{
    using Interfaces;
    using UnityEngine;
    using Utils;
    using Vuforia;

    public readonly struct OnEnemyDetectingStatusChangedMessage : ITargetStatusChangedMessage
    {
        public readonly Status Status { get; }

        public OnEnemyDetectingStatusChangedMessage(Status status)
        {
            Status = status;
        }
    }

    public sealed class EnemyTargetSetuper : BaseTargetSetuper<IEnemy, OnEnemyDetectingStatusChangedMessage>
    {
        protected override string TargetPath => Constants.Targets.EnemyTargetTexturePath;

        protected override void OnDetectStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
        {
            OnDetectingStatusChangedPublisher.Publish(new OnEnemyDetectingStatusChangedMessage(targetStatus.Status));
            ChildTransform.localPosition = Vector3.zero;
        }
    }
}