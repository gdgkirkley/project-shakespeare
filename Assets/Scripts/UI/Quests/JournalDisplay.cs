using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Quests;

namespace Shakespeare.UI.Quests
{
    public class JournalDisplay : MonoBehaviour
    {
        public GameObject journalUI;
        public GameObject infoPanelUI;

        Journal journal;
        QuestSlot[] slots;

        // Start is called before the first frame update
        void Start()
        {
            journal = Journal.instance;
            journal.QuestChanged += UpdateUI;
        }

        // Update is called once per frame
        void Update()
        {
            if (journalUI.activeSelf)
            {
                UpdateUI();
            }
        }

        void UpdateUI()
        {
            slots = GetComponentsInChildren<QuestSlot>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < journal.activeQuests.Count)
                {
                    slots[i].AddQuest(journal.activeQuests[i]);
                }
                else if (i < journal.completedQuests.Count)
                {
                    slots[i].AddCompleteQuest(journal.completedQuests[i]);
                }
                else
                {
                    slots[i].RemoveQuest();
                }
            }
        }
    }
}
