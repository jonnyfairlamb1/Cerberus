//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/NovaFPS/Inputs/PlayerActions.inputactions
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

public partial class @PlayerActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""GameControls"",
            ""id"": ""e8b0cfa3-c0e4-41c4-a4f3-877d8f768807"",
            ""actions"": [
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""c205d408-8e62-4d90-a77b-d05df583228f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reloading"",
                    ""type"": ""Button"",
                    ""id"": ""5ba65d6e-b6c7-4e18-9ec8-3ac53040e708"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""12c0f474-d074-4b0b-b279-57c18046bdb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouching"",
                    ""type"": ""Button"",
                    ""id"": ""c23853d1-3e26-4f12-90a1-76b139bb2535"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprinting"",
                    ""type"": ""Button"",
                    ""id"": ""65cc1277-ad7a-46b9-b792-6a759ee0298f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Firing"",
                    ""type"": ""Button"",
                    ""id"": ""6493f33d-9a7b-4aa6-bed8-73345df0ea63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scrolling"",
                    ""type"": ""Value"",
                    ""id"": ""279c14e4-77e2-4b8e-ad36-f39efa76af99"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aiming"",
                    ""type"": ""Button"",
                    ""id"": ""e6e774e5-fe27-4964-abe6-f04848352a6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interacting"",
                    ""type"": ""Button"",
                    ""id"": ""a4ccaad9-3483-4238-84da-c3be2b4096dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""9b6f98f8-19cb-4ffe-9fc4-9f07705f0d8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeWeapons"",
                    ""type"": ""Value"",
                    ""id"": ""e22ad45d-0997-4bf3-8439-55f3d7de3783"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""1ba852f7-4508-43ff-9a07-a27d5dc86af2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""0059f214-490b-4648-a1f1-9acd1d4d3619"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6ca9fd82-4f86-4626-8b19-7e947547f36f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b50c86bb-9874-41ac-be2b-03c80ee48a9f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f37787fc-e6ac-47e7-a4ef-924b3e3bd042"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reloading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10dc34b7-a069-4bc6-8ef5-544f7597386d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Reloading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bc79522-d599-40e6-a752-266e72890de1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31fec141-9d81-4116-9bcf-9520c22f50a3"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c69fce5-dcf1-4b50-ae6b-d3372c606109"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87a8ae2e-1096-4770-8143-2e6485b312f5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Crouching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0aec971-65d3-4af7-9274-83fcebda8b42"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb59cb24-1f10-474f-aec9-c6de813c855d"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a3ed417-20b0-42b2-9957-f17849f64fca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a98b39c3-270f-4b7a-957a-cf796feece84"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82a6a5d3-67ea-48e0-a8c2-9f638bd1708e"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Scrolling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c8f736a-9941-41e1-8593-180e5f2a7d79"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddd8430f-bc3b-4a17-91d5-bbb5faa59efd"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c32d236f-19be-444f-b7bd-ef99a5ff25c8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interacting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17c6f67e-32f2-457f-a1a1-08ce669ff4e9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interacting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9471ab10-b588-410c-b42e-6a5989debe69"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eae6774f-58f9-499f-bc0e-fdf6c35f167b"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Bumpers"",
                    ""id"": ""fa9921bf-e27a-43cd-8676-3527ae9b6aad"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapons"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c580bb79-5a69-473c-875a-5b493a61ba82"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ChangeWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""35aecb21-0280-49b5-8e49-477fd4a6d1af"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ChangeWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9850fdea-b923-4dba-9112-68721d5d9161"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c68fb7b-3e73-4fe8-bf07-7ca643463803"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""9641315e-6e66-42c2-8bcc-73439d9c4630"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14ed430d-34e6-4cdc-92a9-22b7a2cfdd49"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""895fa7db-67a3-4c55-af09-32200049e759"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e0dcd9b8-d553-4a58-a260-30f783734231"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0ce5bb8f-a06a-40f6-80dd-95b552238022"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""960c68ab-4a4d-4f29-8f16-295baf53fb9b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dd273cf6-d9fd-4775-8dcd-3201eb9e5833"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f9b25b6c-c513-4f60-aa05-40240ca12e3e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""141b2214-ca6f-45e9-8a47-f3698068b772"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0f02c7c5-7f4e-47ec-8658-299f0f672ef9"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": []
        }
    ]
}");
        // GameControls
        m_GameControls = asset.FindActionMap("GameControls", throwIfNotFound: true);
        m_GameControls_Jumping = m_GameControls.FindAction("Jumping", throwIfNotFound: true);
        m_GameControls_Reloading = m_GameControls.FindAction("Reloading", throwIfNotFound: true);
        m_GameControls_Melee = m_GameControls.FindAction("Melee", throwIfNotFound: true);
        m_GameControls_Crouching = m_GameControls.FindAction("Crouching", throwIfNotFound: true);
        m_GameControls_Sprinting = m_GameControls.FindAction("Sprinting", throwIfNotFound: true);
        m_GameControls_Firing = m_GameControls.FindAction("Firing", throwIfNotFound: true);
        m_GameControls_Scrolling = m_GameControls.FindAction("Scrolling", throwIfNotFound: true);
        m_GameControls_Aiming = m_GameControls.FindAction("Aiming", throwIfNotFound: true);
        m_GameControls_Interacting = m_GameControls.FindAction("Interacting", throwIfNotFound: true);
        m_GameControls_Drop = m_GameControls.FindAction("Drop", throwIfNotFound: true);
        m_GameControls_ChangeWeapons = m_GameControls.FindAction("ChangeWeapons", throwIfNotFound: true);
        m_GameControls_Pause = m_GameControls.FindAction("Pause", throwIfNotFound: true);
        m_GameControls_Movement = m_GameControls.FindAction("Movement", throwIfNotFound: true);
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

    // GameControls
    private readonly InputActionMap m_GameControls;
    private IGameControlsActions m_GameControlsActionsCallbackInterface;
    private readonly InputAction m_GameControls_Jumping;
    private readonly InputAction m_GameControls_Reloading;
    private readonly InputAction m_GameControls_Melee;
    private readonly InputAction m_GameControls_Crouching;
    private readonly InputAction m_GameControls_Sprinting;
    private readonly InputAction m_GameControls_Firing;
    private readonly InputAction m_GameControls_Scrolling;
    private readonly InputAction m_GameControls_Aiming;
    private readonly InputAction m_GameControls_Interacting;
    private readonly InputAction m_GameControls_Drop;
    private readonly InputAction m_GameControls_ChangeWeapons;
    private readonly InputAction m_GameControls_Pause;
    private readonly InputAction m_GameControls_Movement;
    public struct GameControlsActions
    {
        private @PlayerActions m_Wrapper;
        public GameControlsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jumping => m_Wrapper.m_GameControls_Jumping;
        public InputAction @Reloading => m_Wrapper.m_GameControls_Reloading;
        public InputAction @Melee => m_Wrapper.m_GameControls_Melee;
        public InputAction @Crouching => m_Wrapper.m_GameControls_Crouching;
        public InputAction @Sprinting => m_Wrapper.m_GameControls_Sprinting;
        public InputAction @Firing => m_Wrapper.m_GameControls_Firing;
        public InputAction @Scrolling => m_Wrapper.m_GameControls_Scrolling;
        public InputAction @Aiming => m_Wrapper.m_GameControls_Aiming;
        public InputAction @Interacting => m_Wrapper.m_GameControls_Interacting;
        public InputAction @Drop => m_Wrapper.m_GameControls_Drop;
        public InputAction @ChangeWeapons => m_Wrapper.m_GameControls_ChangeWeapons;
        public InputAction @Pause => m_Wrapper.m_GameControls_Pause;
        public InputAction @Movement => m_Wrapper.m_GameControls_Movement;
        public InputActionMap Get() { return m_Wrapper.m_GameControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControlsActions set) { return set.Get(); }
        public void SetCallbacks(IGameControlsActions instance)
        {
            if (m_Wrapper.m_GameControlsActionsCallbackInterface != null)
            {
                @Jumping.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnJumping;
                @Jumping.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnJumping;
                @Jumping.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnJumping;
                @Reloading.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnReloading;
                @Reloading.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnReloading;
                @Reloading.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnReloading;
                @Melee.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMelee;
                @Crouching.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCrouching;
                @Crouching.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCrouching;
                @Crouching.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnCrouching;
                @Sprinting.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSprinting;
                @Sprinting.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSprinting;
                @Sprinting.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnSprinting;
                @Firing.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnFiring;
                @Firing.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnFiring;
                @Firing.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnFiring;
                @Scrolling.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnScrolling;
                @Scrolling.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnScrolling;
                @Scrolling.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnScrolling;
                @Aiming.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnAiming;
                @Aiming.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnAiming;
                @Aiming.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnAiming;
                @Interacting.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnInteracting;
                @Interacting.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnInteracting;
                @Interacting.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnInteracting;
                @Drop.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnDrop;
                @ChangeWeapons.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnChangeWeapons;
                @ChangeWeapons.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnChangeWeapons;
                @ChangeWeapons.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnChangeWeapons;
                @Pause.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnPause;
                @Movement.started -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameControlsActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_GameControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jumping.started += instance.OnJumping;
                @Jumping.performed += instance.OnJumping;
                @Jumping.canceled += instance.OnJumping;
                @Reloading.started += instance.OnReloading;
                @Reloading.performed += instance.OnReloading;
                @Reloading.canceled += instance.OnReloading;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Crouching.started += instance.OnCrouching;
                @Crouching.performed += instance.OnCrouching;
                @Crouching.canceled += instance.OnCrouching;
                @Sprinting.started += instance.OnSprinting;
                @Sprinting.performed += instance.OnSprinting;
                @Sprinting.canceled += instance.OnSprinting;
                @Firing.started += instance.OnFiring;
                @Firing.performed += instance.OnFiring;
                @Firing.canceled += instance.OnFiring;
                @Scrolling.started += instance.OnScrolling;
                @Scrolling.performed += instance.OnScrolling;
                @Scrolling.canceled += instance.OnScrolling;
                @Aiming.started += instance.OnAiming;
                @Aiming.performed += instance.OnAiming;
                @Aiming.canceled += instance.OnAiming;
                @Interacting.started += instance.OnInteracting;
                @Interacting.performed += instance.OnInteracting;
                @Interacting.canceled += instance.OnInteracting;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @ChangeWeapons.started += instance.OnChangeWeapons;
                @ChangeWeapons.performed += instance.OnChangeWeapons;
                @ChangeWeapons.canceled += instance.OnChangeWeapons;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public GameControlsActions @GameControls => new GameControlsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IGameControlsActions
    {
        void OnJumping(InputAction.CallbackContext context);
        void OnReloading(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnCrouching(InputAction.CallbackContext context);
        void OnSprinting(InputAction.CallbackContext context);
        void OnFiring(InputAction.CallbackContext context);
        void OnScrolling(InputAction.CallbackContext context);
        void OnAiming(InputAction.CallbackContext context);
        void OnInteracting(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnChangeWeapons(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
