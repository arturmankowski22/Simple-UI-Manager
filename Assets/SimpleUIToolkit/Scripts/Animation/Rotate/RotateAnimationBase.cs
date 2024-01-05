using System;
using UnityEngine;

namespace SUIT.Animation.Rotate
{
    public interface IRotateAnimation : IAnimation
    {
        Vector3 RotateFrom { get; }
        Vector3 RotateTo { get; }
    }

    [Serializable]
    public abstract class RotateAnimationBase : AnimationComponentBase, IRotateAnimation
    {
        public abstract Vector3 RotateFrom { get; set; }
        public abstract Vector3 RotateTo { get; set; }
    }
}