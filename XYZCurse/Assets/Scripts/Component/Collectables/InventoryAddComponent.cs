using Assets.Scripts.Creatures;
using Model.Definition;
using UnityEngine;

namespace Component.Collectables
{
    public class InventoryAddComponent: MonoBehaviour
    {
        [SerializeField] private int _count;
        [InventoryId][SerializeField] private string _id;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
                hero.AddInventory(_id, _count);
        }
        
    }

}