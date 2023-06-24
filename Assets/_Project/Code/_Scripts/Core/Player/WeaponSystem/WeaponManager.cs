namespace PepegaAR.Core.Player.WeaponSystem
{
    using MessagePipe;
    using Interfaces;
    using System;

    public sealed class WeaponManager : IWeaponManager, IDisposable
    {
        public IWeapon CurrentWeapon { get; private set; }

        private readonly ISubscriber<OnWeaponSwitchedMessage> _weaponSwitchedSubscriber;
        private IDisposable _disposable = default;

        public WeaponManager(ISubscriber<OnWeaponSwitchedMessage> weaponSwitchedSubscriber)
        {
            _weaponSwitchedSubscriber = weaponSwitchedSubscriber;

            SubscribeToWeaponSwitchedMessage();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnWeaponSwitched(OnWeaponSwitchedMessage message)
        {
            CurrentWeapon = message.Weapon;
        }

        private void SubscribeToWeaponSwitchedMessage()
        {
            var bag = DisposableBag.CreateBuilder();

            _weaponSwitchedSubscriber.Subscribe(OnWeaponSwitched).AddTo(bag);

            _disposable = bag.Build();
        }
    }
}