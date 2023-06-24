namespace PepegaAR.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using VContainer;
    using VContainer.Unity;

    public sealed class UISetuper : IStartable
    {
        private List<Canvas> _canvases = new List<Canvas>();
        private Camera _cachedCamera = default;

        [Inject]
        private void Construct(WeaponCanvas weaponCanvas)
        {
            _canvases.Add(weaponCanvas.GetComponent<Canvas>());
        }

        public void Start()
        {
            _cachedCamera = Camera.main;

            Setup();
        }

        private void Setup()
        {
            foreach(Canvas canvas in _canvases)
            {
                canvas.worldCamera = _cachedCamera;
            }
        }
    }
}