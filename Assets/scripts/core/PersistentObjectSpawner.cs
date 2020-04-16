using UnityEngine;

namespace RPG.Core
{
    /*WORKS AS A SINGELTON PATTERN*/
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectSpawner;
        static bool hasSpawned = false;

        private void Awake()
        {
            if(hasSpawned) return;
            
            spawnPersistentObject();
            hasSpawned = true;
        }

        private void spawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectSpawner);
            DontDestroyOnLoad(persistentObject);
        }

    }
}