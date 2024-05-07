using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")] 
    public void Spawn()
    {
        var instantiate =  Instantiate(_prefab, _target.position, Quaternion.identity);

        var scale = transform.lossyScale;
        instantiate.transform.localScale = scale; // relatively all scene
        instantiate.SetActive(true);
        
    }
}
