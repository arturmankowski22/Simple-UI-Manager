using SUIM.Animation;
using SUIM.Animation.Move;
using SUIM.Components.Views;

namespace SUIM.Demo1
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