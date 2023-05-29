using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private int _direction;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _direction = transform.lossyScale.x > 0 ? 1 : -1;
        _rigidbody = GetComponent<Rigidbody2D>();
        var force = new Vector2(_direction * _speed, 0);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
