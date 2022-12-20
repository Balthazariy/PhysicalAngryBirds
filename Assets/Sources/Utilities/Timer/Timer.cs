using Balthazariy.Utilities.TimerUtils;
using System;
using UnityEngine;

namespace Balthazariy.Utilities
{
    public class Timer
    {
        public event Action<float> OnTimerChangedValueEvent;
        public event Action OnTimerFinishedEvent;

        public TypeOfTimersEnumaretors Type { get => _type; private set => _type = value; }
        public bool IsPaused { get => _isPaused; private set => _isPaused = value; }
        public float RemainingTime { get => _remainingTime; private set => _remainingTime = value; }

        private TypeOfTimersEnumaretors _type;
        private bool _isPaused;
        private float _remainingTime;

        public Timer(TypeOfTimersEnumaretors type)
        {
            Type = type;
        }

        public Timer(TypeOfTimersEnumaretors type, float startFrom)
        {
            if (startFrom <= 0)
            {
                Debug.LogError($"Can't start timer from {startFrom}");
                return;
            }

            _type = type;
            SetRemainingTime(startFrom);
        }

        public void SetRemainingTime(float remainingTime)
        {
            _remainingTime = remainingTime;
            OnTimerChangedValueEvent?.Invoke(RemainingTime);
        }

        public void Start()
        {
            if (_remainingTime <= 0)
            {
                Debug.LogError($"Can't start timer from {_remainingTime}");
                OnTimerFinishedEvent?.Invoke();
                return;
            }

            _isPaused = false;

            SubsribeOnEvents();

            OnTimerChangedValueEvent?.Invoke(_remainingTime);
        }

        public void Start(float startFrom)
        {
            SetRemainingTime(startFrom);

            Start();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            if (_isPaused)
                UnsubscribeOnEvents();
            else
                SubsribeOnEvents();

            OnTimerChangedValueEvent?.Invoke(_remainingTime);
        }

        public void Stop()
        {
            UnsubscribeOnEvents();

            _remainingTime = 0;

            OnTimerChangedValueEvent?.Invoke(_remainingTime);
            OnTimerFinishedEvent?.Invoke();
        }

        private void SubsribeOnEvents()
        {
            switch (_type)
            {
                case TypeOfTimersEnumaretors.TickTimer:
                    TimeInvoker.Instance.OnUpdateTickTimeEvent += OnUpdateTickTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.UnscaledTickTimer:
                    TimeInvoker.Instance.OnUpdateUnscaledTickTimeEvent += OnUpdateTickTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.OneSecTimer:
                    TimeInvoker.Instance.OnUpdateOneSecTimeEvent += OnUpdateOneSecTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.UnscaledOneSecTimer:
                    TimeInvoker.Instance.OnUpdateUnscaledOneSecTimeEvent += OnUpdateOneSecTimeEventHandler;
                    break;
            }
        }

        private void UnsubscribeOnEvents()
        {
            switch (_type)
            {
                case TypeOfTimersEnumaretors.TickTimer:
                    TimeInvoker.Instance.OnUpdateTickTimeEvent -= OnUpdateTickTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.UnscaledTickTimer:
                    TimeInvoker.Instance.OnUpdateUnscaledTickTimeEvent -= OnUpdateTickTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.OneSecTimer:
                    TimeInvoker.Instance.OnUpdateOneSecTimeEvent -= OnUpdateOneSecTimeEventHandler;
                    break;
                case TypeOfTimersEnumaretors.UnscaledOneSecTimer:
                    TimeInvoker.Instance.OnUpdateUnscaledOneSecTimeEvent -= OnUpdateOneSecTimeEventHandler;
                    break;
            }
        }

        private void OnUpdateTickTimeEventHandler(float deltaTime)
        {
            if (_isPaused)
                return;

            _remainingTime -= deltaTime;

            FinishedTimerLogic();
        }

        private void OnUpdateOneSecTimeEventHandler()
        {
            if (_isPaused)
                return;

            _remainingTime -= 1f;

            FinishedTimerLogic();
        }

        private void FinishedTimerLogic()
        {
            if (_remainingTime <= 0)
                Stop();
            else
                OnTimerChangedValueEvent?.Invoke(_remainingTime);
        }
    }
}