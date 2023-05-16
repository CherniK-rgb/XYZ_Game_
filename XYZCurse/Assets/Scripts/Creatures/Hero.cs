using Assets.Model;
using Assets.Scripts.Creatures;
using Assets.Scripts.Utils;
using Scripts.Component;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Creatures

{
    public class Hero : Creature
    {
        [SerializeField] private float _limitFallVfx;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private CheckCircleOverlap _interactionCheck;


        [Space] [Header("Particles")] [SerializeField]
        private ParticleSystem _hitParticles;

        private readonly Wallet _wallet = new();

        private bool _allowDoubleJump;
        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealtheComponent>();

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
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
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpspeed;
            }
            return base.CalculateJumpVelocity(yVelocite);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _jumpspeed)
                {
                    _particles.Spawn("Fall");
                }
            }
        }

        public Wallet GetWallet()
        {
            _session.Data.Coins += 1;
            return _wallet;
        }

        protected override void TakeDammage()
        {
            base.TakeDammage();

            if (_session.Data.Coins > 0) LoseCoins();
        }

        private void LoseCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispose;

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
            if (!_session.Data.IsArmed) return;

            base.Attack();
        }

        internal void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
            Animator.runtimeAnimatorController = _armed;
        }

        public void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;
        }
    }
}
