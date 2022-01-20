using ScriptableContainers;
using ScriptableContainers.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick_Pack.Scripts.Joysticks
{
    public class FloatingJoystick : Joystick
    {
        [SerializeField] InputContainer inputContainer;
        protected override void Start()
        {
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
        }

        private void Update()
        {
            if (background.gameObject.activeInHierarchy && new Vector2(Horizontal,Vertical).magnitude > inputContainer.InputSensivity)
            {
                inputContainer.SetMovementValues(Horizontal, Vertical); 
            }
            else
            {
                inputContainer.AutoMove(); 

            }
        }
    }
}