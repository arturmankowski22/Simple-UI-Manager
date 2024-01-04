using SUIM.Animation;
using SUIM.Components.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SUIM.Demo1
{
    public sealed class ViewADemo : ViewBase
    {
        [SerializeField] private Button _openBViewButton;

        [Inject] private ViewADemoController _controller;

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

        private void Start()
        {
            _openBViewButton.onClick.AddListener(_controller.OpenBView);
        }
    }
}