namespace PepegaAR.Core.Player.WeaponSystem
{
    using MessagePipe;
    using Interfaces;
    using Utils;
    using System;
    using UnityEngine;
    using VContainer;

    public readonly struct SwitchWeaponMessage { }
    public readonly struct OnWeaponSwitchedMessage : IEquatable<OnWeaponSwitchedMessage>
    {
        public readonly IWeapon Weapon;

        public OnWeaponSwitchedMessage(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public bool Equals(OnWeaponSwitchedMessage other)
        {
            return Weapon == other.Weapon;
        }

        public override bool Equals(object obj)
        {
            return obj is OnWeaponSwitchedMessage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Weapon.GetHashCode();
        }
    }

    public sealed class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons = default;

        private IWeapon currentWeapon = default;

        private ISubscriber<SwitchWeaponMessage> _switchWeaponEventSubscriber = default;
        private IPublisher<OnWeaponSwitchedMessage> _onWeaponSwitchedPublisher = default;

        private IDisposable _disposable = default;

        [Inject]
        private void Construct(ISubscriber<SwitchWeaponMessage> switchWeaponEventSubscriber, 
            IPublisher<OnWeaponSwitchedMessage> onWeapoSwitchedPublisher)
        {
            _switchWeaponEventSubscriber = switchWeaponEventSubscriber;
            _onWeaponSwitchedPublisher = onWeapoSwitchedPublisher;

            SubsribeToSwitchWeaponEvent();
        }

        private void Start()
        {
            ActivateFirstWeapon();
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void SwitchWeapon(SwitchWeaponMessage switchWeaponEvent)
        {
            SwitchToNextWeapon();
            PublishNewWeapon();
        }

        private void ActivateFirstWeapon()
        {
            if (_weapons.Length == 0)
                throw new IndexOutOfRangeException();

            _weapons[0].SetActive(true);

            currentWeapon = _weapons[0];

            PublishNewWeapon();
        }

        private void SubsribeToSwitchWeaponEvent()
        {
            var bag = DisposableBag.CreateBuilder();

            _switchWeaponEventSubscriber.Subscribe(SwitchWeapon).AddTo(bag);

            _disposable = bag.Build();
        }

        private void SwitchToNextWeapon()
        {
            var nextWeapon = _weapons.GetNextOrFirst(currentWeapon);

            if (nextWeapon == currentWeapon)
                return;

            currentWeapon.SetActive(false);
            nextWeapon.SetActive(true);

            currentWeapon = nextWeapon;
        }

        private void PublishNewWeapon()
        {
            _onWeaponSwitchedPublisher.Publish(new OnWeaponSwitchedMessage(currentWeapon));
        }
    }
}