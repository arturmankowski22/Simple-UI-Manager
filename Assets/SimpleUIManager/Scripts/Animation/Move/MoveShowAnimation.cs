using System;

namespace SUIM.Animation.Move
{
    [Serializable]
    public sealed class MoveShowAnimation : MoveAnimationBase
    {
        public override MoveDirection MoveDirection => MoveFromDirection;
        public MoveDirection MoveFromDirection { get; set; } = MoveDirection.Left;
    }
}