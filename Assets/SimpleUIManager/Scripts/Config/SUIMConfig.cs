using SUIM.Utils;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SUIM.Config
{
    public static class SUIMConfigProvider
    {
        public static SUIMConfig Config
        {
            get
            {
                if (_config != null)
                    return _config;

                _config = Resources.Load<SUIMConfig>(Constants.ConfigName);
                if (_config != null)
                    return _config;

                Debug.LogWarning("SUIMConfig not found! Creating a new one in the \"Assets/Resources\" path...");
                _config = ScriptableObject.CreateInstance<SUIMConfig>();
#if UNITY_EDITOR
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                AssetDatabase.CreateAsset(_config, Constants.ConfigAssetPath);
                AssetDatabase.SaveAssets();
#endif
                return _config;
            }
        }
        private static SUIMConfig _config;

        public static ViewsHistoryConfig ViewsHistoryConfig => Config.ViewsHistoryConfig;
        public static GeneralConfig GeneralConfig => Config.GeneralConfig;
    }

    public sealed class SUIMConfig : ScriptableObject
    {
        public GeneralConfig GeneralConfig => _generalConfig;
        public ViewsHistoryConfig ViewsHistoryConfig => _viewsHistoryConfig;
        
        [SerializeField] private GeneralConfig _generalConfig;
        [SerializeField] private ViewsHistoryConfig _viewsHistoryConfig;
    }
}