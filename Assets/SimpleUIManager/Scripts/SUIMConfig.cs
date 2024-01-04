using SUIM.Utils;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SUIM
{
    public static class SUIMConfigProvider
    {
        private static SUIMConfig _config;

        public static SUIMConfig Config
        {
            get
            {
                if (_config != null)
                    return _config;

                _config = Resources.Load<SUIMConfig>(Constants.ConfigName);
                if (_config != null)
                    return _config;

                Debug.LogWarning("SUIMConfig not found! Creating a new one...");
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
    }

    public sealed class SUIMConfig : ScriptableObject
    {
        [SerializeField] private bool _enableDebug = true;
        [SerializeField] private bool _enableAutoRebuildViewsTree = true;
        public bool EnableDebug => _enableDebug;
        public bool EnableAutoRebuildViewsTree => _enableAutoRebuildViewsTree;
    }
}