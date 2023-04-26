using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class ButtonAnimationUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private bool _checkButtonInteractable;

        [SerializeField, Min(0)]
        private float _scaleTime = 0.15f;

        [SerializeField]
        private Vector2 _scale = new(0.85f, 0.85f);
        
        [SerializeField]
        private RectTransform _rectTransform;
        
        [SerializeField]
        private Button _button;
        
        private Tweener _scaleTN;
        private Vector3 _startScale;
        private Vector3 _finalScale;
        private bool _isEventThrowing;
        
        private void Awake() =>
            _startScale = _rectTransform.localScale;

        private void OnDestroy() =>
            _scaleTN.Kill();

        private void ScaleDown()
        {
            _scaleTN.Complete();
            _finalScale = _startScale * _scale;
            _finalScale.z = _finalScale.x;
            _scaleTN = _rectTransform.DOScale(_finalScale, _scaleTime).SetUpdate(true);
        }

        private void ScaleUp()
        {
            _scaleTN.Complete(); 
            _scaleTN = _rectTransform.DOScale(_startScale, _scaleTime).SetUpdate(true);
        }

        private bool CanInteract()
        {
            bool canInteract = !_checkButtonInteractable || _button.interactable;
            return canInteract;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!CanInteract())
                return;
            
            ScaleDown();
        }

        public void OnPointerUp(PointerEventData eventData) => ScaleUp();
    }
}
