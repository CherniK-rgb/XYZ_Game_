using Scripts;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if(hero != null)
            {
                hero.ArmHero();
            }
        }
    } 
}
