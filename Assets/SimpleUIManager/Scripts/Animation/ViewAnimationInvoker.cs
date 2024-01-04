using System;
using System.Collections.Generic;
using DG.Tweening;
using SUIM.Animation.Fade;
using SUIM.Animation.Move;
using SUIM.Animation.Rotate;
using SUIM.Animation.Scale;
using SUIM.Components.Views;
using UnityEngine;
using UnityEngine.UI;

namespace SUIM.Animation
{
    public interface IViewAnimationInvoker
    {
        void Show(ViewBase view, Action onAnimationFinished = null);
        void Hide(ViewBase view, Action onAnimationFinished = null);
    }

    public sealed class ViewAnimationInvoker : IViewAnimationInvoker
    {
        private readonly Dictionary<ViewBase, Sequence> _activeSequences = new();

        public void Show(ViewBase view, Action onAnimationFinished = null)
        {
            InvokeAnimation(view, true, onAnimationFinished);
        }

        public void Hide(ViewBase view, Action onAnimationFinished = null)
        {
            InvokeAnimation(view, false, onAnimationFinished);
        }

        private void InvokeAnimation(ViewBase view, bool isShowAnimation, Action onAnimationFinished = null)
        {
            var animationBehaviour = GetAnimationBehaviour(view, isShowAnimation);
            if (!animationBehaviour.UseAnimation)
                return;

            TryKillAlreadyActiveSequence(view);
            var sequence = DOTween.Sequence()
                .OnComplete(() => onAnimationFinished?.Invoke())
                .OnKill(view.ResetView);
            RegisterNewSequence(sequence, view);

            SetupAnimations(sequence, view, animationBehaviour, isShowAnimation);
        }

        private IAnimationBehaviourBase GetAnimationBehaviour(ViewBase view, bool isShowAnimation)
        {
            return isShowAnimation
                ? view.ViewAnimationModule.ShowAnimBehaviour
                : view.ViewAnimationModule.HideAnimBehaviour;
        }

        private void SetupAnimations(Sequence sequence, ViewBase view, IAnimationBehaviourBase animationBehaviour,
            bool isShowAnimation)
        {
            if (animationBehaviour.Move.UseAnimation)
                SetupMoveAnimation(sequence, view, animationBehaviour.Move, isShowAnimation);

            if (animationBehaviour.Rotate.UseAnimation)
                SetupRotateAnimation(sequence, view, animationBehaviour.Rotate);

            if (animationBehaviour.Scale.UseAnimation)
                SetupScaleAnimation(sequence, view, animationBehaviour.Scale);

            if (animationBehaviour.Fade.UseAnimation)
                SetupFadeAnimation(sequence, view, animationBehaviour.Fade);
        }

        private void RegisterNewSequence(Sequence sequence, ViewBase view)
        {
            _activeSequences[view] = sequence;
        }

        private void TryKillAlreadyActiveSequence(ViewBase view)
        {
            if (!_activeSequences.TryGetValue(view, out var activeSequence))
                return;

            if (activeSequence == null)
                return;

            activeSequence.OnComplete(null);
            activeSequence.Kill(true);
            _activeSequences[view] = null;
        }

        private static void SetupMoveAnimation(Sequence sequence, ViewBase view, IMoveAnimation moveAnimation,
            bool isShowAnimation)
        {
            Vector2 startValue;
            Vector2 endValue;

            var size = GetCanvasSize(view);
            if (moveAnimation.IsHorizontalMovement)
            {
                var viewWidth = view.RectTransform.sizeDelta.x;
                if (isShowAnimation)
                {
                    startValue = moveAnimation.MoveDirection == MoveDirection.Left
                        ? new Vector2(-(size.x + viewWidth), 0)
                        : new Vector2(size.x + viewWidth, 0);
                    endValue = Vector2.zero;
                }
                else
                {
                    startValue = Vector2.zero;
                    endValue = moveAnimation.MoveDirection == MoveDirection.Left
                        ? new Vector2(-(size.x + viewWidth), 0)
                        : new Vector2(size.x + viewWidth, 0);
                }
            }
            else
            {
                var viewHeight = view.RectTransform.sizeDelta.y;
                if (isShowAnimation)
                {
                    startValue = moveAnimation.MoveDirection == MoveDirection.Up
                        ? new Vector2(0, size.y + viewHeight)
                        : new Vector2(0, -(size.y + viewHeight));
                    endValue = Vector2.zero;
                }
                else
                {
                    startValue = Vector2.zero;
                    endValue = moveAnimation.MoveDirection == MoveDirection.Up
                        ? new Vector2(0, size.y + viewHeight)
                        : new Vector2(0, -(size.y + viewHeight));
                }
            }

            view.RectTransform.anchoredPosition = startValue;
            sequence.Join(view.RectTransform.DOAnchorPos(endValue, moveAnimation.Duration))
                .SetEase(moveAnimation.Ease);
        }

        private static void SetupRotateAnimation(Sequence sequence, ViewBase view, IRotateAnimation rotateAnimation)
        {
            view.transform.rotation = Quaternion.Euler(rotateAnimation.RotateFrom);
            sequence.Join(view.transform.DORotate(rotateAnimation.RotateTo, rotateAnimation.Duration))
                .SetEase(rotateAnimation.Ease);
        }

        private static void SetupScaleAnimation(Sequence sequence, ViewBase view, IScaleAnimation scaleAnimation)
        {
            view.transform.localScale = scaleAnimation.ScaleFrom;
            sequence.Join(view.transform.DOScale(scaleAnimation.ScaleTo, scaleAnimation.Duration))
                .SetEase(scaleAnimation.Ease);
        }

        private static void SetupFadeAnimation(Sequence sequence, ViewBase view, IFadeAnimation fadeAnimation)
        {
            view.CanvasGroup.alpha = Mathf.Clamp01(fadeAnimation.FadeFrom);
            sequence.Join(view.CanvasGroup.DOFade(Mathf.Clamp01(fadeAnimation.FadeTo), fadeAnimation.Duration))
                .SetEase(fadeAnimation.Ease);
        }

        private static Vector2 GetCanvasSize(ViewBase view)
        {
            var canvasScaler = view.GetComponentInParent<CanvasScaler>();
            if (canvasScaler == null)
                return new Vector2(Screen.width, Screen.height);

            var rect = canvasScaler.GetComponent<RectTransform>().rect;
            return new Vector2(rect.width, rect.height);
        }
    }
}