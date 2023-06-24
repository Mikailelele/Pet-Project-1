namespace PepegaAR.LifetimeScope
{
    using VContainer;
    using VContainer.Unity;
    using MessagePipe;
    using Core.Player.WeaponSystem;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using Core.Player;
    using Player.PlayerManagers;
    using Interfaces;
    using UI;

    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        [FoldoutGroup("Player")]
        private Player _player;

        [SerializeField]
        [FoldoutGroup("UI")]
        private WeaponCanvas _weaponCanvas;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            ConfigureMessagePipe();

            SpawnObjects();

            RegisterEntryPoints();
        }

        private void SpawnObjects()
        {
            SpawnPlayer();
            SpawnUi();
        }

        private void RegisterEntryPoints()
        {
            _builder.UseEntryPoints(Lifetime.Singleton, entryPoint =>
            {
                entryPoint.Add<PlayerSetuper>();
                entryPoint.Add<UISetuper>();
                entryPoint.Add<WeaponManager>();
                entryPoint.Add<ShootController>();
            }); 
        }

        private void ConfigureMessagePipe()
        {
            var options = _builder.RegisterMessagePipe();

            _builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            RegisterMessageBrokers(options);
        }

        private void RegisterMessageBrokers(MessagePipeOptions options)
        {
            _builder.RegisterMessageBroker<ShootActionMessage>(options);
            _builder.RegisterMessageBroker<OnWeaponSwitchedMessage>(options);
            _builder.RegisterMessageBroker<SwitchWeaponMessage>(options);
        }

        private void SpawnPlayer()
        {
            var playerInstance = Instantiate(_player);
            _builder.RegisterInstance(playerInstance).As<IPlayer>();

            _builder.RegisterBuildCallback(container => {
                container.InjectGameObject(playerInstance.gameObject);
            });
        }

        private void SpawnUi()
        {
            WeaponCanvas weaponCanvasInstance = Instantiate(_weaponCanvas);
            _builder.RegisterInstance(weaponCanvasInstance);

            _builder.RegisterBuildCallback(container => {
                container.InjectGameObject(weaponCanvasInstance.gameObject);
            });
        }
    }
}
