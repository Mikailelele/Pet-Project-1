namespace PepegaAR.Core.Player.WeaponSystem
{
    using Cysharp.Threading.Tasks;
    using MessagePipe;
    using Interfaces;
    using System;
    using VContainer.Unity;

    public enum EShootAction
    {
        Started,
        Stopped,
    }

    public readonly struct ShootActionMessage : IEquatable<ShootActionMessage>
    {
        public readonly EShootAction Action;

        public ShootActionMessage(EShootAction action)
        {
            Action = action;
        }

        public bool Equals(ShootActionMessage other)
        {
            return Action == other.Action;
        }

        public override bool Equals(object obj)
        {
            return obj is ShootActionMessage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)Action;
        }
    }

    public sealed class ShootController : IShootController, IStartable, IDisposable
    {
        private IWeapon CurrentWeapon => _weaponManager.CurrentWeapon;

        private readonly IWeaponManager _weaponManager = default;

        private readonly ISubscriber<ShootActionMessage> _shootSubscriber = default;
        private IDisposable _disposable = default;

        public ShootController(ISubscriber<ShootActionMessage> shootSubscriber, IWeaponManager weaponSwitcher)
        {
            _shootSubscriber = shootSubscriber;

            _weaponManager = weaponSwitcher;
        }

        public void Start()
        {
            SubscribeToMessages();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnShoot(ShootActionMessage message)
        {
            switch(message.Action)
            {
                case EShootAction.Started:
                    CurrentWeapon.StartShooting();
                    break;
                case EShootAction.Stopped:
                    CurrentWeapon.StopShooting();
                    break;
            }
        }

        private void SubscribeToMessages()
        {
            var bag = DisposableBag.CreateBuilder();

            _shootSubscriber.Subscribe(OnShoot).AddTo(bag);

            _disposable = bag.Build();
        }
    }
}
