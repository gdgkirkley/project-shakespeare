using System.Linq;
using Shakespeare.Quests;
using UnityEngine;

namespace Shakespeare.Dialogue
{
    [CreateAssetMenu(menuName = ("Shakespeare/Dialogue/Quest Status Condition"))]
    [System.Serializable]
    public class QuestConditionStatus : QuestCondition
    {
        public QuestStatus requiredQuestStatus = QuestStatus.InProgress;

        public override bool CanUse(Conversation conversation)
        {
            return quests.All(quest =>
            {
                return quest != null && quest.status == requiredQuestStatus;
            });
        }
    }
}