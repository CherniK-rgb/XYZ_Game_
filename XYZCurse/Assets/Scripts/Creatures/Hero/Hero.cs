using System;
using Assets.Model;
using Assets.Scripts.Creatures;
using Assets.Scripts.Utils;
using Model.Data;
using Scripts.Component;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Creatures

{
    public class Hero : Creature
    {
        [SerializeField] private float _limitFallVfx;

        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;

        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private Cooldown _throwCoolDown;


        [Space] [Header("Particles")] [SerializeField]
        private ParticleSystem _hitParticles;

        //private readonly Wallet _wallet = new();

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private bool _allowDoubleJump;
        private GameSession _session;
        
        private  int CoinsCount => _session.Data.Inventory.Count("Coin");
        private  int SwordCount => _session.Data.Inventory.Count("Sword");

        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealtheComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            
            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if(id == "Sword") UpdateHeroWeapon();
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_throwCoolDown.IsReady)
            {
                Animator.SetTrigger(ThrowKey);
                _throwCoolDown.Reset();
            }
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();

            if (_data != null) UseEffect();
        }

        protected override float CalculateYVelocity()
        {
            if (IsGrounded) _allowDoubleJump = true;

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocite)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                _particles.Spawn("Jump");
                return _jumpspeed;
            }
            return base.CalculateJumpVelocity(yVelocite);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y < _limitFallVfx)
                {
                   //_particles.Spawn("Fall");
                }
            }
        }

        public void AddInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }
        
        public override void TakeDammage()
        {
            base.TakeDammage();
            if (CoinsCount > 0)
            {
                LoseCoins();
            }
        }

        private void LoseCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);
            
            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void TakeHeal()
        {
            Animator.SetTrigger(Heal);
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }
       
        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
        }
        
        public void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;
        }
    }
}
