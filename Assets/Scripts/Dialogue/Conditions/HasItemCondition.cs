using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Inventories;

namespace Shakespeare.Dialogue
{
    [CreateAssetMenu(menuName = ("Shakespeare/Dialogue/Has Item Condition"))]
    [System.Serializable]
    public class HasItemCondition : BaseCondition
    {
        [SerializeField] InventoryItem item;
        [SerializeField] bool hasItem = true;

        public override bool CanUse(Conversation conversation)
        {
            Inventory inventory = Inventory.GetPlayerInventory();

            return inventory.HasItem(item) == hasItem;
        }
    }
}
