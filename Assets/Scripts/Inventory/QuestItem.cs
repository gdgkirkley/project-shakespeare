using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Quests;

namespace Shakespeare.Inventories
{
    [CreateAssetMenu(menuName = "Shakespeare/Inventory/Quest Item")]
    public class QuestItem : InventoryItem
    {
        [SerializeField] Quest quest;
        [SerializeField] Task task;
        [SerializeField] float progressToAdd;

        public override void OnAdd()
        {
            task.AddProgress(progressToAdd);
        }
    }
}