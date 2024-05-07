using Assets.Scripts.Utils;
using Script;
using UnityEngine;

namespace Assets.Scripts.Creatures.Mobs

{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private ScriptAnimation _animation;

        private void Update()
        {
            if (_vision.IsTouchingLayer && _cooldown.IsReady)
                Shoot();
        }

        private void Shoot()
        {
            _cooldown.Reset();
            _animation.SetClip("start-attack");
        }
    }
}