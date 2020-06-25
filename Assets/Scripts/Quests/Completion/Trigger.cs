using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests.Completion
{

    public class Trigger : QuestCompletion
    {
        private void OnTriggerEnter(Collider other)
        {
            CompleteQuest();
        }
    }

}