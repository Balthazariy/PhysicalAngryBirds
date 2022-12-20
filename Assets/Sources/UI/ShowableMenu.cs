using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Balthazariy.UI
{
    public class ShowableMenu : Monoable<CanvasGroup>, IShowable
    {
        public CanvasGroup CanvasGroup => Dependency;

        public event Action OnShowEvent;
        public event Action OnHideEvent;

        private bool _isShowing;
        [SerializeField] private string _menuType;

        public bool IsActive => _isShowing;

        public string MenuType => _menuType;

        public GameObject ActivePage => CanvasGroup.gameObject;

        public void Show()
        {
            _isShowing = true;

            ShowPage();

            OnShowEvent?.Invoke();

            ActivePage.transform.localPosition = new Vector3(0, ActivePage.transform.position.y - (Screen.height / 2), 0);
            ActivePage.transform.DOLocalMoveY(0, 0.5f);
        }

        public void Hide()
        {
            _isShowing = false;

            ShowPage();

            OnHideEvent?.Invoke();

            ActivePage.transform.localPosition = new Vector3(0, 0, 0);
            ActivePage.transform.DOLocalMoveY(ActivePage.transform.position.y - (Screen.height / 2), 0.5f);
        }


        private void ShowPage()
        {
            CanvasGroup.alpha = _isShowing ? 1 : 0;
            CanvasGroup.interactable = _isShowing;
            CanvasGroup.blocksRaycasts = _isShowing;
        }
    }
}