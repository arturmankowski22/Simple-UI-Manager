using System;
using UnityEngine;

namespace SUIM.Animation.Scale
{
    public interface IScaleAnimation : IAnimation
    {
        Vector3 ScaleFrom { get; }
        Vector3 ScaleTo { get; }
    }

    [Serializable]
    public abstract class ScaleAnimationBase : AnimationComponentBase, IScaleAnimation
    {
        public abstract Vector3 ScaleFrom { get; set; }
        public abstract Vector3 ScaleTo { get; set; }
    }
}