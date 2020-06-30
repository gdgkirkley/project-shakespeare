using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Quests;

namespace Shakespeare.Inventories
{
    [CreateAssetMenu(menuName = "Shakespeare/Inventory/Quest Item")]
    public class QuestItem : InventoryItem
    {
        [SerializeField] Task task;
        [SerializeField] float progressToAdd;

        public override void OnAdd()
        {
            Debug.Log("Calling");
            task.AddProgress(progressToAdd);
        }
    }
}