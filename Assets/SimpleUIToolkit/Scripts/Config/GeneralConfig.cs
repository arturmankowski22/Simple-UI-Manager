using System;
using UnityEngine;

namespace SUIT.Config
{
    [Serializable]
    public sealed class GeneralConfig
    {
        public bool EnableDebug => _enableDebug;
        public bool EnableAutoRebuildViewsTree => _enableAutoRebuildViewsTree;
        
        [SerializeField] private bool _enableDebug = true;
        [SerializeField] private bool _enableAutoRebuildViewsTree = true;
    }
}