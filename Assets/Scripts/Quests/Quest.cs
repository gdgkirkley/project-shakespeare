using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests
{
    [Serializable]
    [CreateAssetMenu(menuName = ("Shakespeare/Quest"))]
    public class Quest : ScriptableObject
    {
        [SerializeField] string uniqueName;
        [SerializeField] string displayName;

        [TextArea]
        public string description;

        [NonSerialized]
        public QuestStatus status;

        [SerializeField] int rewardXP;
        [SerializeField] int rewardCoin;

        [SerializeField] bool autoComplete = true;
        [SerializeField] Task[] tasks;

        Journal journal;

        public int RewardCoin
        {
            get
            {
                return rewardCoin;
            }
        }

        public string GetQuestAsString()
        {
            return displayName;
        }

        public string uniqueId
        {
            get
            {
                return uniqueName;
            }
        }

        public Task[] GetTasks()
        {
            return tasks;
        }

        public void Create()
        {
            Journal.instance.AddQuest(this);

            foreach (Task task in tasks)
            {
                Debug.Log("Initializing task " + task.name);
                task.OnStatusChange += NotifyTaskStatusChange;
            }
        }

        private void NotifyTaskStatusChange(Task task)
        {
            switch (task.status)
            {
                case TaskStatus.Completed:
                    if (autoComplete && AreAllTasksComplete())
                    {
                        CompleteQuest();
                    }
                    break;
                case TaskStatus.Failed:
                    Debug.Log("Failure!");
                    break;
            }
        }

        private bool AreAllTasksComplete()
        {
            bool complete = false;

            foreach (Task task in tasks)
            {
                complete = task.isCompleted;
            }

            return complete;
        }

        public void CompleteQuest()
        {
            Debug.Log("Quest complete!");
            status = QuestStatus.Complete;
            Journal.instance.CompleteQuest(this);
        }
    }

}