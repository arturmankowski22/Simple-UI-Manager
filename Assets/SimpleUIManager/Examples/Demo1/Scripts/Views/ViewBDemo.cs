using SUIM.Animation;
using SUIM.Components.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SUIM.Demo1
{
    public class ViewBDemo : ViewBase
    {
        [SerializeField] private Button _openAViewButton;
        [SerializeField] private Button _openCViewButton;
        [SerializeField] private Button _openB1ViewButton;
        [SerializeField] private Button _openB2ViewButton;

        [Inject] private ViewBDemoController _controller;

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

        private void Start()
        {
            _openAViewButton.onClick.AddListener(_controller.OpenAView);
            _openCViewButton.onClick.AddListener(_controller.OpenCView);
            _openB1ViewButton.onClick.AddListener(_controller.OpenB1SubView);
            _openB2ViewButton.onClick.AddListener(_controller.OpenB2SubView);
        }
    }
}