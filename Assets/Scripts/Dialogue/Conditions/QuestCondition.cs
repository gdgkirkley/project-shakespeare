using System.Collections.Generic;
using Shakespeare.Quests;

namespace Shakespeare.Dialogue
{
    [System.Serializable]
    public abstract class QuestCondition : BaseCondition
    {
        public List<Quest> _quests = new List<Quest>();

        public IEnumerable<Quest> quests
        {
            get
            {
                foreach (Quest quest in _quests)
                {
                    yield return quest;
                }
            }
        }
    }
}