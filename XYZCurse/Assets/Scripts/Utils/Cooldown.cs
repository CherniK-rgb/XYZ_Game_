using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _values;

        private float _timesUp;

        public void Reset()
        {
            _timesUp = Time.time + _values;
        }

        public bool IsReady => _timesUp <= Time.time;
    }
}
