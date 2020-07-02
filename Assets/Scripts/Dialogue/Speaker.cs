using System;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Control;

namespace Shakespeare.Dialogue
{
    public class Speaker : MonoBehaviour, IAction, IConversable
    {
        [SerializeField] ConversationSource source;
        [SerializeField] CameraDirector director;
        public ConversationNode currentNode { get; private set; }

        public event Action OnCurrentNodeChanged;

        private bool isTalking = false;

        private void Update()
        {
            if (source == null || isTalking) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(source.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                ConversationBehaviour();
            }
        }

        public void SetActiveConversation(ConversationSource source)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.source = source;
        }

        public void ConversationBehaviour()
        {
            transform.LookAt(source.transform);

            if (this.source != null)
            {
                isTalking = true;
                Conversation conversation = this.source.GetConversation();
                currentNode = conversation.GetRootNode();
                director.DialogueInit(transform, source.transform);
                director.OnSourceDialogue();
            }
            else
            {
                currentNode = null;
                isTalking = false;
            }

            OnCurrentNodeChanged();
        }

        public void Cancel()
        {
            CancelConversation();
            GetComponent<Mover>().Cancel();
        }

        public IEnumerable<ConversationNode> children
        {
            get
            {
                foreach (var child in currentNode.children)
                {
                    yield return source.GetConversation().GetNodeByUUID(child);
                }
            }
        }

        public void ChooseResponse(ConversationNode childNode)
        {
            if (childNode.actionToTrigger != DialogueEvent.None)
            {
                source.TriggerEventForAction(childNode.actionToTrigger);
            }
            if (childNode.children.Count == 0)
            {
                CancelConversation();
            }
            else
            {
                currentNode = source.GetConversation().GetNodeByUUID(childNode.children[0]);
                // director.OnPlayerDialogue();
            }

            OnCurrentNodeChanged();
        }

        public void GetNextNode()
        {
            if (currentNode.children.Count == 0)
            {
                CancelConversation();
            }
            else
            {
                ConversationNode newNode = GetUsableChild();

                if (newNode != null)
                {
                    // director.OnSourceDialogue();
                    currentNode = newNode;
                }
                else
                {
                    CancelConversation();
                }
            }

            OnCurrentNodeChanged();
        }

        private void CancelConversation()
        {
            if (source != null)
            {
                source.isSpeaking = false;
            }
            currentNode = null;
            source = null;
            isTalking = false;
            director.EndDialogue();
        }

        private ConversationNode GetUsableChild()
        {
            Conversation convo = source.GetConversation();

            foreach (string node in currentNode.children)
            {
                ConversationNode testNode = convo.GetNodeByUUID(node);

                if (testNode.conditions == null || testNode.conditions.Length == 0)
                {
                    TriggerAction(testNode);
                    return testNode;
                }
                else
                {
                    bool canUse = true;
                    foreach (BaseCondition condition in testNode.conditions)
                    {
                        // All conditions must be true, so if any return false, we skip all the rest
                        if (canUse == false) continue;
                        if (condition == null) continue;
                        canUse = condition.CanUse(convo);
                    }

                    if (canUse)
                    {
                        TriggerAction(testNode);
                        return testNode;
                    }
                }
            }

            return null;
        }

        private void TriggerAction(ConversationNode node)
        {

            if (node.actionToTrigger != DialogueEvent.None)
            {
                source.TriggerEventForAction(node.actionToTrigger);
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, source.transform.position) < 1f;
        }

        public string GetName()
        {
            return "Hero";
        }
    }
}
