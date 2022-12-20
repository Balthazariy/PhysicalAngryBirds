using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balthazariy.UI
{
    public abstract class Monoable<T> : MonoBehaviour where T : Component
    {
        private void Reset()
        {
            _dependency = GetComponent<T>();
        }

        [Header("DependendBehaviour")]
        [SerializeField]
        private T _dependency;

        protected T Dependency
        {
            get
            {
                if (_dependency == null)
                {
                    try
                    {
                        _dependency = GetComponent<T>();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Missing Component in {gameObject.name}\n\n{e}");
                    }
                }

                return _dependency;
            }
        }
    }

    public abstract class Monoable<T, K> : MonoBehaviour where T : Component where K : Component
    {
        private void Reset()
        {
            _dependency = GetComponent<T>();
            _dependency2 = GetComponent<K>();
        }

        [Header("DependendBehaviour")]
        [SerializeField]
        private T _dependency;

        protected T Dependency
        {
            get
            {
                if (_dependency == null)
                    _dependency = GetComponent<T>();

                return _dependency;
            }
        }

        [SerializeField]
        private K _dependency2;

        protected K Dependency2
        {
            get
            {
                if (_dependency2 == null)
                    _dependency2 = GetComponent<K>();

                return _dependency2;
            }
        }
    }
}