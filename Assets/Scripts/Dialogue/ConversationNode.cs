using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Dialogue
{
    [System.Serializable]
    public class ConversationNode
    {
        public string UUID;
        public Vector2 position;
        public bool isResponse;
        public string text;
        public List<string> children = new List<string>();
        public DialogueEvent actionToTrigger = DialogueEvent.None;
        public BaseCondition condition;
    }
    /*
    Can we create multiple types of nodes?
    One of these nodes could sit at the root of the conversation
    and it could direct to the relevant node based on the conditions.
    */
}
