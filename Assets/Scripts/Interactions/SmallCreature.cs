using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public class SmallCreature : Creature
    {
        public override void OnInteract(Actor actor)
        {
            actor.PetSmallAnimal(this);
        }
    }
}