using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public class SmallCreature : Creature
    {
        public override void OnInteract(Actor actor)
        {
            wanderScript.MovementState(walkTo.position);
            actor.PetSmallAnimal(this, lookAtPoint);
            interactionPoint.gameObject.SetActive(false);
        }
    }
}