namespace PepegaAR.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class BaseButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public virtual void OnPointerDown(PointerEventData eventData) { }

        public virtual void OnPointerUp(PointerEventData eventData) { }
    }

}