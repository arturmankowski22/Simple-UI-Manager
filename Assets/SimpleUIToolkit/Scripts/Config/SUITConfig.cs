using SUIT.Utils;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SUIT.Config
{
    public static class SUITConfigProvider
    {
        public static SUITConfig Config
        {
            get
            {
                if (_config != null)
                    return _config;

                _config = Resources.Load<SUITConfig>(Constants.ConfigName);
                if (_config != null)
                    return _config;

                Debug.LogWarning($"{Constants.SUITPrefix} {Constants.ConfigName} not found! Creating a new one in the \"Assets/Resources\" path...");
                _config = ScriptableObject.CreateInstance<SUITConfig>();
#if UNITY_EDITOR
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                AssetDatabase.CreateAsset(_config, Constants.ConfigAssetPath);
                AssetDatabase.SaveAssets();
#endif
                return _config;
            }
        }
        private static SUITConfig _config;

        public static ViewsHistoryConfig ViewsHistoryConfig => Config.ViewsHistoryConfig;
        public static GeneralConfig GeneralConfig => Config.GeneralConfig;
    }

    public sealed class SUITConfig : ScriptableObject
    {
        public GeneralConfig GeneralConfig => _generalConfig;
        public ViewsHistoryConfig ViewsHistoryConfig => _viewsHistoryConfig;
        
        [SerializeField] private GeneralConfig _generalConfig;
        [SerializeField] private ViewsHistoryConfig _viewsHistoryConfig;
    }
}