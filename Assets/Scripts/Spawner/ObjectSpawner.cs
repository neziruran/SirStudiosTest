using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using ScriptableContainers.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnContainer spawnContainer;
        [SerializeField] private List<ACrystal> objectPool = new List<ACrystal>(15);

        internal int CurrentObjects = 0;
        private const int Redline = 2;
        
        private void Awake()
        {
            // Register to Spawn Container
            spawnContainer.ObjectSpawner = this;
            for (int i = 0; i < 5; i++)
            {
                PopulatePool();
            }
        }

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                StartSpawnProcess();
            }
        }
        internal void StartSpawnProcess()
        {
            if (CheckSpawn())
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            if (objectPool.Count <= Redline)
            {
                PopulatePool();
                GetObjectFromPool();
            }
            else
            {
                GetObjectFromPool();
            }
        }

        private void GetObjectFromPool()
        {
            StartCoroutine(SpawnDelay());
        }

        private IEnumerator SpawnDelay()
        {
            yield return new WaitForSeconds(Random.Range(1,spawnContainer.spawnRate));
            
            var spawnedCrystal = objectPool[0];
            Vector3 randomPosition = GetRandomPosition();

            Collider[] overlappingColliders = new Collider[4];

            var detectedCounts = CheckOverlapSphere(randomPosition, overlappingColliders);
            

            while (detectedCounts > 0)
            {
                Vector3 pos = GetRandomPosition();
                detectedCounts = CheckOverlapSphere(pos,overlappingColliders);
            }

            spawnedCrystal.transform.position = randomPosition;
            spawnedCrystal.gameObject.SetActive(true);

    
            objectPool.Remove(spawnedCrystal);
            CurrentObjects++;
            

        }

        private int CheckOverlapSphere(Vector3 randomPosition,Collider[] colliders)
        {
            return Physics.OverlapSphereNonAlloc(randomPosition, 2, colliders, LayerMask.GetMask("Collectable"));
        }

        private Vector3 GetRandomPosition()
        {
           return new Vector3(Random.Range(spawnContainer.min_X_Range, spawnContainer.max_X_Range), .5f,Random.Range(spawnContainer.min_Z_Range, spawnContainer.max_Z_Range));
        }

        private void PopulatePool()
        {
            ACrystal poolObject = Instantiate(spawnContainer.spawnPrefab, new Vector3(0, 0, -5f), transform.rotation);
            objectPool.Add(poolObject);
            poolObject.gameObject.SetActive(false); 
            
        }

        private bool CheckSpawn()
        {
            if (CurrentObjects <= 5)
                return true;
            else
            {
                return false;
            }
        }

        public void SendToPool(ACrystal crystal)
        {
            objectPool.Add(crystal);
            CurrentObjects--;
        }
    }
}