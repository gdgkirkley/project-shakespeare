using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests
{
    public class TaskProgressTrigger : MonoBehaviour
    {
        [SerializeField] Task task;
        [SerializeField] float progressToAdd;

        private void OnTriggerEnter(Collider other)
        {
            task.AddProgress(progressToAdd);
        }
    }
}
