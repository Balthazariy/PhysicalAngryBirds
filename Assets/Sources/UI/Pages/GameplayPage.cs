using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI
{
    public class GameplayPage : ShowableMenuController
    {
        [SerializeField] private Button _pauseButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(PauseButtonOnClickHandler);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseButtonOnClickHandler);
        }

        #region Button Handlers
        private void PauseButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.MainPage);
        }
        #endregion
    }
}