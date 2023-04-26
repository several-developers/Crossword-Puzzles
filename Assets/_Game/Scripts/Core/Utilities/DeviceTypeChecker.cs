using UnityEngine;
using DeviceType = Core.Enums.DeviceType;

namespace Core.Utilities
{
    public static class DeviceTypeChecker
    {
        public static bool isTablet;

        static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }

        public static DeviceType GetDeviceType()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            bool isTablet;
            
#if UNITY_IOS
            float aspectRatio = Mathf.Max(screenWidth, screenHeight) / Mathf.Min(screenWidth, screenHeight);
            isTablet = DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f;

            return isTablet ? DeviceType.Tablet : DeviceType.Phone;
#elif UNITY_ANDROID

            float aspectRatio = Mathf.Max(screenWidth, screenHeight) / Mathf.Min(screenWidth, screenHeight);
            isTablet = DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f;

            return isTablet ? DeviceType.Tablet : DeviceType.Phone;
#endif
#pragma warning disable 0162
            return DeviceType.Phone;
#pragma warning restore 0162
        }
    }
}