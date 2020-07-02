using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Control;
using PolyPerfect;

namespace Shakespeare.Interactions
{
    public abstract class Creature : MonoBehaviour, IInteractable, IRaycastable
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public abstract void OnInteract(Actor actor);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collide");
                Actor actor = other.GetComponent<Actor>();
                OnInteract(actor);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Mover>().StartMoveAction(transform.position, 1f);
            }
            return true;
        }
    }
}