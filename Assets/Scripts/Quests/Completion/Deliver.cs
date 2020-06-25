using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Dialogue;

namespace Shakespeare.Quests
{
    public class Deliver : Quest, Questable
    {
        [SerializeField] GameObject itemToDeliver;
        [SerializeField] int numberToDeliver;
        [SerializeField] ConversationSource source;

        public void OnComplete()
        {
            throw new System.NotImplementedException();
        }

        public void OnProgress()
        {
            throw new System.NotImplementedException();
        }

        public void OnStart()
        {
            throw new System.NotImplementedException();
        }

    }
}

// Add to QuestList
// Trigger from Journal