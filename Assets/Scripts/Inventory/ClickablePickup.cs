using System.Collections;
using System.Collections.Generic;
using Shakespeare.Control;
using UnityEngine;

namespace Shakespeare.Inventories
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                pickup.PickupItem();
            }
            return true;
        }
    }
}