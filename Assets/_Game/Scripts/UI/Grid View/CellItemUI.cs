using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.GridView
{
    public class CellItemUI : MonoBehaviour
    {
        [SerializeField]
        private Color _grayColor;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private TextMeshProUGUI _charTMP;

        public void SetColor(bool isGray)
        {
            Color color = isGray ? _grayColor : Color.white;
            _background.color = color;
        }

        public void SetChar(char letter) =>
            _charTMP.text = letter.ToString();
    }
}