using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Inventories
{
    [CreateAssetMenu(menuName = ("Shakespeare/Inventory/Item"))]
    public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string itemId = null;
        [SerializeField] string displayName = null;
        [SerializeField] [TextArea] string description = null;
        [SerializeField] Sprite icon = null;
        [SerializeField] Pickup pickup;
        [SerializeField] bool stackable = false;

        static Dictionary<string, InventoryItem> itemLookupCache;

        public static InventoryItem GetFromID(string itemId)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");

                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemId))
                    {
                        continue;
                    }

                    itemLookupCache[item.itemId] = item;
                }
            }

            if (itemId == null || !itemLookupCache.ContainsKey(itemId)) return null;

            return itemLookupCache[itemId];
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemId()
        {
            return itemId;
        }

        public bool IsStackable()
        {
            return stackable;
        }

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        public Pickup SpawnPickup(Vector3 position)
        {
            var pickup = Instantiate(this.pickup);

            pickup.transform.position = position;
            pickup.Setup(this);

            return pickup;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                itemId = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // Required
        }
    }
}