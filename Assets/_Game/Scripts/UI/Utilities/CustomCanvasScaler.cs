using Core.Utilities;
using UnityEngine;
using UnityEngine.UI;
using DeviceType = Core.Enums.DeviceType;

namespace Core.UI.Utilities
{
    public class CustomCanvasScaler : MonoBehaviour
    {
        [SerializeField]
        private bool _matchWidthOnPhone;

        private void Start() => UpdateCanvas();

        private void UpdateCanvas()
        {
            int phoneValue = _matchWidthOnPhone ? 0 : 1;
            int tabletValue = _matchWidthOnPhone ? 1 : 0;

            float matchWidth = DeviceTypeChecker.GetDeviceType() == DeviceType.Phone
                ? phoneValue
                : tabletValue;
            
            if (TryGetComponent(out CanvasScaler canvasScaler))
                canvasScaler.matchWidthOrHeight = matchWidth;
        }
    }
}