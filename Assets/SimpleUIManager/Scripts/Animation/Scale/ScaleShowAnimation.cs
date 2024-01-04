using System;
using UnityEngine;

namespace SUIM.Animation.Scale
{
    [Serializable]
    public sealed class ScaleShowAnimation : ScaleAnimationBase
    {
        public override Vector3 ScaleFrom { get; set; } = Vector3.zero;
        public override Vector3 ScaleTo { get; set; } = Vector3.one;
    }
}