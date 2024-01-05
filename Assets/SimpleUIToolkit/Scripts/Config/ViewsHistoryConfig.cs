using System;
using UnityEngine;

namespace SUIT.Config
{
    [Serializable]
    public sealed class ViewsHistoryConfig
    {
        public bool EnableViewsHistory => _enableViewsHistory;
        public int ViewsHistoryCapacity => _viewsHistoryCapacity;
        
        [SerializeField] private bool _enableViewsHistory = true;
        [SerializeField] private int _viewsHistoryCapacity = 10;
    }
}