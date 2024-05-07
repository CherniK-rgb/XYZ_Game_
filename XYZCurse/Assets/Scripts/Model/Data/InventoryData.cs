using System;
using System.Collections.Generic;
using Model.Definition;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged OnChanged;
        
        public void Add(string id, int amount)
        {
            if (amount <= 0) return;
            
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if (item == null)
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            
            item.Amount += amount;
            OnChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int amount)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if (item == null) return;
            
            item.Amount -= amount;

            if (item.Amount <= 0)
                _inventory.Remove(item);
            
            OnChanged?.Invoke(id, Count(id));
        }

        public InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id) return itemData;
            }

            return null;
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var item in _inventory)
            {
                if (item.Id == id)
                    count += item.Amount;
            }
            return count;
        }
    }

    [Serializable]
    public class InventoryItemData
    {
       [InventoryId] public string Id;
        public int Amount;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}