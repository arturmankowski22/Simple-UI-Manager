using System;

namespace SUIT.Animation.Fade
{
    [Serializable]
    public sealed class FadeShowAnimation : FadeAnimationBase
    {
        public override float FadeFrom { get; set; } = 0;
        public override float FadeTo { get; set; } = 1;
    }
}