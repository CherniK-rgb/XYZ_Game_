using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Scripts.Component
{
    public class PotionTrigerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(_tag))
            {
                _action?.Invoke();
            }
        }
    }
}