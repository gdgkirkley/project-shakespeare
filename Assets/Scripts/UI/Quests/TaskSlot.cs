using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shakespeare.Quests;

namespace Shakespeare.UI.Quests
{
    public class TaskSlot : MonoBehaviour
    {
        [SerializeField] GameObject taskUI;
        [SerializeField] Text text;

        public void AddTask(Task newTask)
        {
            taskUI.SetActive(true);
            text.text = newTask.GetStatusMessage();
        }

        public void RemoveTask()
        {
            taskUI.SetActive(false);
            text.text = "";
        }
    }

}