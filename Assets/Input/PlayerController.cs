// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""keyboard"",
            ""id"": ""2cca80a9-0092-4393-9ac1-38fa51a23ebe"",
            ""actions"": [
                {
                    ""name"": ""MoveKeys"",
                    ""type"": ""Value"",
                    ""id"": ""710ba284-e4e7-4e58-b1d2-5b265046a689"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""9242b8c6-9e4c-412d-996a-b5a7c5c27714"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""943054a3-bd83-420e-8005-3fff9ffa57d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b19a8ccc-594f-4f82-94a3-696b0cbdf791"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveKeys"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c11b3d67-f6d7-4d0d-b9dd-2643e791f9aa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ac694c4a-6387-4943-94f2-ca1b3421f8df"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""35b3d56b-a26b-45a8-b09c-b9da82606b8b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a5868d49-5d43-49c1-b852-f538309bff20"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a9f63b35-51eb-45af-b06b-211f8f92ec9b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6f000c7-978d-458c-978b-ccac6682e056"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // keyboard
        m_keyboard = asset.FindActionMap("keyboard", throwIfNotFound: true);
        m_keyboard_MoveKeys = m_keyboard.FindAction("MoveKeys", throwIfNotFound: true);
        m_keyboard_Run = m_keyboard.FindAction("Run", throwIfNotFound: true);
        m_keyboard_Jump = m_keyboard.FindAction("Jump", throwIfNotFound: true);
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

    // keyboard
    private readonly InputActionMap m_keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_keyboard_MoveKeys;
    private readonly InputAction m_keyboard_Run;
    private readonly InputAction m_keyboard_Jump;
    public struct KeyboardActions
    {
        private @PlayerController m_Wrapper;
        public KeyboardActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveKeys => m_Wrapper.m_keyboard_MoveKeys;
        public InputAction @Run => m_Wrapper.m_keyboard_Run;
        public InputAction @Jump => m_Wrapper.m_keyboard_Jump;
        public InputActionMap Get() { return m_Wrapper.m_keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @MoveKeys.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoveKeys;
                @MoveKeys.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoveKeys;
                @MoveKeys.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoveKeys;
                @Run.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveKeys.started += instance.OnMoveKeys;
                @MoveKeys.performed += instance.OnMoveKeys;
                @MoveKeys.canceled += instance.OnMoveKeys;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public KeyboardActions @keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnMoveKeys(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
