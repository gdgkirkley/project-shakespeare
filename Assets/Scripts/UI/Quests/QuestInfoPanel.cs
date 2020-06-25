using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Quests;
using UnityEngine.UI;

namespace Shakespeare.UI.Quests
{
    public class QuestInfoPanel : MonoBehaviour
    {
        [SerializeField] Text questTitle;

        TaskSlot[] slots;

        public void UpdateUI(Quest quest)
        {
            slots = GetComponentsInChildren<TaskSlot>();
            Task[] tasks = quest.GetTasks();

            questTitle.text = quest.GetQuestAsString();

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < tasks.Length)
                {
                    slots[i].AddTask(tasks[i]);
                }
                else
                {
                    slots[i].RemoveTask();
                }
            }
        }
    }
}