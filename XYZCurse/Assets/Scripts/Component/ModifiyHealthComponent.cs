using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Component
{
    public class ModifiyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealtheComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}

