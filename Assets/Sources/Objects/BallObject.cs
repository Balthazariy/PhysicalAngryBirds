using System;
using UnityEngine;

namespace Balthazariy.Objects
{
    public class BallObject : MonoBehaviour
    {
        public event Action OnBallGroundedEvent;




        private void OnTriggerEnter2D()
        {
            OnBallGroundedEvent?.Invoke();
        }
    }
}