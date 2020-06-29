using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Inventories
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] InventoryItem item = null;

        private void Awake()
        {
            SpawnPickup();
        }

        public Pickup GetPickup()
        {
            return GetComponentInChildren<Pickup>();
        }

        public bool IsCollected()
        {
            return GetPickup() == null;
        }

        private void SpawnPickup()
        {
            var spawnedPickup = item.SpawnPickup(transform.position);
            spawnedPickup.transform.SetParent(transform);
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }


    }
}