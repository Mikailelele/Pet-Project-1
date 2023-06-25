namespace PepegaAR.Core.Enemy
{
    using MessagePipe;
    using Interfaces;
    using System;
    using UnityEngine;
    using Vuforia;
    using Cysharp.Threading.Tasks;
    using TargetImageSetuper;
    using VContainer.Unity;

    public sealed class EnemyMovement : IStartable, IDisposable
    {
        private Rigidbody _enemyRigidbody = default;

        private Transform _enemyTransform = default;
        private Transform _playerTransform = default;

        private float _movingSpeed = 0.1f;
        private bool _isMoving;

        private readonly ISubscriber<OnPlayerDetectingStatusChangedMessage> _playerDetectedMessageSubscriber = default;
        private IDisposable _disposable = default;

        public EnemyMovement(ISubscriber<OnPlayerDetectingStatusChangedMessage> playerDetectedMessageSubscriber, 
            IEnemy enemy,
            IPlayer player)
        {
            _playerDetectedMessageSubscriber = playerDetectedMessageSubscriber;

            _enemyTransform = enemy.CachedTransform;
            _enemyRigidbody = enemy.Rigidbody;
            //_movingSpeed = enemy.Data.MovindSpeed;

            _playerTransform = player.CachedTransform;
        }

        public void Start()
        {
            SubscribeToPlayerDetectedMessage();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void StartMoving()
        {
            if (_isMoving) return;

            _isMoving = true;

            Move().Forget();
        }

        private void StopMoving()
        {
            _isMoving = false;
        }

        private async UniTaskVoid Move()
        {
            while (_isMoving)
            {
                Vector3 direction = (_playerTransform.position - _enemyTransform.position).normalized;
                Vector3 movement = direction * _movingSpeed * Time.fixedDeltaTime;
                _enemyRigidbody.MovePosition(_enemyTransform.position + movement);

                await UniTask.WaitForFixedUpdate();
            }
        }

        private void HandlePlayerDetect(OnPlayerDetectingStatusChangedMessage message)
        {
            switch (message.Status)
            {
                case Status.TRACKED:
                    StartMoving();
                    break;
                case Status.EXTENDED_TRACKED:
                    StopMoving();
                    break;
            }
        }

        private void SubscribeToPlayerDetectedMessage()
        {
            var bag = DisposableBag.CreateBuilder();

            _playerDetectedMessageSubscriber.Subscribe(HandlePlayerDetect).AddTo(bag);

            _disposable = bag.Build();
        }
    }
}