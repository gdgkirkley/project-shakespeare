using System.Linq;
using Shakespeare.Quests;
using UnityEngine;

namespace Shakespeare.Dialogue
{
    [CreateAssetMenu(menuName = ("Shakespeare/Dialogue/Quest Inactive Condition"))]
    [System.Serializable]
    public class QuestConditionInactive : QuestCondition
    {
        public override bool CanUse(Conversation conversation)
        {
            // When quest is inactive return true
            // When quest is active return false
            bool active = quests.All(quest => quest != null && Journal.instance.IsActiveQuest(quest));
            bool completed = quests.All(quest => quest != null && Journal.instance.IsCompletedQuest(quest));

            return !active && !completed;
        }
    }
}