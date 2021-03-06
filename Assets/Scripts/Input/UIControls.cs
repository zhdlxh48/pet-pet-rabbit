//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.0
//     from Assets/Resources/UIControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Rabbit
{
    public partial class @UIControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @UIControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIControls"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""83444388-077d-4a5c-a436-3a068f039647"",
            ""actions"": [
                {
                    ""name"": ""UpArrow"",
                    ""type"": ""Button"",
                    ""id"": ""3273497b-2770-4277-be02-9307d1b54d3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownArrow"",
                    ""type"": ""Button"",
                    ""id"": ""8becfe36-4b59-47a5-8fe2-5c45719ce412"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a70cfb6e-1430-47b5-a849-719d133119e2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a7688f4-c3c1-4941-a03b-738fb8e2e1a0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_UpArrow = m_UI.FindAction("UpArrow", throwIfNotFound: true);
            m_UI_DownArrow = m_UI.FindAction("DownArrow", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_UpArrow;
        private readonly InputAction m_UI_DownArrow;
        public struct UIActions
        {
            private @UIControls m_Wrapper;
            public UIActions(@UIControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @UpArrow => m_Wrapper.m_UI_UpArrow;
            public InputAction @DownArrow => m_Wrapper.m_UI_DownArrow;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @UpArrow.started -= m_Wrapper.m_UIActionsCallbackInterface.OnUpArrow;
                    @UpArrow.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnUpArrow;
                    @UpArrow.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnUpArrow;
                    @DownArrow.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDownArrow;
                    @DownArrow.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDownArrow;
                    @DownArrow.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDownArrow;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @UpArrow.started += instance.OnUpArrow;
                    @UpArrow.performed += instance.OnUpArrow;
                    @UpArrow.canceled += instance.OnUpArrow;
                    @DownArrow.started += instance.OnDownArrow;
                    @DownArrow.performed += instance.OnDownArrow;
                    @DownArrow.canceled += instance.OnDownArrow;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnUpArrow(InputAction.CallbackContext context);
            void OnDownArrow(InputAction.CallbackContext context);
        }
    }
}
