﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _treshold = 1f;

        private Creature _creature;
        private int _currentPoint;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }


        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                {
                    _currentPoint = (int) Mathf.Repeat(_currentPoint + 1, _points.Length);
                }

                var direction = _points[_currentPoint].position - transform.position;
                direction.y = 0;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
            yield return null;
        }

        private bool IsOnPoint()
        {
            // если _treshold больше чем длинна вектора значит мы уже дошли до этой точки
            return (_points[_currentPoint].position - transform.position).magnitude < _treshold;
        }
    }
}
