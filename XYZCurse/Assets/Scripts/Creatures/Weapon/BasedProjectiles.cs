using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures.Weapon
{
    public class BasedProjectiles : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected bool _invert;

        protected int Direction;
        protected Rigidbody2D Rigidbody;

        protected virtual void Start()
        {
            var mod = _invert ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
