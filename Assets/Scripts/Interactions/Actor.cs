using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public class Actor : MonoBehaviour, IAction
    {
        Animator animator;

        string currentTrigger;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PetTallAnimal(Creature animal)
        {
            transform.LookAt(animal.transform);
            currentTrigger = "pettingTall";
            animator.SetTrigger(currentTrigger);
            StartAction();
        }

        public void CancelPetTallAnimal()
        {
            animator.ResetTrigger("pettingTall");
        }

        public void PetSmallAnimal(Creature animal)
        {
            transform.LookAt(animal.transform);
            currentTrigger = "pettingSmall";
            animator.SetTrigger(currentTrigger);
            StartAction();
        }

        public void CancelPetSmallAnimal()
        {
            animator.ResetTrigger("pettingSmall");
        }

        public void Cancel()
        {
            animator.ResetTrigger(currentTrigger);
        }

        private void StartAction()
        {
            GetComponent<ActionScheduler>().StartAction(this);
        }
    }
}
