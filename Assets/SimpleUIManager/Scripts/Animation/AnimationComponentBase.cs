using System;
using DG.Tweening;

namespace SUIM.Animation
{
    public interface IAnimation
    {
        bool UseAnimation { get; }
        float Duration { get; }
        Ease Ease { get; }
    }

    [Serializable]
    public abstract class AnimationComponentBase : IAnimation
    {
        public bool UseAnimation { get; set; }
        public float Duration { get; set; } = 0.3f;
        public Ease Ease { get; set; } = Ease.Linear;
    }
}