using System;
using UnityEngine;

namespace SUIT.Animation.Rotate
{
    [Serializable]
    public sealed class RotateShowAnimation : RotateAnimationBase
    {
        public override Vector3 RotateFrom { get; set; } = new(0, 0, 180);
        public override Vector3 RotateTo { get; set; } = Vector3.zero;
    }
}