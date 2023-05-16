using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")] 
    public void SpawnParticles()
    {
        var instantiate =  Instantiate(_prefab, _target.position, Quaternion.identity);
        instantiate.transform.localScale = transform.lossyScale; // relatively all scene
    }
}
