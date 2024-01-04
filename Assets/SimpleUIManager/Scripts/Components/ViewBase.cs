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
        [SerializeField] [HideInInspector] private ViewBase _parentView;
        [SerializeField] [HideInInspector] private List<ViewBase> _childViews = new();

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        [Inject] protected SignalBus _signalBus;
        public bool UseShowAnimation => ViewAnimationModule is { ShowAnimBehaviour: { UseAnimation: true } };
        public bool UseHideAnimation => ViewAnimationModule is { HideAnimBehaviour: { UseAnimation: true } };
        public virtual ViewAnimationModule ViewAnimationModule { get; }

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

        public ViewBase ParentView => _parentView;
        public List<ViewBase> ChildViews => _childViews;

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