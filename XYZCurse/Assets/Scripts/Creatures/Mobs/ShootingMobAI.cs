using Assets.Scripts.Utils;
using Scripts.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMobAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;

    [Header("Melee")]
    [SerializeField] private Cooldown _cooldownMelee;
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] private LayerCheck __meleecanAttack;

    [Header("Range")]
    [SerializeField] private SpawnComponent _rangeAttack;
    [SerializeField] private Cooldown _rangeCooldown;

    private void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (__meleecanAttack.IsTouchingLayer)
            {
                if (_cooldownMelee.IsReady)
                {
                    MeleeAttack();
                }
            }

            if (_rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
    }

    private void RangeAttack()
    {

    }

    private void MeleeAttack()
    {

    }
}
