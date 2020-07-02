using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public interface IInteractable
    {
        void OnInteract(Actor actor);
    }
}