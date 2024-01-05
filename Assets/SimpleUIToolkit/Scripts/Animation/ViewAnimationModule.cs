using System;
using SUIT.Animation.Fade;
using SUIT.Animation.Move;
using SUIT.Animation.Rotate;
using SUIT.Animation.Scale;

namespace SUIT.Animation
{
    public interface IAnimationBehaviourBase
    {
        bool UseAnimation { get; }
        IMoveAnimation Move { get; }
        IRotateAnimation Rotate { get; }
        IScaleAnimation Scale { get; }
        IFadeAnimation Fade { get; }
    }

    [Serializable]
    public sealed class ViewAnimationModule
    {
        public AnimationBehaviourBase<MoveShowAnimation, RotateShowAnimation, ScaleShowAnimation, FadeShowAnimation>
            ShowAnimBehaviour { get; } = new ShowAnimBehaviour();

        public AnimationBehaviourBase<MoveHideAnimation, RotateHideAnimation, ScaleHideAnimation, FadeHideAnimation>
            HideAnimBehaviour { get; } = new HideAnimBehaviour();
    }

    [Serializable]
    public abstract class AnimationBehaviourBase<TMove, TRotate, TScale, TFade> : IAnimationBehaviourBase
        where TMove : IMoveAnimation
        where TRotate : IRotateAnimation
        where TScale : IScaleAnimation
        where TFade : IFadeAnimation
    {
        public abstract TMove Move { get; }
        public abstract TRotate Rotate { get; }
        public abstract TScale Scale { get; }
        public abstract TFade Fade { get; }

        public bool UseAnimation => Move.UseAnimation ||
                                    Rotate.UseAnimation ||
                                    Scale.UseAnimation ||
                                    Fade.UseAnimation;

        IMoveAnimation IAnimationBehaviourBase.Move => Move;
        IRotateAnimation IAnimationBehaviourBase.Rotate => Rotate;
        IScaleAnimation IAnimationBehaviourBase.Scale => Scale;
        IFadeAnimation IAnimationBehaviourBase.Fade => Fade;
    }

    [Serializable]
    public sealed class ShowAnimBehaviour : AnimationBehaviourBase<MoveShowAnimation, RotateShowAnimation,
        ScaleShowAnimation, FadeShowAnimation>
    {
        public override MoveShowAnimation Move { get; } = new();
        public override RotateShowAnimation Rotate { get; } = new();
        public override ScaleShowAnimation Scale { get; } = new();
        public override FadeShowAnimation Fade { get; } = new();
    }

    [Serializable]
    public sealed class HideAnimBehaviour : AnimationBehaviourBase<MoveHideAnimation, RotateHideAnimation,
        ScaleHideAnimation, FadeHideAnimation>
    {
        public override MoveHideAnimation Move { get; } = new();
        public override RotateHideAnimation Rotate { get; } = new();
        public override ScaleHideAnimation Scale { get; } = new();
        public override FadeHideAnimation Fade { get; } = new();
    }
}