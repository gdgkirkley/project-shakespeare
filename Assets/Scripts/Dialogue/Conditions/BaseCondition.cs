using UnityEngine;

namespace Shakespeare.Dialogue
{
    [System.Serializable]
    public abstract class BaseCondition : ScriptableObject, ICondition
    {
        public string uniqueId;
        public string description;
        public Vector2 position;
        public bool canViewEndNode = false;

        public bool CanViewEndNode(Conversation conversation)
        {
            return CanUse(conversation) || canViewEndNode;
        }

        public abstract bool CanUse(Conversation conversation);
    }
}