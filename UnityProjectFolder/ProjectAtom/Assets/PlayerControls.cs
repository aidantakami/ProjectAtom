// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Dog Player Controls"",
            ""id"": ""f6db9c46-6e5f-413a-8ead-c9297800352c"",
            ""actions"": [
                {
                    ""name"": ""Left Right Movement"",
                    ""type"": ""Button"",
                    ""id"": ""28b492cb-af6d-4f86-be2d-8f7669ada8ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw Boomerang"",
                    ""type"": ""Button"",
                    ""id"": ""f61587c3-16cc-4fa7-b2a8-1fb4ef7984bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b326e01f-f9c7-405a-8509-8183bc4ceded"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Right Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6fbaa7c2-7e79-48fc-bffb-d34b9df72891"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Right Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c0863c3e-0fbe-4305-a0c9-62b5a1fd1851"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Right Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7b394ade-3935-4fe1-93ac-15b642d5b049"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw Boomerang"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Boomerang Player Controls"",
            ""id"": ""9506f2f0-f6f3-4b0c-b9c7-292b0d05055c"",
            ""actions"": [
                {
                    ""name"": ""Boomerang Movement"",
                    ""type"": ""Button"",
                    ""id"": ""8f27e49b-7bf6-49ad-af64-297b96b81e2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f3d98009-6eb8-4453-8163-5aecd7069f0d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boomerang Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8ee4feec-87f2-4838-be2e-bb4915e2663f"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boomerang Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3d1d61c3-c634-47e4-9c70-b6a166b6bca8"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boomerang Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""50b5a7cc-1135-4088-8f8a-8fccc2dafc4f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boomerang Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d1ef68cc-093f-4229-ad3b-376f96e02fee"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boomerang Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Dog Player Controls
        m_DogPlayerControls = asset.FindActionMap("Dog Player Controls", throwIfNotFound: true);
        m_DogPlayerControls_LeftRightMovement = m_DogPlayerControls.FindAction("Left Right Movement", throwIfNotFound: true);
        m_DogPlayerControls_ThrowBoomerang = m_DogPlayerControls.FindAction("Throw Boomerang", throwIfNotFound: true);
        // Boomerang Player Controls
        m_BoomerangPlayerControls = asset.FindActionMap("Boomerang Player Controls", throwIfNotFound: true);
        m_BoomerangPlayerControls_BoomerangMovement = m_BoomerangPlayerControls.FindAction("Boomerang Movement", throwIfNotFound: true);
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

    // Dog Player Controls
    private readonly InputActionMap m_DogPlayerControls;
    private IDogPlayerControlsActions m_DogPlayerControlsActionsCallbackInterface;
    private readonly InputAction m_DogPlayerControls_LeftRightMovement;
    private readonly InputAction m_DogPlayerControls_ThrowBoomerang;
    public struct DogPlayerControlsActions
    {
        private @PlayerControls m_Wrapper;
        public DogPlayerControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftRightMovement => m_Wrapper.m_DogPlayerControls_LeftRightMovement;
        public InputAction @ThrowBoomerang => m_Wrapper.m_DogPlayerControls_ThrowBoomerang;
        public InputActionMap Get() { return m_Wrapper.m_DogPlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DogPlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IDogPlayerControlsActions instance)
        {
            if (m_Wrapper.m_DogPlayerControlsActionsCallbackInterface != null)
            {
                @LeftRightMovement.started -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnLeftRightMovement;
                @LeftRightMovement.performed -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnLeftRightMovement;
                @LeftRightMovement.canceled -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnLeftRightMovement;
                @ThrowBoomerang.started -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnThrowBoomerang;
                @ThrowBoomerang.performed -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnThrowBoomerang;
                @ThrowBoomerang.canceled -= m_Wrapper.m_DogPlayerControlsActionsCallbackInterface.OnThrowBoomerang;
            }
            m_Wrapper.m_DogPlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftRightMovement.started += instance.OnLeftRightMovement;
                @LeftRightMovement.performed += instance.OnLeftRightMovement;
                @LeftRightMovement.canceled += instance.OnLeftRightMovement;
                @ThrowBoomerang.started += instance.OnThrowBoomerang;
                @ThrowBoomerang.performed += instance.OnThrowBoomerang;
                @ThrowBoomerang.canceled += instance.OnThrowBoomerang;
            }
        }
    }
    public DogPlayerControlsActions @DogPlayerControls => new DogPlayerControlsActions(this);

    // Boomerang Player Controls
    private readonly InputActionMap m_BoomerangPlayerControls;
    private IBoomerangPlayerControlsActions m_BoomerangPlayerControlsActionsCallbackInterface;
    private readonly InputAction m_BoomerangPlayerControls_BoomerangMovement;
    public struct BoomerangPlayerControlsActions
    {
        private @PlayerControls m_Wrapper;
        public BoomerangPlayerControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @BoomerangMovement => m_Wrapper.m_BoomerangPlayerControls_BoomerangMovement;
        public InputActionMap Get() { return m_Wrapper.m_BoomerangPlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BoomerangPlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IBoomerangPlayerControlsActions instance)
        {
            if (m_Wrapper.m_BoomerangPlayerControlsActionsCallbackInterface != null)
            {
                @BoomerangMovement.started -= m_Wrapper.m_BoomerangPlayerControlsActionsCallbackInterface.OnBoomerangMovement;
                @BoomerangMovement.performed -= m_Wrapper.m_BoomerangPlayerControlsActionsCallbackInterface.OnBoomerangMovement;
                @BoomerangMovement.canceled -= m_Wrapper.m_BoomerangPlayerControlsActionsCallbackInterface.OnBoomerangMovement;
            }
            m_Wrapper.m_BoomerangPlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BoomerangMovement.started += instance.OnBoomerangMovement;
                @BoomerangMovement.performed += instance.OnBoomerangMovement;
                @BoomerangMovement.canceled += instance.OnBoomerangMovement;
            }
        }
    }
    public BoomerangPlayerControlsActions @BoomerangPlayerControls => new BoomerangPlayerControlsActions(this);
    public interface IDogPlayerControlsActions
    {
        void OnLeftRightMovement(InputAction.CallbackContext context);
        void OnThrowBoomerang(InputAction.CallbackContext context);
    }
    public interface IBoomerangPlayerControlsActions
    {
        void OnBoomerangMovement(InputAction.CallbackContext context);
    }
}
