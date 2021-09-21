using System;
using UnityEngine;

namespace Rabbit
{
    public class UIInput : MonoBehaviour
    {
        private UIControls inputSystem;

        public static Action UPArrowAction;
        public static Action DownArrowAction;

        private void Awake()
        {
            inputSystem = new UIControls();

            inputSystem.UI.UpArrow.performed += (ctx) => UPArrowAction.Invoke();
            inputSystem.UI.DownArrow.performed += (ctx) => DownArrowAction.Invoke();
        }

        private void OnEnable()
        {
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            inputSystem.Disable();
        }
    }
}