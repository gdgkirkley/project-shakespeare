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
        [SerializeField] float turnSpeed;
        public List<VoiceEventMapping> dialogueEventBindings;

        private bool speaking;

        public bool isSpeaking
        {
            get
            {
                return speaking;
            }
            set
            {
                speaking = value;
            }
        }

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
            isSpeaking = true;
            LookAtSpeaker();
            speaker.SetActiveConversation(this);
        }

        private void LookAtSpeaker()
        {
            float time = 1f;
            Vector3 lookAt = speaker.transform.position;
            lookAt.y = transform.position.y;

            StartCoroutine(LookAtSmoothly(transform, lookAt, time));
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

        private IEnumerator LookAtSmoothly(Transform objectToMove, Vector3 worldPosition, float duration)
        {
            Quaternion currentRot = objectToMove.rotation;
            Quaternion newRot = Quaternion.LookRotation(worldPosition -
                objectToMove.position, objectToMove.TransformDirection(Vector3.up));

            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                objectToMove.rotation =
                    Quaternion.Lerp(currentRot, newRot, counter / duration);
                yield return null;
            }
        }
    }
}
