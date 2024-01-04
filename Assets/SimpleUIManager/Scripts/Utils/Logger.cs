using UnityEngine;

namespace SUIM.Utils
{
    public static class Logger
    {
        public static void Log(string message, GameObject context = null)
        {
            if (SUIMConfigProvider.Config.EnableDebug)
                Debug.Log($"{Constants.SUIMPrefix} {message}", context);
        }

        public static void LogWarning(string message, GameObject context = null)
        {
            if (SUIMConfigProvider.Config.EnableDebug)
                Debug.LogWarning($"{Constants.SUIMPrefix} {message}", context);
        }

        public static void LogError(string message, GameObject context = null)
        {
            if (SUIMConfigProvider.Config.EnableDebug)
                Debug.LogError($"{Constants.SUIMPrefix} {message}", context);
        }
    }
}