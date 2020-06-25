using System;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Control;

namespace Shakespeare.Dialogue
{
    public class Speaker : MonoBehaviour, IAction, IConversable
    {
        [SerializeField] ConversationSource source;
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
                GetComponent<CameraController>().SetConversationMode();
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
                currentNode = null;
                source = null;
                isTalking = false;
            }
            else
            {
                currentNode = source.GetConversation().GetNodeByUUID(childNode.children[0]);
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
            currentNode = null;
            source = null;
            isTalking = false;
        }

        private ConversationNode GetUsableChild()
        {
            Conversation convo = source.GetConversation();

            foreach (string node in currentNode.children)
            {
                ConversationNode testNode = convo.GetNodeByUUID(node);

                if (testNode.condition == null || testNode.condition.CanUse(convo))
                {
                    return testNode;
                }
            }

            return null;
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
