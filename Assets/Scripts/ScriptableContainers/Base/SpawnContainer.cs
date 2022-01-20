using Abstract;
using Player;
using UnityEngine;
using Spawner;

namespace ScriptableContainers.Base
{
    [CreateAssetMenu (menuName ="ScriptableContainers / Spawn Container")]

    public class SpawnContainer : ScriptableObject
    {
        public ACrystal spawnPrefab; 
        public float spawnRate;
        public float max_X_Range;
        public float min_X_Range;
        
        public float max_Z_Range;
        public float min_Z_Range;
        public ObjectSpawner ObjectSpawner { get; set; }
        public PlayerController PlayerController { get; set; }

    }
}
