using System;
using DG.Tweening;
using UnityEngine;

namespace Core.UI.Crossword
{
    [Serializable]
    public class CrosswordViewShakeAnimation
    {
        [SerializeField, Min(0)]
        private float _duration;

        [SerializeField]
        private Vector3 _strength;

        [SerializeField, Range(1, 10)]
        private int _vibrato = 10;

        [SerializeField]
        private RectTransform _targetRT;
        
        private Tweener _shakeTN;
        
        public void StartAnimation()
        {
            _shakeTN.Complete();
            _shakeTN = _targetRT.DOShakePosition(_duration, _strength, _vibrato);
        }
    }
}