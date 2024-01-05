using System;
using System.Collections.Generic;
using SUIM.Animation;
using UnityEngine;
using Zenject;

namespace SUIM.Components.Views
{
    [Serializable]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ViewBase : MonoBehaviour
    {
        public bool UseShowAnimation => ViewAnimationModule is { ShowAnimBehaviour: { UseAnimation: true } };
        public bool UseHideAnimation => ViewAnimationModule is { HideAnimBehaviour: { UseAnimation: true } };
        /// <summary>
        /// Module responsible for animations. By default, no animation is configured (interpreted by the system as instant animation)
        /// </summary>
        public virtual ViewAnimationModule ViewAnimationModule { get; }
        public ViewBase ParentView => _parentView;
        public List<ViewBase> ChildViews => _childViews;
        public bool IncludeInViewsHistory => _includeInViewsHistory;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }
        
        [SerializeField] private bool _includeInViewsHistory = true;
        [SerializeField, HideInInspector] private ViewBase _parentView;
        [SerializeField, HideInInspector] private List<ViewBase> _childViews = new();

        [Inject] protected SignalBus _signalBus;
        
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        #region Editor Methods

#if UNITY_EDITOR
        public void SetupParentViewFromEditor(ViewBase parentView)
        {
            _parentView = parentView;
            if (parentView != null)
                parentView.ChildViews.Add(this);
        }
#endif

        #endregion

        public virtual void Init()
        {
        }

        public virtual void OnShowStarted()
        {
        }

        public virtual void OnShowFinished()
        {
        }

        public virtual void OnHideStarted()
        {
        }

        public virtual void OnHideFinished()
        {
        }

        public void ResetView()
        {
            CanvasGroup.alpha = 1;
            var cachedTransform = transform;
            cachedTransform.localScale = Vector3.one;
            cachedTransform.rotation = Quaternion.identity;
        }
    }
}