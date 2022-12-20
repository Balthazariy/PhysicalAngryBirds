using System;
using UnityEngine;

namespace Balthazariy.Utilities.TimerUtils
{
    public class TimeInvoker : MonoBehaviour
    {
        public event Action<float> OnUpdateTickTimeEvent;
        public event Action<float> OnUpdateUnscaledTickTimeEvent;
        public event Action OnUpdateOneSecTimeEvent;
        public event Action OnUpdateUnscaledOneSecTimeEvent;

        public static TimeInvoker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeInvoker();
                }
                return _instance;
            }
        }

        private static TimeInvoker _instance;

        private float _oneSecTimer,
                      _oneSecTimerUnscaled;

        private void Update()
        {
            UpdateScaledTime();

            UpdateUnscaledTime();
        }

        private void UpdateScaledTime()
        {
            var deltaTime = Time.deltaTime;

            OnUpdateTickTimeEvent?.Invoke(deltaTime);

            _oneSecTimer += deltaTime;
            if (_oneSecTimer >= 1f)
            {
                _oneSecTimer -= 1f;
                OnUpdateOneSecTimeEvent?.Invoke();
            }
        }

        private void UpdateUnscaledTime()
        {
            var unscaledDeltaTime = Time.unscaledDeltaTime;

            OnUpdateUnscaledTickTimeEvent?.Invoke(unscaledDeltaTime);

            _oneSecTimerUnscaled += unscaledDeltaTime;
            if (_oneSecTimerUnscaled >= 1f)
            {
                _oneSecTimerUnscaled -= 1f;
                OnUpdateUnscaledOneSecTimeEvent?.Invoke();
            }
        }
    }
}