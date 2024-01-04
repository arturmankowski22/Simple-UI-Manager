using SUIM.Animation;

namespace SUIM.Components.Views
{
    public sealed class PlaceholderView : ViewBase
    {
        public override ViewAnimationModule ViewAnimationModule { get; } = new()
        {
            ShowAnimBehaviour =
            {
                Fade =
                {
                    UseAnimation = true
                }
            },
            HideAnimBehaviour =
            {
                Fade =
                {
                    UseAnimation = true
                }
            }
        };
    }
}