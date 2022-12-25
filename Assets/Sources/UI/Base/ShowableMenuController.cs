using Balthazariy.Settings;
using System;

namespace Balthazariy.UI
{
    public abstract class ShowableMenuController : Monoable<ShowableMenu>
    {
        public ShowableMenu ShowableMenu => Dependency;

        protected bool _isShowing = false;

        public event Action<MenuTypeEnumerators> OnScreenChangeRequestedEvent;

        public virtual void Show()
        {
            _isShowing = true;

            ShowableMenu.Show();
        }

        public virtual void Hide()
        {
            _isShowing = false;

            ShowableMenu.Hide();
        }

        public void InvokeScreenChange(MenuTypeEnumerators screen)
        {
            OnScreenChangeRequestedEvent?.Invoke(screen);
        }
    }
}