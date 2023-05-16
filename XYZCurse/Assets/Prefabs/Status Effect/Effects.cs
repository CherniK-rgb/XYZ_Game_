using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Prefabs.Status_Effect
{
    class Effects : MonoBehaviour
    {
        [SerializeField] public StatusEffectData _data;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var effectable = collision.GetComponent<IEffectable>();
            if (effectable != null)
            {
                effectable.ApplyEffects(_data);
            }
        }
    }
}
