using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool spawned = false;

        private void Awake()
        {
            if (spawned) return;

            SpawnPersistentObjects();

            spawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}