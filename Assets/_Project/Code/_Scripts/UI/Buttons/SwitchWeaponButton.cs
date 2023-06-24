namespace PepegaAR.UI
{
    using MessagePipe;
    using Core.Player.WeaponSystem;
    using VContainer;
    using UnityEngine.EventSystems;

    public sealed class SwitchWeaponButton : BaseButtonController
    {
        private IPublisher<SwitchWeaponMessage> _switchWeaponEventPublisher = default;
        private SwitchWeaponMessage _switchWeaponEvent = new SwitchWeaponMessage();

        [Inject]
        private void Construct(IPublisher<SwitchWeaponMessage> switchWeaponEventPublisher)
        {
            _switchWeaponEventPublisher = switchWeaponEventPublisher;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _switchWeaponEventPublisher.Publish(_switchWeaponEvent);
        }
    }
}   