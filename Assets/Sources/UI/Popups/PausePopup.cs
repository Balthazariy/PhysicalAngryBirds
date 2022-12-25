using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI.Popups
{
    public class PausePopup : ShowableMenuController
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueButtonOnClickHandler);
            _restartButton.onClick.AddListener(RestartButtonOnClickHandler);
            _exitButton.onClick.AddListener(ExitButtonOnClickHandler);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueButtonOnClickHandler);
            _restartButton.onClick.RemoveListener(RestartButtonOnClickHandler);
            _exitButton.onClick.RemoveListener(ExitButtonOnClickHandler);
        }

        #region Button Handlers
        private void ContinueButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.GameplayPage);
        }

        private void RestartButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.GameplayPage);
        }

        private void ExitButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.MainPage);
        }
        #endregion
    }
}