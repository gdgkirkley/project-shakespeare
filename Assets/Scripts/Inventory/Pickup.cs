using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Inventories
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] float pickupXRotation;
        [SerializeField] float pickupYRotation;
        [SerializeField] float pickupZRotation;

        InventoryItem item;

        Inventory inventory;

        Transform holdIn;
        bool pickingUp = false;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            holdIn = player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
            inventory = player.GetComponent<Inventory>();
        }

        private void Update()
        {
            if (!pickingUp) return;
            transform.position = new Vector3(holdIn.position.x, holdIn.position.y + 0.12f, holdIn.position.z - 0.123f);
            transform.rotation = new Quaternion(pickupXRotation, pickupYRotation, pickupZRotation, holdIn.rotation.w);
        }

        public void Setup(InventoryItem item)
        {
            this.item = item;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item);

            if (foundSlot)
            {
                pickingUp = true;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpace(item);
        }
    }
}