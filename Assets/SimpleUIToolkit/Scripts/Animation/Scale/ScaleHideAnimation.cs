using System;
using UnityEngine;

namespace SUIT.Animation.Scale
{
    [Serializable]
    public sealed class ScaleHideAnimation : ScaleAnimationBase
    {
        public override Vector3 ScaleFrom { get; set; } = Vector3.one;
        public override Vector3 ScaleTo { get; set; } = Vector3.zero;
    }
}