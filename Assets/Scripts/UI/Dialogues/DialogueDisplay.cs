using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shakespeare.Dialogue;

namespace Shakespeare.UI.Dialogue
{
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform root;
        [SerializeField] Text NPCTextField;
        [SerializeField] Transform responseHolder;
        [SerializeField] ResponseDisplay responsePrefab;

        private Speaker speaker;
        private ConversationNode lastNode;

        private void Start()
        {
            speaker = GameObject.FindGameObjectWithTag("Player").GetComponent<Speaker>();
            speaker.OnCurrentNodeChanged += UpdateDisplay;
            ActivateUI(false);
        }

        private void ActivateUI(bool activate)
        {
            root.gameObject.SetActive(activate);
            responseHolder.gameObject.SetActive(activate);
        }

        private void UpdateDisplay()
        {
            var node = speaker.currentNode;

            // Activate if the node is not null
            ActivateUI(node != null);

            if (node != lastNode)
            {
                SetNPCText(node);

                ClearResponseObjects();

                if (node != null)
                {
                    CreateResponsesForNode();
                }

                lastNode = node;
            }
        }

        private void SetNPCText(ConversationNode node)
        {
            NPCTextField.text = node != null ? node.text : "";
        }

        private void ClearResponseObjects()
        {
            foreach (Transform child in responseHolder)
            {
                child.GetComponent<ResponseDisplay>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
        }

        private void CreateResponsesForNode()
        {
            int numberOfResponses = 0;
            foreach (ConversationNode child in speaker.children)
            {
                if (!child.isResponse) continue;

                var responseObject = Instantiate(responsePrefab, responseHolder);
                responseObject.text = child.text;
                responseObject.onClick.AddListener(() =>
                {
                    speaker.ChooseResponse(child);
                });
                numberOfResponses++;
            }

            if (numberOfResponses == 0)
            {
                var responseObject = Instantiate(responsePrefab, responseHolder);
                responseObject.text = "...";
                responseObject.onClick.AddListener(() =>
                {
                    speaker.GetNextNode();
                });
            }
        }

    }
}
