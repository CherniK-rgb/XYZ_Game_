using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Meteorit : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 1.5f);
        if (collision.gameObject.name == "ColiderDelete") 
            Destroy(gameObject);
    }
}