using DG.Tweening;
using System;
using UnityEngine;
using Balthazariy.Settings;

namespace Balthazariy.UI
{
    public class ShowableMenu : Monoable<CanvasGroup>, IShowable
    {
        public CanvasGroup CanvasGroup => Dependency;

        public event Action OnShowEvent;
        public event Action OnHideEvent;

        private bool _isShowing;
        [SerializeField] private MenuTypeEnumerators _menuType;

        public bool IsActive => _isShowing;

        public MenuTypeEnumerators MenuType => _menuType;

        public GameObject SelfObject => CanvasGroup.gameObject;

        public void Show()
        {
            _isShowing = true;

            ShowPage();

            OnShowEvent?.Invoke();

            SelfObject.transform.localPosition = new Vector3(0, SelfObject.transform.position.y - (Screen.height / 2), 0);
            SelfObject.transform.DOLocalMoveY(0, 0.5f);
        }

        public void Hide()
        {
            _isShowing = false;

            ShowPage();

            OnHideEvent?.Invoke();

            SelfObject.transform.localPosition = new Vector3(0, 0, 0);
            SelfObject.transform.DOLocalMoveY(SelfObject.transform.position.y - (Screen.height / 2), 0.5f);
        }


        private void ShowPage()
        {
            CanvasGroup.alpha = _isShowing ? 1 : 0;
            CanvasGroup.interactable = _isShowing;
            CanvasGroup.blocksRaycasts = _isShowing;
        }
    }
}