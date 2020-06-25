using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Control;
using Shakespeare.Quests;

namespace Shakespeare.Dialogue
{
    public class ConversationSource : MonoBehaviour, IRaycastable, IConversable
    {
        const float DIALOGUE_LIFETIME = 5.0f;

        [SerializeField] string characterName;
        [SerializeField] Conversation conversation;
        [SerializeField] [Tooltip("Optional")] Quest quest;
        [Space(15)]
        [SerializeField] Transform canvas;
        [SerializeField] GameObject speechBubblePrefab;
        public List<VoiceEventMapping> dialogueEventBindings;

        Speaker speaker;
        Journal journal;

        public Conversation GetConversation()
        {
            return conversation;
        }

        public void TriggerEventForAction(DialogueEvent action)
        {
            switch (action)
            {
                case DialogueEvent.StartQuest:
                    Debug.Log("Start Quest");
                    TriggerQuestIfAny();
                    break;
                case DialogueEvent.CompleteQuest:
                    CompleteQuestIfAny();
                    break;
            }
        }

        void Start()
        {
            Instantiate(speechBubblePrefab, canvas);
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            speaker = p.GetComponent<Speaker>();
            journal = p.GetComponent<Journal>();
        }

        public void VoiceClicked()
        {
            ShowDialogue();
        }

        public void TriggerQuestIfAny()
        {
            if (quest == null) return;
            quest.Create();
        }

        public void CompleteQuestIfAny()
        {
            if (quest == null) return;
            quest.CompleteQuest();
        }

        private void ShowDialogue()
        {
            speaker.SetActiveConversation(this);
        }

        public bool HandleRaycast(PlayerController playerControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowDialogue();
            }
            return true;
        }

        public string GetName()
        {
            return characterName;
        }
    }
}
