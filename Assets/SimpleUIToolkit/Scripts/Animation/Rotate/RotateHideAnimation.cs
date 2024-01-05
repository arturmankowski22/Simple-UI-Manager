using System;
using UnityEngine;

namespace SUIT.Animation.Rotate
{
    [Serializable]
    public sealed class RotateHideAnimation : RotateAnimationBase
    {
        public override Vector3 RotateFrom { get; set; } = Vector3.zero;
        public override Vector3 RotateTo { get; set; } = new(0, 0, 180);
    }
}