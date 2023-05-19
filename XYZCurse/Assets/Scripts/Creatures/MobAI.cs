using Assets.Scripts.Creatures;
using Scripts.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCoolDown = 0.5f;


        private Coroutine _current;
        private GameObject _target;
        private Creature _creature;
        private Animator _animator;


        private SpawnListComponent _particules;
        private Patrol _patrol;


        private bool _isDead;
        protected static readonly int IsDeadKey = Animator.StringToHash("is-dead");


        private void Awake()
        {
            _particules = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;
            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            _particules.Spawn("Agro");

            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
         
        }
        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
             

                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }

                else
                {
                    SetDirectionToTarget();
                }

                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);

            if (_current != null) 
                StopCoroutine(_current);
        }
    }
}