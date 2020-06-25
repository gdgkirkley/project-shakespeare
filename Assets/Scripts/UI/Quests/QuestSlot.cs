using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shakespeare.Quests;

namespace Shakespeare.UI.Quests
{
    public class QuestSlot : MonoBehaviour
    {
        [SerializeField] GameObject questButton;
        [SerializeField] GameObject infoPanel;

        [SerializeField] Image icon;
        [SerializeField] Sprite completeIcon;

        [SerializeField] Text questName;

        [SerializeField] Color activeColor;
        [SerializeField] Color completeColor;

        Quest quest;
        Button button;

        private void Awake()
        {
            questButton.SetActive(false);
            button = questButton.GetComponent<Button>();
            button.onClick.AddListener(ShowQuestInfo);
        }

        private void Update()
        {
        }

        // Refactor this. It's not great to have to manually add to each slot
        public void ShowQuestInfo()
        {
            infoPanel.SetActive(true);
            infoPanel.GetComponent<QuestInfoPanel>().UpdateUI(quest);
        }

        public bool HasQuest()
        {
            return quest != null;
        }

        public void AddQuest(Quest newQuest)
        {
            questButton.SetActive(true);
            button.interactable = true;
            quest = newQuest;
            icon.color = activeColor;
            questName.text = quest.GetQuestAsString();
        }

        public void RemoveQuest()
        {
            quest = null;
            icon.color = new Color(0, 0, 79, 100);
            questName.text = "";
            questButton.SetActive(false);
        }

        public void AddCompleteQuest(Quest newQuest)
        {
            questButton.SetActive(true);
            quest = newQuest;
            icon.sprite = completeIcon;
            icon.color = completeColor;
            questName.text = quest.GetQuestAsString();
        }
    }

}