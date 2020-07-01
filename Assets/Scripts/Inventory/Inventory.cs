using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Inventories
{
    // TODO add saving
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int inventorySize = 16;

        InventoryItem[] slots;

        public event Action inventoryUpdated;

        private Pickup itemToPickup;

        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        public bool HasSpace(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        public int GetSize()
        {
            return slots.Length;
        }

        public void TriggerPickup(Pickup item)
        {
            GetComponent<Animator>().SetTrigger("lifting");
            itemToPickup = item;
        }

        public void AddPickupItem()
        {
            itemToPickup.PickupItem();
        }

        public void PickupDestroy()
        {
            itemToPickup.Destroy();
            itemToPickup = null;
        }

        public void ResetPickupAnimation()
        {
            GetComponent<Animator>().ResetTrigger("lifting");
        }

        public bool AddToFirstEmptySlot(InventoryItem item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;

            item.OnAdd();

            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }

            return true;
        }

        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot];
        }

        public void RemoveFromSlot(int slot)
        {
            slots[slot] = null;

            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        public bool AddItemToSlot(int slot, InventoryItem item)
        {
            if (slots[slot] != null)
            {
                return AddToFirstEmptySlot(item);
            }

            slots[slot] = item;

            item.OnAdd();

            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }

            return true;
        }


        private void Awake()
        {
            slots = new InventoryItem[inventorySize];

            // Add any default items here
        }

        private int FindSlot(InventoryItem item)
        {
            return FindEmptySlot();
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
