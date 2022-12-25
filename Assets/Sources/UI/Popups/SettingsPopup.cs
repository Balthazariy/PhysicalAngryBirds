using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.UI.Popups
{
    public class SettingsPopup : ShowableMenuController
    {
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _languageButton;
        [SerializeField] private Button _backButton;

        private void OnEnable()
        {
            _musicButton.onClick.AddListener(MusicButtonOnClickHandler);
            _soundButton.onClick.AddListener(SoundButtonOnClickHandler);
            _languageButton.onClick.AddListener(LanguageButtonOnClickHandler);
            _backButton.onClick.AddListener(BackButtonOnClickHandler);
        }

        private void OnDisable()
        {
            _musicButton.onClick.RemoveListener(MusicButtonOnClickHandler);
            _soundButton.onClick.RemoveListener(SoundButtonOnClickHandler);
            _languageButton.onClick.RemoveListener(LanguageButtonOnClickHandler);
            _backButton.onClick.RemoveListener(BackButtonOnClickHandler);
        }

        #region Button Handlers
        private void MusicButtonOnClickHandler()
        {
            Debug.Log("Not impelemnted yet");
        }

        private void SoundButtonOnClickHandler()
        {
            Debug.Log("Not impelemnted yet");
        }

        private void LanguageButtonOnClickHandler()
        {
            Debug.Log("Not impelemnted yet");
        }

        private void BackButtonOnClickHandler()
        {
            InvokeScreenChange(Settings.MenuTypeEnumerators.MainPage);
        }
        #endregion
    }
}