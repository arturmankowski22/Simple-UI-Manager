using System;

namespace SUIT.Animation.Move
{
    [Serializable]
    public sealed class MoveHideAnimation : MoveAnimationBase
    {
        public override MoveDirection MoveDirection => MoveToDirection;
        public MoveDirection MoveToDirection { get; set; } = MoveDirection.Left;
    }
}