using System;

namespace SUIT.Animation.Fade
{
    [Serializable]
    public sealed class FadeHideAnimation : FadeAnimationBase
    {
        public override float FadeFrom { get; set; } = 1;
        public override float FadeTo { get; set; } = 0;
    }
}