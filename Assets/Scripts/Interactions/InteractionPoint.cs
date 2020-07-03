using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public class InteractionPoint : MonoBehaviour
    {
        IInteractable parent;

        private void Awake()
        {
            parent = GetComponentInParent<IInteractable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collide");
                Actor actor = other.GetComponent<Actor>();
                parent.OnInteract(actor);
            }
        }
    }
}