using System;
using Abstract;
using Collectable;
using JetBrains.Annotations;
using Player;
using UnityEditor;
using ScriptableContainers.Base;
using Spawner;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerController),true),CanEditMultipleObjects]
public class TestEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerController playerController = (PlayerController) target;
        if (GUILayout.Button("Collect"))
        {
            playerController.TriggerTest();
        }
    }
}
#endif

namespace Player
{
    public class PlayerController : MonoBehaviour,IUserInterface
    {

        public int score;
        
        [SerializeField] private CharacterController controller;
        [SerializeField] private InputContainer inputContainer;
        [SerializeField] private TMP_Text scoreText;

        public ObjectSpawner spawner;
        private void Awake()
        {
            // Register to Containers
            inputContainer.PlayerController = this;
            inputContainer.SpawnContainer.PlayerController = this;
            ResetScore();
        }

        internal void InvokeMovement()
        {
            controller.Move(inputContainer.movementWay);
            SmoothRotation(inputContainer.normalizedDirection, inputContainer.rotationReferanceValue, inputContainer.RotationSmooth);
        }
    
        private void SmoothRotation(Vector3 directionOfMovement , float rotationSmooth, float smoothMultiplier)
        {
            float angleToTurn = Mathf.Atan2(directionOfMovement.x,directionOfMovement.z) * Mathf.Rad2Deg;
            float angleToSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y,angleToTurn,ref rotationSmooth , smoothMultiplier);
            transform.rotation = Quaternion.Euler(0,angleToSmooth,0);
        }

        //testing
        public Crystal crystal;
        public void TriggerTest()
        {
            if (CheckPoints)
            {
                UpdateScore();
            }
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (other.TryGetComponent(out crystal))
            {
                if (CheckPoints)
                {
                    UpdateScore();
                }
                    
            }
        }

        private void AddScore()
        {
            crystal.OnCollected(ref score, spawner);
        }

        private void ResetScore()
        {
            score = 0;
            scoreText.text = $"{score}";
        }

        public bool CheckPoints
        {
            get
            {
                if (score < 100)
                {
                    AddScore();
                    if (score == 100)
                    {
                        Debug.Log("Win");
                        GetComponent<ButtonListener>().LoadScene();
                    }
                    return true;
                }
                return false;
            }
        }

        private void UpdateScore()
        {
            scoreText.text = $"{score}";
        }
    }
}