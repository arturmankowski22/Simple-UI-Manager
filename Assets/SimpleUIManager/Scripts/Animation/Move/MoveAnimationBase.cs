using System;

namespace SUIM.Animation.Move
{
    public enum MoveDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public interface IMoveAnimation : IAnimation
    {
        bool IsHorizontalMovement { get; }
        MoveDirection MoveDirection { get; }
    }

    [Serializable]
    public abstract class MoveAnimationBase : AnimationComponentBase, IMoveAnimation
    {
        public bool IsHorizontalMovement => MoveDirection is MoveDirection.Left or MoveDirection.Right;
        public abstract MoveDirection MoveDirection { get; }
    }
}