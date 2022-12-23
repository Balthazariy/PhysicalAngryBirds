using Balthazariy.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Balthazariy.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private List<ShowableMenuController> _menus = new List<ShowableMenuController>();

        private void OnEnable()
        {
            foreach (var menu in _menus)
                menu.OnScreenChangeRequestedEvent += OnScreenChangeRequestedEventHandler;
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

            targetMenu.Show();
        }
    }
}