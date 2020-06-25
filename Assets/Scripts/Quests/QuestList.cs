using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests
{
    public class QuestList
    {
        [SerializeField] Quest[] quests;

        public Quest GetQuestById(string id)
        {
            foreach (Quest quest in quests)
            {
                if (quest.uniqueId == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public Quest GetQuestByIndex(int index)
        {
            return quests[index];
        }
    }

}