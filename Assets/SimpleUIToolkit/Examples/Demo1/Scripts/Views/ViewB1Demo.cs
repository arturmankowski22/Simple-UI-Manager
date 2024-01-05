using SUIT.Animation;
using SUIT.Components.Views;

namespace SUIT.Demo1
{
    public sealed class ViewB1Demo : ViewBase
    {
        public override ViewAnimationModule ViewAnimationModule { get; } = new()
        {
            ShowAnimBehaviour =
            {
                Move =
                {
                    UseAnimation = true
                }
            },
            HideAnimBehaviour =
            {
                Move =
                {
                    UseAnimation = true
                }
            }
        };
    }
}