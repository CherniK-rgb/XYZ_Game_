using UnityEngine;
using UnityEngine.Events;

namespace Script.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private Transform _positionToTeleport;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                collision.gameObject.transform.position = _positionToTeleport.position;
            }
        }
    }
}

