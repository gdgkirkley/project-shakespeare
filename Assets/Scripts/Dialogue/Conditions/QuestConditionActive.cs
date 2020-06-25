using System.Linq;
using Shakespeare.Quests;
using UnityEngine;

namespace Shakespeare.Dialogue
{
    [CreateAssetMenu(menuName = ("Shakespeare/Dialogue/Quest Active Condition"))]
    [System.Serializable]
    public class QuestConditionActive : QuestCondition
    {
        public override bool CanUse(Conversation conversation)
        {
            return quests.All(quest => quest != null && Journal.instance.IsActiveQuest(quest));
        }
    }
}