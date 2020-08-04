// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Kart"",
            ""id"": ""29650e63-46c4-480b-9d9d-9f0ba7710a3b"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""c46e7387-98f4-490e-96e8-79f60846daa3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d2db2e43-ef4a-47ef-9f30-47f1b3e88da7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brakes"",
                    ""type"": ""Button"",
                    ""id"": ""2d637014-8a11-4abf-b0b0-61bf2b70b5a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""db2f86e5-b362-4fc4-82f0-89885bb6da2d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eaa9031c-58e7-489a-a516-0e47a48cd37a"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c15210b-1b21-481e-90e0-fc64e30586c4"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brakes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Kart
        m_Kart = asset.FindActionMap("Kart", throwIfNotFound: true);
        m_Kart_Accelerate = m_Kart.FindAction("Accelerate", throwIfNotFound: true);
        m_Kart_Movement = m_Kart.FindAction("Movement", throwIfNotFound: true);
        m_Kart_Brakes = m_Kart.FindAction("Brakes", throwIfNotFound: true);
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

    // Kart
    private readonly InputActionMap m_Kart;
    private IKartActions m_KartActionsCallbackInterface;
    private readonly InputAction m_Kart_Accelerate;
    private readonly InputAction m_Kart_Movement;
    private readonly InputAction m_Kart_Brakes;
    public struct KartActions
    {
        private @Controls m_Wrapper;
        public KartActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Kart_Accelerate;
        public InputAction @Movement => m_Wrapper.m_Kart_Movement;
        public InputAction @Brakes => m_Wrapper.m_Kart_Brakes;
        public InputActionMap Get() { return m_Wrapper.m_Kart; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KartActions set) { return set.Get(); }
        public void SetCallbacks(IKartActions instance)
        {
            if (m_Wrapper.m_KartActionsCallbackInterface != null)
            {
                @Accelerate.started -= m_Wrapper.m_KartActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_KartActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_KartActionsCallbackInterface.OnAccelerate;
                @Movement.started -= m_Wrapper.m_KartActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KartActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KartActionsCallbackInterface.OnMovement;
                @Brakes.started -= m_Wrapper.m_KartActionsCallbackInterface.OnBrakes;
                @Brakes.performed -= m_Wrapper.m_KartActionsCallbackInterface.OnBrakes;
                @Brakes.canceled -= m_Wrapper.m_KartActionsCallbackInterface.OnBrakes;
            }
            m_Wrapper.m_KartActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Brakes.started += instance.OnBrakes;
                @Brakes.performed += instance.OnBrakes;
                @Brakes.canceled += instance.OnBrakes;
            }
        }
    }
    public KartActions @Kart => new KartActions(this);
    public interface IKartActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnBrakes(InputAction.CallbackContext context);
    }
}
