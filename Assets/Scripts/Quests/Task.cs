using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Quests
{
    [CreateAssetMenu(menuName = "Shakespeare/Task")]
    public class Task : ScriptableObject
    {
        public delegate void ProgressChanged(Task task);
        public event ProgressChanged OnStatusChange;

        [SerializeField] string uniqueId;
        [TextArea]
        public string description;

        public TaskStatus status = TaskStatus.NotStarted;

        // Ie. Cut 0 / 15 logs
        [Tooltip("Use {0}, {1}, {2} to insert progress")]
        [SerializeField] string statusMessage;

        [NonSerialized]
        public float progress = 0f;

        [SerializeField] float maxProgress = 1f;

        [SerializeField] TaskRequirement requirement = TaskRequirement.Required;

        public bool isCompleted
        {
            get { return status == TaskStatus.Completed; }
        }

        public bool AddProgress(float amount)
        {
            SetProgress(progress + amount);
            return true;
        }

        public bool SetProgress(float amount)
        {
            float before = progress;

            progress = amount;

            if (before == 0f)
            {
                status = TaskStatus.InProgress;
            }

            if (IsProgressSufficientToComplete())
            {
                Complete();
            }

            return true;
        }

        public bool IsProgressSufficientToComplete()
        {
            if (progress >= maxProgress)
            {
                return true;
            }
            return false;
        }

        public bool Complete()
        {
            status = TaskStatus.Completed;
            OnStatusChange(this);
            return isCompleted;
        }

        public void Fail()
        {
            status = TaskStatus.Failed;
            OnStatusChange(this);
        }

        public string GetStatusMessage()
        {
            return string.Format(statusMessage, progress, maxProgress);
        }
    }
}
