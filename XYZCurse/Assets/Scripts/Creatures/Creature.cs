using Scripts.Component;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    public class Creature : MonoBehaviour, IEffectable
    {
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpspeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage;

        [Header("Chekers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] protected LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected bool IsGrounded;
        protected bool IsJumping;

        protected StatusEffectData _data;
        private float currentEffectTime = 0f;
        private float lastTickTime = 0f;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        protected static readonly int Heal = Animator.StringToHash("heal");
        protected static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);

            //Flip sprite
            UpdateSpriteDirection();
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                IsJumping = false;
            }

            if (isJumpPressing)
            {
                IsJumping = true;

                var isFalling = yVelocity <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && IsJumping)
            {
                yVelocity = Rigidbody.velocity.y * -1 ;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsGrounded)
            {
                yVelocity = _jumpspeed;
                _particles.Spawn("Jump");
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (Direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        protected virtual void TakeDammage()
        {
            IsJumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }

        public void OnDoAtack()
        {
           _attackRange.Check();
        }

        public void ApplyEffects(StatusEffectData _data)
        {
            this._data = _data;
        }

        public void RemoveEffects()
        {
            _data = null;
        }

        public void UseEffect()
        {
            currentEffectTime += Time.deltaTime;

            if (currentEffectTime >= _data.LifeTime) RemoveEffects();

            if (_data == null) return;

            var hp = GetComponent<HealtheComponent>();
            if (_data.DOTAmount != 0 && currentEffectTime > lastTickTime + _data.TickSpeed)
            {
                lastTickTime += _data.TickSpeed;
                hp.ModifyHealth(_data.DOTAmount);
            }
        }
    }
}
