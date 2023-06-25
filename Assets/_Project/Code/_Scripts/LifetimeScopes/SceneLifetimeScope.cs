namespace PepegaAR.LifetimeScope
{
    using VContainer;
    using VContainer.Unity;
    using MessagePipe;
    using Core.Player.WeaponSystem;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using Core.Player;
    using Interfaces;
    using UI;
    using Core.TargetImageSetuper;
    using Core.Enemy;

    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [FoldoutGroup("Targets")]
        [SerializeField] private Player _player;

        [FoldoutGroup("Targets")]
        [SerializeField] private Enemy _enemy;

        [FoldoutGroup("UI")]
        [SerializeField] private WeaponCanvas _weaponCanvas;

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
            SpawnTargets();
            SpawnUi();
        }

        private void RegisterEntryPoints()
        {
            _builder.UseEntryPoints(Lifetime.Singleton, entryPoint =>
            {
                entryPoint.Add<PlayerTargetSetuper>();
                entryPoint.Add<EnemyTargetSetuper>();
                entryPoint.Add<EnemyMovement>();
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
            _builder.RegisterMessageBroker<OnPlayerDetectingStatusChangedMessage>(options);
            _builder.RegisterMessageBroker<OnEnemyDetectingStatusChangedMessage>(options);
        }

        private void SpawnTargets()
        {
            var playerInstance = Instantiate(_player);
            _builder.RegisterInstance(playerInstance).As<IPlayer>();

            var enemyInstance = Instantiate(_enemy);
            _builder.RegisterInstance(enemyInstance).As<IEnemy>();

            _builder.RegisterBuildCallback(container => {
                container.InjectGameObject(playerInstance.gameObject);
                container.InjectGameObject(enemyInstance.gameObject);
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
