using UnityEngine;
using UnityEngine.InputSystem;

namespace  Assets.Scripts.Creatures
{
    public class HeroIputReader : MonoBehaviour
    {
        private Hero _hero;
        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Attack();
            }
        }

        public void OnThrrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Throw();
            }
        }

    }
}
    
