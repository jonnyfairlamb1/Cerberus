using UnityEngine;

#if ENABLE_INPUT_SYSTEM

using UnityEngine.InputSystem;

#endif

namespace NovaFPS {

    /// <summary>
    /// This script receives inputs from the player and makes it accessible for any script which might require these.
    /// This is pretty convenient regarding to organization, having all inputs in one same place may result in an easier customization later on.
    /// Note that this script is still subject to major changes in order to make it more adaptable for both old and new Unity input system as well as for
    /// a rebinding system
    /// </summary>
    public class InputManager : MonoBehaviour {
        public static InputManager inputManager;
        public static PlayerMovement PlayerMovement;

        // Inputs
        public static bool shooting, reloading, aiming, jumping, sprinting, crouching, crouchingDown, interacting, dropping, nextweapon, previousweapon, inspecting, melee, pausing;

        public static float x, y, scrolling, mousex, mousey, controllerx, controllery;
        public static float sensitivity = 50f, controllerSensitivityX = 30f, controllerSensitivityY = 30f;
        [Range(.1f, 1f)] public static float aimingSensitivityMultiplier = .5f;
        public static bool invertedAxis;
        public float sensitivity_ = 50f, controllerSensitivityX_ = 30f, controllerSensitivityY_ = 30f;
        [Range(.1f, 1f)] public float aimingSensitivityMultiplier_ = .5f;
        public bool alternateCrouch;
        public bool alternateSprint;
        public bool alternateAiming;
        private bool canAim = true;

        public static PlayerActions inputActions;

        private bool toggleCrouching, toggleSprinting, toggleAiming;

        public enum curDevice {
            Keyboard, Gamepad
        };

        //public curDevice currentDev;
        private void Awake() {
            // We only wanna have a single InputManager in the scene
            if (inputManager == null) {
                DontDestroyOnLoad(this);
                inputManager = this;
            } else Destroy(this.gameObject);

            shooting = false;

            if (inputActions == null) {
                inputActions = new PlayerActions();
                inputActions.Enable();
            }

            //currentDev = curDevice.Keyboard;
        }

#if ENABLE_INPUT_SYSTEM

        private void OnEnable() {
            inputActions.GameControls.Crouching.started += ctx => toggleCrouching = true;
            inputActions.GameControls.Crouching.canceled += ctx => toggleCrouching = false;

            inputActions.GameControls.Sprinting.started += ctx => toggleSprinting = true;
            inputActions.GameControls.Sprinting.canceled += ctx => toggleSprinting = false;

            inputActions.GameControls.Aiming.started += ctx => toggleAiming = true;
            inputActions.GameControls.Aiming.canceled += ctx => toggleAiming = false;

            inputActions.GameControls.Pause.started += ctx => PauseMenu.TogglePause();
        }

#endif

        private void Update() {
            // Handle all the required inputs here

            sensitivity = sensitivity_;
            controllerSensitivityX = controllerSensitivityX_;
            controllerSensitivityY = controllerSensitivityY_;
            aimingSensitivityMultiplier = aimingSensitivityMultiplier_;

#if ENABLE_INPUT_SYSTEM
            if (Mouse.current != null) {
                mousex = Mouse.current.delta.x.ReadValue();
                mousey = Mouse.current.delta.y.ReadValue();
            }

            if (Gamepad.current != null) {
                controllerx = Gamepad.current.rightStick.x.ReadValue();
                controllery = -Gamepad.current.rightStick.y.ReadValue();
            }

            x = inputActions.GameControls.Movement.ReadValue<Vector2>().x;
            y = inputActions.GameControls.Movement.ReadValue<Vector2>().y;
            jumping = inputActions.GameControls.Jumping.ReadValue<float>() != 0;

            reloading = inputActions.GameControls.Reloading.ReadValue<float>() > 0;
            melee = inputActions.GameControls.Melee.ReadValue<float>() > 0;
            // Handle different crouching methods
            if (alternateCrouch) {
                if (toggleCrouching) {
                    crouching = !crouching;
                    crouchingDown = !crouchingDown;
                    toggleCrouching = false;
                }
            } else {
                crouching = inputActions.GameControls.Crouching.ReadValue<float>() > 0;
                crouchingDown = inputActions.GameControls.Crouching.ReadValue<float>() > 0;
            }

            if (alternateSprint) {
                if (toggleSprinting) {
                    sprinting = !sprinting;
                    toggleSprinting = false;
                }
            } else
                sprinting = inputActions.GameControls.Sprinting.ReadValue<float>() > 0;

            shooting = inputActions.GameControls.Firing.ReadValue<float>() > 0;

            scrolling = inputActions.GameControls.Scrolling.ReadValue<Vector2>().y + inputActions.GameControls.ChangeWeapons.ReadValue<float>(); ;

            if (alternateAiming) {
                if (toggleAiming) { aiming = !aiming; canAim = false; toggleAiming = false; }
            } else {
                if (inputActions.GameControls.Aiming.ReadValue<float>() > 0) aiming = true; else aiming = false;
            }
            if (inputActions.GameControls.Aiming.ReadValue<float>() == 0) canAim = true;

            interacting = inputActions.GameControls.Interacting.ReadValue<float>() > 0;
            dropping = inputActions.GameControls.Drop.ReadValue<float>() > 0;

#else
            mousex =  Input.GetAxis("Mouse X");
            mousey =  Input.GetAxis("Mouse Y");

            controllerx =  Input.GetAxis("XController");
            controllery =  Input.GetAxis("YController");

            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            jumping = Input.GetButton("Jump");
            reloading = Input.GetButton("reload");
            melee = Input.GetButton("Melee");

            // Handle different crouching methods
            if (alternateCrouch)
            {
                if (Input.GetButtonDown("crouch")) crouching = !crouching;
            }
            else crouching = Input.GetButton("crouch");
            crouchingDown = Input.GetButtonDown("crouch"); // Just get the moment of crouching
            HandleSprinting();

            if (Input.GetButton("Fire1") || Input.GetAxis("RightTrigger") > 0) shooting = true;
            else shooting = false;
            scrolling = Input.GetAxis("Mouse ScrollWheel");
            // Handle different aiming methods
            if(alternateAiming)
            {
                if (Input.GetAxis("LeftTrigger") != 0  && canAim || Input.GetButtonDown("aiming")) { aiming = !aiming;  canAim = false;}
            }
            else
            {
                if (Input.GetAxis("LeftTrigger") != 0 || Input.GetButton("aiming")) aiming = true; else aiming = false;
            }
            if (Input.GetAxis("LeftTrigger") == 0) canAim = true;

            interacting = Input.GetButton("interact");
            dropping = Input.GetButtonDown("drop");

            nextweapon = Input.GetButtonDown("nextweapon");
            previousweapon = Input.GetButtonDown("previousweapon");

            pausing = Input.GetButtonDown("pause");
#endif
        }
    }
}