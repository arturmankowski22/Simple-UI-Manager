using SUIM.Animation;
using SUIM.Components.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SUIM.Demo1
{
    public class ViewDDemo : ViewBase
    {
        [SerializeField] private Button _openCViewButton;

        [Inject] private ViewDDemoController _controller;

        public override ViewAnimationModule ViewAnimationModule { get; } = new()
        {
            ShowAnimBehaviour =
            {
                Rotate =
                {
                    UseAnimation = true
                }
            },
            HideAnimBehaviour =
            {
                Rotate =
                {
                    UseAnimation = true
                }
            }
        };

        private void Start()
        {
            _openCViewButton.onClick.AddListener(_controller.OpenCView);
        }
    }
}