using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Creatures.Weapon
{
    public class ProjectTile : BasedProjectiles
    {
        protected override void Start()
        {
            base.Start();

            var force = new Vector2(Direction * _speed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}