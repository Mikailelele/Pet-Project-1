namespace PepegaAR.Player.PlayerManagers
{
    using Cysharp.Threading.Tasks;
    using Interfaces;
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using VContainer;
    using VContainer.Unity;
    using Vuforia;
    using static Utils.Constants;

    public sealed class PlayerSetuper : IStartable, IDisposable
    {
        private Transform _playerTransform = default;

        [Inject]
        private void Construct(IPlayer player)
        {
            _playerTransform = player.CachedTransform;
        }

        public void Start()
        {
            VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
        }

        public void Dispose()
        {
            VuforiaApplication.Instance.OnVuforiaInitialized -= OnVuforiaInitialized;
        }

        private void OnVuforiaInitialized(VuforiaInitError error)
        {
            if (error == VuforiaInitError.NONE)
                CreatePlayerTargetAsync().Forget();
        }

        private async UniTaskVoid CreatePlayerTargetAsync()
        {
            Texture2D targetTexture = await Addressables.LoadAssetAsync<Texture2D>($"{Targets.TargetTexturePath}/{Targets.PlayerTargetName}");

            var target = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            targetTexture,
            1,
            "Player");

            _playerTransform.SetParent(target.transform);

        }
    }
}