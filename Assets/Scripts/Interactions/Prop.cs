using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public abstract class Prop : MonoBehaviour, IInteractable
    {
        public void OnInteract(Actor actor)
        {
            throw new System.NotImplementedException();
        }
    }
}