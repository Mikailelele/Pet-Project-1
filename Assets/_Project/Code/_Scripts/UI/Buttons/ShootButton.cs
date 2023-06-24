namespace PepegaAR.UI
{
    using MessagePipe;
    using Core.Player.WeaponSystem;
    using UnityEngine.EventSystems;
    using VContainer;

    public sealed class ShootButton : BaseButtonController
    {
        private IPublisher<ShootActionMessage> _shootActionPublisher = default;
        private ShootActionMessage _startShootMessage = new ShootActionMessage(EShootAction.Started);
        private ShootActionMessage _stopShootMessage = new ShootActionMessage(EShootAction.Stopped);

        [Inject]
        private void Construct(IPublisher<ShootActionMessage> ShootPublisher)
        {
            _shootActionPublisher = ShootPublisher;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _shootActionPublisher.Publish(_startShootMessage);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _shootActionPublisher.Publish(_stopShootMessage);
        }
    }
}