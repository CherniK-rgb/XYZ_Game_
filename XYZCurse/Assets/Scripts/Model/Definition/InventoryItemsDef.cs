using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDef : ScriptableObject
        // ScriptableObject - special object which distributed with unity. He can create instances subclass this class how asset
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id) return itemDef;
            }

            return default;
        }

#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string  _id;
        public string Id => _id;    
        
        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
    
}