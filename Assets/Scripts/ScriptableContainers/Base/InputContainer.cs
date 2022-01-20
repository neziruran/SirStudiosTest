using System;
using Player;
using UnityEngine;

namespace ScriptableContainers.Base
{
    [CreateAssetMenu (menuName ="ScriptableContainers / Input Container")]
    public class InputContainer : ScriptableObject
    {
        public SpawnContainer SpawnContainer;
        public float InputSensivity = 0.1f;
        public float VerticalValue;
        public float HorizontalValue;
        public float MovementSpeed;
        public float RotationSmooth;
        [Range(-5,0)]public float Gravity;
        public Vector3 movementWay;
        public Vector3 normalizedDirection;
        public float rotationReferanceValue;

        [field: Tooltip("Listeners")] public PlayerController PlayerController { get; set; }

        private void OnEnable()
        {
            VerticalValue = 0;
            HorizontalValue = 0;
            movementWay = Vector3.zero;
            normalizedDirection = Vector3.zero;
        }

        public void SetMovementValues(float valueX , float valueZ)
        {
            VerticalValue = valueX;
            HorizontalValue = valueZ;
            normalizedDirection = new Vector3(VerticalValue,0,HorizontalValue).normalized;
            movementWay = new Vector3(normalizedDirection.x,Gravity,normalizedDirection.z) * Time.deltaTime * MovementSpeed;
            PlayerController.InvokeMovement();
        }

        public void AutoMove()
        {
            normalizedDirection = new Vector3(VerticalValue,0,HorizontalValue).normalized;
            movementWay = new Vector3(normalizedDirection.x,Gravity,normalizedDirection.z) * Time.deltaTime * MovementSpeed;
            PlayerController.InvokeMovement();
        }

    }
}
