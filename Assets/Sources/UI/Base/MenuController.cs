using Balthazariy.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Balthazariy.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private List<ShowableMenuController> _menus = new List<ShowableMenuController>();
        [SerializeField] private GameObject _overlayPanelObject;

        private void OnEnable()
        {
            foreach (var menu in _menus)
            {
                menu.OnScreenChangeRequestedEvent += OnScreenChangeRequestedEventHandler;
                menu.Hide();
            }

            _menus.Find(name => name.ShowableMenu.MenuType == MenuTypeEnumerators.MainPage).Show();
        }

        private void OnDisable()
        {
            foreach (var menu in _menus)
                menu.OnScreenChangeRequestedEvent -= OnScreenChangeRequestedEventHandler;
        }

        private void OnScreenChangeRequestedEventHandler(MenuTypeEnumerators value)
        {
            if (value == MenuTypeEnumerators.Unknown)
            {
                Debug.LogError($"You try to change menu to [{value}]");
                return;
            }

            var activeMenu = _menus.Find(name => name.ShowableMenu.IsActive);
            var targetMenu = _menus.Find(name => name.ShowableMenu.MenuType == value);

            activeMenu.Hide();

            _overlayPanelObject.SetActive(targetMenu.ShowableMenu.NeedOverlay);

            targetMenu.Show();
        }
    }
}