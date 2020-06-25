using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shakespeare.Dialogue
{
    public struct VoiceEventMapping
    {
        public string name;
        public UnityEvent callback;
    }
}
