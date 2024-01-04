using System;

namespace SUIM.Animation.Fade
{
    public interface IFadeAnimation : IAnimation
    {
        float FadeFrom { get; }
        float FadeTo { get; }
    }

    [Serializable]
    public abstract class FadeAnimationBase : AnimationComponentBase, IFadeAnimation
    {
        public abstract float FadeFrom { get; set; }
        public abstract float FadeTo { get; set; }
    }
}