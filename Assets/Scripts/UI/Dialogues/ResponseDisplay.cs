using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shakespeare.UI.Dialogue
{
    public class ResponseDisplay : MonoBehaviour
    {
        [SerializeField] Text displayText;
        [SerializeField] Button button;

        public string text
        {
            set
            {
                displayText.text = value;
            }
        }

        public Button.ButtonClickedEvent onClick => button.onClick;
    }
}