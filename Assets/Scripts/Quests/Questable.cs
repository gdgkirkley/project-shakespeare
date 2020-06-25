using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests
{
    public interface Questable
    {
        // Set quest objectives and init
        void OnStart();
        // Add progress to quest
        void OnProgress();
        // Finish quest
        void OnComplete();

        // Extend from Quest and implement Questable
    }
}