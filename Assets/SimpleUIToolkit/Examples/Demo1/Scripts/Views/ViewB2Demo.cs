using SUIT.Animation;
using SUIT.Animation.Move;
using SUIT.Components.Views;

namespace SUIT.Demo1
{
    public sealed class ViewB2Demo : ViewBase
    {
        public override ViewAnimationModule ViewAnimationModule { get; } = new()
        {
            ShowAnimBehaviour =
            {
                Move =
                {
                    UseAnimation = true,
                    MoveFromDirection = MoveDirection.Right
                }
            },
            HideAnimBehaviour =
            {
                Move =
                {
                    UseAnimation = true,
                    MoveToDirection = MoveDirection.Right
                }
            }
        };
    }
}