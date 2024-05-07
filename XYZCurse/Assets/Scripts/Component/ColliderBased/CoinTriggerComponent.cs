using UnityEngine.Events;
using UnityEngine;
using Scripts.Component;
using Assets.Scripts.Creatures;

namespace Assets.Scripts.Component
{
    /*public class CoinTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;
        [SerializeField] private CoinType coinType;
        [SerializeField] private int amount;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if(collision.gameObject.CompareTag(_tag))
            {
                var wallet = collision.gameObject.GetComponent<Hero>().GetWallet();
                wallet.AddCoins(coinType, amount);

                Debug.Log(message: $"wallet balance: {wallet.GetCoins(coinType)} {coinType}");
                _action?.Invoke();
            }
        }
    }*/
}

