using Assets.Scripts.Utils;
using Scripts.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeashellTrapAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;

    [Header("Melee")]
    [SerializeField] private Cooldown _cooldownMelee;
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] private LayerCheck _meleeCanAttack;

    [Header("Range")]
    [SerializeField] private SpawnComponent _rangeAttack;
    [SerializeField] private Cooldown _rangeCooldown;

    protected static readonly int Melee = Animator.StringToHash("melee");
    protected static readonly int Range = Animator.StringToHash("range");
    protected static readonly int Hit = Animator.StringToHash("hitSeaShell");
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (_meleeCanAttack.IsTouchingLayer)
            {
                if (_cooldownMelee.IsReady)
                    MeleeAttack();
                return;
            }

            if (_rangeCooldown.IsReady)
                RangeAttack();
        }
    }

    private void RangeAttack()
    {
        _rangeCooldown.Reset();
        _animator.SetTrigger(Range);
    }

    private void MeleeAttack()
    {
        _cooldownMelee.Reset();
        _animator.SetTrigger(Melee);
    }

    public void OnMeleeAttack()
    {
        _meleeAttack.Check();
    }

    public void OnRangeAttack()
    {
        _rangeAttack.Spawn();
    }

    public  void TakeDammage()
    {
        _animator.SetTrigger(Hit);
    }
}
