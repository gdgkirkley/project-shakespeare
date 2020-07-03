using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shakespeare.Control;
using PolyPerfect;
using UnityEngine.AI;

namespace Shakespeare.Interactions
{
    public abstract class Creature : MonoBehaviour, IInteractable, IRaycastable
    {
        public Transform interactionPoint;
        public Transform lookAtPoint;
        public Common_WanderScript wanderScript;

        protected Transform walkTo;
        protected NavMeshAgent agent;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            walkTo = player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
            wanderScript = GetComponent<Common_WanderScript>();
            agent = GetComponent<NavMeshAgent>();
        }

        public abstract void OnInteract(Actor actor);

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                interactionPoint.gameObject.SetActive(true);
                callingController.GetComponent<Mover>().StartMoveAction(interactionPoint.position, 1f);
            }
            return true;
        }
    }
}