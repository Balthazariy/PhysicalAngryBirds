using Balthazariy.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI.Pages
{
    public class MainPage : ShowableMenuController
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(PlayButtonOnClickHandler);
            _settingButton.onClick.AddListener(SettingsButtonOnClickHandler);

        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(PlayButtonOnClickHandler);
            _settingButton.onClick.RemoveListener(SettingsButtonOnClickHandler);
        }

        private void Awake()
        {
        }

        private void Start()
        {
            Show();
        }

        #region Button Handlers
        private void PlayButtonOnClickHandler()
        {
            InvokeScreenChange(MenuTypeEnumerators.GameplayPage);
        }

        private void SettingsButtonOnClickHandler()
        {

        }

        #endregion
    }
}