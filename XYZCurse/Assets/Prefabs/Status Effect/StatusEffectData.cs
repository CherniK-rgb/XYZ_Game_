using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public string Name;
    public int DOTAmount;
    public float TickSpeed;
    public float LifeTime;

    // public GameObject EffectParticles;
}
 
