using System;
using Abstract;
using Player;
using Spawner;
using TMPro;
using UnityEngine;
using UnityEditor;

namespace Abstract
{
    public abstract class ACrystal : MonoBehaviour, ICollectable
    {
        public bool HasCollected
        {
            set
            {
                gameObject.SetActive(!value);
            }
        }

        private const int PointAmount = 10;

        public virtual void OnCollected(ref int currentScore, ObjectSpawner spawner)
        {
            spawner.StartSpawnProcess();
            currentScore += PointAmount;
        }
        
    }
}