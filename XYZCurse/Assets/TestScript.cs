using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SomeCoroutine());
    }

    private IEnumerator SomeCoroutine()
    {
        var some = "34";
         some += "42";


        while (enabled)
        {
            Debug.Log(some);
            yield return new WaitForSeconds(10f);
            yield return null;
        }
    } 
}
