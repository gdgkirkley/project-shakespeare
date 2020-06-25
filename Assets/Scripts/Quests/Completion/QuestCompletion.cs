using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests.Completion
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] protected string questIdToComplete;

        Quest questToComplete;

        Journal journal;

        private void Start()
        {
            journal = FindObjectOfType<Journal>();
            questToComplete = journal.GetQuestById(questIdToComplete);
        }

        protected bool IsActive()
        {
            return journal.IsActiveQuest(questToComplete);
        }

        protected void CompleteQuest()
        {
            journal.CompleteQuest(questToComplete);
            EarnReward();
        }

        private void EarnReward()
        {
            Debug.Log("Earned " + questToComplete.RewardCoin + " coins");
        }
    }

}