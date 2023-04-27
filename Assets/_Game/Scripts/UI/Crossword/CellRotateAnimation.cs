using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.UI.Crossword
{
    [Serializable]
    public class CellRotateAnimation
    {
        [SerializeField]
        private Vector3 _rotateValue;
        
        [SerializeField, Min(0)]
        private float _duration;

        [SerializeField, Min(0)]
        private float _showCharDelay;

        [SerializeField]
        private Ease _ease;

        [SerializeField]
        private RotateMode _rotateMode;

        [SerializeField]
        private RectTransform _targetRT;
        
        [SerializeField]
        private RectTransform _charRT;

        [SerializeField]
        private TextMeshProUGUI _charTMP;

        private Tweener _rotationTN;
        private bool _showChar;
        
        public void StartAnimation()
        {
            Rotate();
            ChangeCharState();
        }

        public void StopAnimation()
        {
            _rotationTN.Kill();
            _targetRT.DORotate(Vector3.zero, 0);
            _charRT.localScale = Vector3.one;
        }

        private void Rotate()
        {
            _rotationTN.Complete();
            _rotationTN = _targetRT
                .DORotate(_rotateValue, _duration, _rotateMode)
                .SetEase(_ease);
        }

        private async void ChangeCharState()
        {
            _showChar = !_showChar;
            int delay = (int)(_showCharDelay * 1000);
            
            await Task.Delay(delay);
            
            Vector3 scale = Vector3.one;
            scale.x = _showChar ? -1 : 1;
            _charRT.localScale = scale;
            _charTMP.enabled = _showChar;
        }
    }
}