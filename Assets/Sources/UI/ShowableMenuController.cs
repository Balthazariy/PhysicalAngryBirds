using System;
using UnityEngine;

namespace Balthazariy.UI
{
    public abstract class ShowableMenuController : Monoable<ShowableMenu>
    {
        public ShowableMenu ShowableMenu => Dependency;

        protected bool _isShowing = false;

        public event Action<string> OnScreenChangeRequestedEvent;

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

        public void InvokeScreenChange(string screen)
        {
            //Debug.Log($"Screenchange requested: {screen}");
            OnScreenChangeRequestedEvent?.Invoke(screen);
        }
    }
}