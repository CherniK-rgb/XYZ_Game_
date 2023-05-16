using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private float torqueMin, torqueMax;
    [SerializeField] private Meteorit meteorit;
    [SerializeField] private int minQMeteor, maxQMeteor;
    [SerializeField] private float xPositMin, xPositMax, yPositMin, yPositMax;
    [SerializeField] private float xForceMin, xForceMax, yForceMin, yForceMax;
    
    Collider2D collision;
    
    float timerate;

    private void Update()
    {
        int QMeteor = Random.Range(minQMeteor, maxQMeteor);
        timerate += Time.deltaTime;
        if (timerate < spawnRate) return;

        Spawn(QMeteor);
        timerate = 0;
    }

    private void Spawn(int QMeteorr)
    {
        for (int i = 0; i < QMeteorr; ++i)
        {
            var position = new Vector2(Random.Range(xPositMin, xPositMax), Random.Range(yPositMin, yPositMax));
            var velocity = new Vector2(Random.Range(xForceMin, xForceMax), Random.Range(yForceMin, yForceMax));
            var torque = Random.Range(torqueMin, torqueMax);

            var meteor = Instantiate(meteorit, position, Quaternion.identity);
            var rigidbody = meteor.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(velocity);
            rigidbody.AddTorque(torque);
        }
    }
}
