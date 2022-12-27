using Balthazariy.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI.Popups
{
    public class BallParametersPopup : ShowableMenuController
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _launchButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private Slider _pullForceSlider;
        [SerializeField] private Slider _angleSlider;

        [SerializeField] private TextMeshProUguiUtility _pullForceValueText;
        [SerializeField] private TextMeshProUguiUtility _angleValueText;

        private bool _isConfirmed;

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(ResetButtonOnClickHandler);
            _confirmButton.onClick.AddListener(ConfirmButtonOnClickHandler);
            _launchButton.onClick.AddListener(LaunchButtonOnClickHandler);
            _backButton.onClick.AddListener(BackButtonOnClickHandler);

            _pullForceSlider.onValueChanged.AddListener(PullForceSliderOnValueChangedHandler);
            _angleSlider.onValueChanged.AddListener(AngleSliderOnValueChangedHandler);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(ResetButtonOnClickHandler);
            _confirmButton.onClick.RemoveListener(ConfirmButtonOnClickHandler);
            _launchButton.onClick.RemoveListener(LaunchButtonOnClickHandler);
            _backButton.onClick.RemoveListener(BackButtonOnClickHandler);

            _pullForceSlider.onValueChanged.RemoveListener(PullForceSliderOnValueChangedHandler);
            _angleSlider.onValueChanged.RemoveListener(AngleSliderOnValueChangedHandler);
        }

        private void Awake()
        {
            _isConfirmed = false;
            SetLaunchButtonInteractable(false);
        }

        private void Start()
        {
        }

        #region Button Handlers
        private void ResetButtonOnClickHandler()
        {
            _isConfirmed = false;
            SetLaunchButtonInteractable(false);
        }

        private void ConfirmButtonOnClickHandler()
        {
            _isConfirmed = true;
            SetLaunchButtonInteractable(true);
        }

        private void LaunchButtonOnClickHandler()
        {
            _isConfirmed = false;
            SetLaunchButtonInteractable(false);

            InvokeScreenChange(Settings.MenuTypeEnumerators.GameplayPage);
        }

        private void BackButtonOnClickHandler()
        {
            _isConfirmed = false;
            SetLaunchButtonInteractable(false);

            InvokeScreenChange(Settings.MenuTypeEnumerators.GameplayPage);
        }
        #endregion

        #region Slider Handlers
        private void PullForceSliderOnValueChangedHandler(float value)
        {
            //_pullForceValueText.SetText(value.ToString());
        }

        private void AngleSliderOnValueChangedHandler(float value)
        {
            //_angleValueText.SetText(value.ToString());
        }
        #endregion

        private void SetLaunchButtonInteractable(bool interactable) => _launchButton.interactable = interactable;
    }
}