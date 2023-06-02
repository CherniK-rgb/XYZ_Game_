using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Component
{
    public class HealtheComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;
      
            _health += healthDelta;

            _onChange?.Invoke(_health);

            if(healthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
            //чтобы передать значение текущего здоровья
        }

        internal void SetHealth(int health)
        {
            _health = health;
        }
    }
}

