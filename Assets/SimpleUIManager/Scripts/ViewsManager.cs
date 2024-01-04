using System;
using System.Collections.Generic;
using System.Linq;
using SUIM.Animation;
using SUIM.Components.Views;
using SUIM.Signals;
using SUIM.Utils;
using UnityEngine;
using Zenject;
using Logger = SUIM.Utils.Logger;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SUIM
{
    namespace Signals
    {
        public sealed class OnChangeView
        {
            public Type ViewToShow;
        }

        public sealed class OnShowViewAdditively
        {
            public Type ViewToShow;
        }

        public sealed class OnHideCurrentView
        {
        }

        public sealed class OnHideLastAdditiveView
        {
        }

        public sealed class OnHideGivenAdditiveView
        {
            public Type ViewToHide;
        }

        public sealed class OnHideAllAdditiveViews
        {
        }
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(RootView))]
    public sealed class ViewsManager : MonoBehaviour
    {
        [SerializeField] private ViewBase _startView;
        [SerializeField] [HideInInspector] private LCATree _lcaTree;
        private readonly List<ViewBase> _currentAdditiveViews = new();

        private ViewBase _currentView;
        private ViewBase _previousView;
        private SignalBus _signalBus;

        [Inject] private IViewAnimationInvoker _viewAnimationInvoker;

        private void Awake()
        {
            if (Application.isEditor && !Application.isPlaying)
                return;

            foreach (var view in _lcaTree.AvailableViews)
            {
                if (view.TryGetComponent<RootView>(out _))
                    continue;

                var rect = view.GetComponent<RectTransform>();
                rect.offsetMin = rect.offsetMax = Vector2.zero;
                view.CanvasGroup.blocksRaycasts = true;
                view.Init();
                view.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            if ((Application.isEditor && !Application.isPlaying) || _lcaTree.AvailableViews.Count == 0)
                return;

            _currentView = GetView(_startView.GetType());
            ShowView(_currentView);
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged += BuildViewsTreeAutomatically;
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged -= BuildViewsTreeAutomatically;
#endif
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            InitEvents();
        }

        private void InitEvents()
        {
            _signalBus.Subscribe<OnChangeView>(HandleChangeView);
            _signalBus.Subscribe<OnShowViewAdditively>(HandleShowViewAdditively);
            _signalBus.Subscribe<OnHideCurrentView>(HandleHideCurrentView);
            _signalBus.Subscribe<OnHideLastAdditiveView>(HandleHideLastAddictiveView);
            _signalBus.Subscribe<OnHideGivenAdditiveView>(HandleHideGivenAdditiveView);
            _signalBus.Subscribe<OnHideAllAdditiveViews>(HandleHideAllAdditiveViews);
        }

        private void HandleChangeView(OnChangeView data)
        {
            var viewToShow = GetView(data.ViewToShow);
            if (viewToShow == _currentView)
                return;

            _previousView = _currentView;
            _currentView = Helpers.GetDeepestChild(viewToShow);

            if (_currentView == _previousView)
                return;

            ShowView(_currentView);
            HideView(_previousView);
        }

        private void HandleShowViewAdditively(OnShowViewAdditively data)
        {
            var viewToShow = GetView(data.ViewToShow);
            if (viewToShow == null)
            {
                Logger.LogWarning($"Couldn't find a view=[{data.ViewToShow.Name}] type.");
                return;
            }

            ShowAdditiveView(viewToShow);
        }

        private void HandleHideCurrentView()
        {
            HideView(_currentView);
        }

        private void HandleHideLastAddictiveView()
        {
            HideLastAdditiveView();
        }

        private void HandleHideGivenAdditiveView(OnHideGivenAdditiveView data)
        {
            var viewToHide = GetView(data.ViewToHide);
            if (viewToHide == null)
            {
                Logger.LogWarning($"Couldn't find a view=[{data.ViewToHide.Name}] type.");
                return;
            }

            HideGivenAdditiveView(viewToHide);
        }

        private void HandleHideAllAdditiveViews()
        {
            HideAllAdditiveViews();
        }

        private void ShowView(ViewBase view)
        {
            if (view == null)
            {
                Logger.LogError("View to show can't be null!");
                return;
            }

            view.CanvasGroup.blocksRaycasts = true;
            view.OnShowStarted();

            var currentView = view;
            var commonParent = _lcaTree.FindCommonParent(_previousView, _currentView);

            if (commonParent != null)
                while (currentView.ParentView != commonParent || currentView.ParentView == null)
                {
                    currentView.RectTransform.anchoredPosition = Vector2.zero;
                    currentView = currentView.ParentView;
                    currentView.gameObject.SetActive(true);
                }

            view.ResetView();
            view.gameObject.SetActive(true);
            if (!currentView.UseShowAnimation)
            {
                view.OnShowFinished();
                return;
            }

            _viewAnimationInvoker.Show(currentView, view.OnShowFinished);
        }

        private void HideView(ViewBase view)
        {
            if (view == null)
            {
                Logger.LogError("View to hide can't be null!");
                return;
            }

            view.CanvasGroup.blocksRaycasts = false;
            view.OnHideStarted();

            var currentView = view;
            var commonParent = _lcaTree.FindCommonParent(_previousView, _currentView);

            if (commonParent != null)
                while (currentView.ParentView != commonParent || currentView.ParentView == null)
                    currentView = currentView.ParentView;

            if (!currentView.UseHideAnimation)
            {
                view.OnHideFinished();
                view.gameObject.SetActive(false);
                return;
            }

            _viewAnimationInvoker.Hide(currentView, () =>
            {
                currentView.gameObject.SetActive(false);
                view.gameObject.SetActive(false);
                view.OnHideFinished();
            });
        }

        private void ShowAdditiveView(ViewBase view)
        {
            view.gameObject.SetActive(true);
            _currentAdditiveViews.Add(view);
        }

        private void HideLastAdditiveView()
        {
            if (_currentAdditiveViews.Count == 0)
                return;

            var additiveView = _currentAdditiveViews.Last();

            additiveView.gameObject.SetActive(false);
            _currentAdditiveViews.Remove(additiveView);
        }

        private void HideGivenAdditiveView(ViewBase view)
        {
            if (_currentAdditiveViews.Count == 0)
                return;

            var additiveView = _currentAdditiveViews.FirstOrDefault(x => x == view);
            if (additiveView == null)
                return;

            additiveView.gameObject.SetActive(false);
            _currentAdditiveViews.Remove(additiveView);
        }

        private void HideAllAdditiveViews()
        {
            if (_currentAdditiveViews.Count == 0)
                return;

            _currentAdditiveViews.ForEach(view =>
            {
                _viewAnimationInvoker.Hide(view, () =>
                {
                    view.gameObject.SetActive(false);
                    view.OnHideFinished();
                });
            });
            _currentAdditiveViews.Clear();
        }

        private ViewBase GetView(Type viewType)
        {
            return _lcaTree.GetView(viewType);
        }

        #region Editor Methods

#if UNITY_EDITOR
        [ContextMenu("Build Views Tree")]
        private void BuildViewsTreeManual()
        {
            _lcaTree = LCATreeBuilder.Build(this);
        }

        private void BuildViewsTreeAutomatically()
        {
            if (Application.isPlaying || !SUIMConfigProvider.Config.EnableAutoRebuildViewsTree)
                return;
            _lcaTree = LCATreeBuilder.Build(this);
        }
#endif

        #endregion
    }
}