// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/HookInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HookInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HookInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HookInput"",
    ""maps"": [
        {
            ""name"": ""Hook"",
            ""id"": ""888e67e6-11e3-4600-86b4-4318351d2ef2"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""63ec6d76-904c-4e03-8603-60e982e541e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1c7be440-217c-4d0a-a173-d7571a640ebd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse & KeyBoard"",
            ""bindingGroup"": ""Mouse & KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Hook
        m_Hook = asset.FindActionMap("Hook", throwIfNotFound: true);
        m_Hook_Use = m_Hook.FindAction("Use", throwIfNotFound: true);
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

    // Hook
    private readonly InputActionMap m_Hook;
    private IHookActions m_HookActionsCallbackInterface;
    private readonly InputAction m_Hook_Use;
    public struct HookActions
    {
        private @HookInput m_Wrapper;
        public HookActions(@HookInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Hook_Use;
        public InputActionMap Get() { return m_Wrapper.m_Hook; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HookActions set) { return set.Get(); }
        public void SetCallbacks(IHookActions instance)
        {
            if (m_Wrapper.m_HookActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_HookActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_HookActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_HookActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_HookActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public HookActions @Hook => new HookActions(this);
    private int m_MouseKeyBoardSchemeIndex = -1;
    public InputControlScheme MouseKeyBoardScheme
    {
        get
        {
            if (m_MouseKeyBoardSchemeIndex == -1) m_MouseKeyBoardSchemeIndex = asset.FindControlSchemeIndex("Mouse & KeyBoard");
            return asset.controlSchemes[m_MouseKeyBoardSchemeIndex];
        }
    }
    public interface IHookActions
    {
        void OnUse(InputAction.CallbackContext context);
    }
}
