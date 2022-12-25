using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI.Popups
{
    public class GameoverPopup : ShowableMenuController
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _metersText;

        private string _localizedMetersText;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueButtonOnClickHandler);
            _exitButton.onClick.AddListener(ExitButtonOnClickHandler);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueButtonOnClickHandler);
            _exitButton.onClick.RemoveListener(ExitButtonOnClickHandler);
        }

        private void Awake()
        {

        }

        private void Start()
        {

        }

        private void UpdateLocalization()
        {
            _localizedMetersText = "Meters: ";
        }

        private void SetFinalMeters(int value)
        {
            _metersText.text = _localizedMetersText + value.ToString();
        }

        #region Button Handlers
        private void ContinueButtonOnClickHandler()
        {
            // todo add reset gameplay logic
            InvokeScreenChange(Settings.MenuTypeEnumerators.GameplayPage);
        }

        private void ExitButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.MainPage);
        }
        #endregion
    }
}