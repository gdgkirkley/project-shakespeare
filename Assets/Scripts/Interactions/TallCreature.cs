﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Interactions
{
    public class TallCreature : Creature, IInteractable
    {
        public override void OnInteract(Actor actor)
        {
            actor.PetTallAnimal(this);
        }
    }
}