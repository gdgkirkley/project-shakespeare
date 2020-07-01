using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shakespeare.Quests
{
    public class Journal : MonoBehaviour
    {

        #region Singleton
        public static Journal instance;
        private void Awake()
        {
            Debug.Log("Creating journal");
            instance = this;
        }
        #endregion

        public delegate void OnQuestChanged();
        public event OnQuestChanged QuestChanged;

        QuestList questList = new QuestList();

        public List<Quest> activeQuests = new List<Quest>();
        public List<Quest> completedQuests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            Debug.Log("Adding" + quest.GetQuestAsString());
            activeQuests.Add(quest);
            quest.status = QuestStatus.InProgress;
            QuestChanged();
        }

        public void CompleteQuest(Quest quest)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            QuestChanged();
        }

        public bool IsActiveQuest(Quest quest)
        {
            return activeQuests.Contains(quest);
        }

        public bool IsCompletedQuest(Quest quest)
        {
            return completedQuests.Contains(quest);
        }

        public QuestStatus GetQuestStatus(Quest quest)
        {
            Quest activeQuest = activeQuests.Find(q => q.name == quest.name);

            if (activeQuest != null)
            {
                return activeQuest.status;
            }

            Quest completeQuest = completedQuests.Find(q => q.name == quest.name);

            if (completeQuest != null)
            {
                return completeQuest.status;
            }

            return QuestStatus.None;
        }

        public Quest GetQuestById(string questId)
        {
            return questList.GetQuestById(questId);
        }
    }
}
