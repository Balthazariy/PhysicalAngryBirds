using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI
{
    public class GameplayPage : ShowableMenuController
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _defineLaunchButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(PauseButtonOnClickHandler);
            _defineLaunchButton.onClick.AddListener(DefineLaunchButtonOnClickHandler);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseButtonOnClickHandler);
            _defineLaunchButton.onClick.RemoveListener(DefineLaunchButtonOnClickHandler);
        }

        #region Button Handlers
        private void PauseButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.PausePopup);
        }

        private void DefineLaunchButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.BallParametersPopup);
        }
        #endregion
    }
}