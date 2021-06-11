using System;
using UnityEngine;

public class BallEvenArgs : MonoBehaviour
{
    public event Action BallIsGroundedEvent;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        BallIsGroundedEvent?.Invoke();
    }
}