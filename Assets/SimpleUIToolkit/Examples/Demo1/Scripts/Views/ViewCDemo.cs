using SUIT.Animation;
using SUIT.Components.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SUIT.Demo1
{
    public class ViewCDemo : ViewBase
    {
        [SerializeField] private Button _openBViewButton;
        [SerializeField] private Button _openDViewButton;

        [Inject] private ViewCDemoController _controller;

        public override ViewAnimationModule ViewAnimationModule { get; } = new()
        {
            ShowAnimBehaviour =
            {
                Scale =
                {
                    UseAnimation = true
                }
            },
            HideAnimBehaviour =
            {
                Scale =
                {
                    UseAnimation = true
                }
            }
        };

        private void Start()
        {
            _openBViewButton.onClick.AddListener(_controller.OpenBView);
            _openDViewButton.onClick.AddListener(_controller.OpenDView);
        }
    }
}