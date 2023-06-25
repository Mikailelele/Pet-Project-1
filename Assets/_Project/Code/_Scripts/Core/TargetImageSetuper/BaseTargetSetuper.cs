namespace PepegaAR.Core.TargetImageSetuper
{
    using Cysharp.Threading.Tasks;
    using System;
    using UnityEngine.AddressableAssets;
    using UnityEngine;
    using VContainer.Unity;
    using Vuforia;
    using Interfaces;
    using VContainer;
    using MessagePipe;

    public abstract class BaseTargetSetuper<T, U> : IStartable, IDisposable
        where T : IEntity 
        where U : ITargetStatusChangedMessage
    {
        protected Transform ChildTransform = default;

        protected abstract string TargetPath { get; }

        protected ImageTargetBehaviour ImageTarget = default;

        protected IPublisher<U> OnDetectingStatusChangedPublisher = default;

        [Inject]
        private void Construct(IPublisher<U> onDetectingStatusChangedPublisher, T entity)
        {
            OnDetectingStatusChangedPublisher = onDetectingStatusChangedPublisher;

            ChildTransform = entity.CachedTransform;
        }

        public void Start()
        {
            VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
        }

        public void Dispose()
        {
            ImageTarget.OnTargetStatusChanged -= OnDetectStatusChanged;

            VuforiaApplication.Instance.OnVuforiaInitialized -= OnVuforiaInitialized;
        }

        private void OnVuforiaInitialized(VuforiaInitError error)
        {
            if (error == VuforiaInitError.NONE)
                CreateTargetAsync().Forget();
        }

        private async UniTaskVoid CreateTargetAsync()
        {
            Texture2D targetTexture = await Addressables.LoadAssetAsync<Texture2D>(TargetPath);

            ImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            targetTexture,
            1,
            "ImageTarget");

            ChildTransform.SetParent(ImageTarget.transform);

            SubscribeToTargetEvents();
            OnTargetCreated();
        }

        private void SubscribeToTargetEvents()
        {
            ImageTarget.OnTargetStatusChanged += OnDetectStatusChanged;
        }

        protected virtual void OnDetectStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus) { }
        protected virtual void OnTargetCreated() { }
    }
}