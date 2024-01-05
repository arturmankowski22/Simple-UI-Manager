using SUIT.Config;
using UnityEngine;

namespace SUIT.Utils
{
    public static class Logger
    {
        public static void Log(string message, GameObject context = null)
        {
            if (SUITConfigProvider.GeneralConfig.EnableDebug)
                Debug.Log($"{Constants.SUITPrefix} {message}", context);
        }

        public static void LogWarning(string message, GameObject context = null)
        {
            if (SUITConfigProvider.GeneralConfig.EnableDebug)
                Debug.LogWarning($"{Constants.SUITPrefix} {message}", context);
        }

        public static void LogError(string message, GameObject context = null)
        {
            if (SUITConfigProvider.GeneralConfig.EnableDebug)
                Debug.LogError($"{Constants.SUITPrefix} {message}", context);
        }
    }
}