using System.Collections;
using System.Collections.Generic;
using Shakespeare.Inventories;
using UnityEngine;

namespace Shakespeare.UI.Inventories
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlotUI InventoryItemPrefab = null;

        Inventory inventory;

        private void Awake()
        {
            inventory = Inventory.GetPlayerInventory();
            inventory.inventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < inventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, transform);
                itemUI.Setup(inventory, i);
            }
        }
    }
}