using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Crossword
{
    public class CellItemUI : MonoBehaviour
    {
        [SerializeField]
        private Color _grayColor;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private TextMeshProUGUI _charTMP;

        [SerializeField]
        private CellRotateAnimation _cellRotateAnimation;

        public bool HasRotated { get; private set; }

        public void SetColor(bool isGray)
        {
            Color color = isGray ? _grayColor : Color.white;
            _background.color = color;
        }

        public void SetChar(char letter) =>
            _charTMP.text = letter.ToString();

        public void HideChar() =>
            _charTMP.enabled = false;

        [ContextMenu("Start Rotate Animation")]
        public void StartRotateAnimation()
        {
            HasRotated = true;
            _cellRotateAnimation.StartAnimation();
        }

        [ContextMenu("Stop Rotate Animation")]
        public void StopRotateAnimation() =>
            _cellRotateAnimation.StopAnimation();
    }
}