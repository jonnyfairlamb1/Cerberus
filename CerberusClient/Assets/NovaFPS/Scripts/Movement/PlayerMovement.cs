using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NovaFPS;

// Add a rigidbody if needed, PlayerMovement.cs requires a rigidbody to work
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    #region others

    [System.Serializable]
    public class Events // Store your events
    {
        public UnityEvent OnMove, OnJump, OnLand, OnCrouch, OnStopCrouch, OnSprint, OnSpawn, OnSlide;
    }

    [System.Serializable]
    public class FootStepsAudio // store your footsteps audio
    {
        public AudioClip[] defaultStep, grassStep, metalStep, mudStep, woodStep;
    }

    #endregion others

    #region variables

    //Assignables
    [Tooltip("Find your camera parent, this is where it should be attached."), SerializeField]
    private Transform playerCam;

    [Tooltip("Object with the same height as your camera, used to orientate the player.")]
    public Transform orientation;

    [Tooltip("Our UI Canvas.")]
    public Transform UI;

    private PlayerStats stats;

    // References
    private Rigidbody rb;

    [HideInInspector] public WeaponController weapon;

    //Rotation and look
    private float xRotation;

    private float desiredX;

    //Movements
    [Tooltip("If true: Speed while running backwards = runSpeed." +
    "       if false: Speed while running backwards = walkSpeed")]
    public bool canRunBackwards;

    [Tooltip("If true: Speed while shooting = runSpeed." +
   "       if false: Speed while shooting = walkSpeed")]
    public bool canRunWhileShooting;

    public bool canJumpWhileCrouching;

    [Tooltip("Player deceleration from running speed to walking"), SerializeField]
    private float loseSpeedDeceleration;

    [Tooltip("Capacity to gain speed."), SerializeField]
    private float acceleration = 4500;

    [Tooltip("Maximum allowed speed.")]
    public float currentSpeed = 20;

    [Min(0.01f)]
    public float runSpeed, walkSpeed, crouchSpeed, crouchTransitionSpeed, superRunSpeed;

    [HideInInspector] public bool grounded { get; private set; }

    [Tooltip("Every object with this layer will be detected as ground, so you will be able to walk on it")]
    public LayerMask whatIsGround;

    [Range(0, .5f)]
    [Tooltip("Counter movement."), SerializeField]
    private float frictionForceAmount = 0.175f;

    private float threshold = 0.01f;

    [Tooltip("Maximum slope angle that you can walk through."), SerializeField]
    private float maxSlopeAngle = 35f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);

    private Vector3 playerScale;
    public Vector3 PlayerScale { get { return playerScale; } }

    [Tooltip("When true, player will be allowed to slide.")]
    public bool allowSliding;

    [Tooltip("Force added on sliding."), SerializeField]
    private float slideForce = 400;

    [Tooltip("Slide Friction Amount."), SerializeField]
    private float slideCounterMovement = 0.2f;

    private Vector3 normalVector = Vector3.up;

    //Jumping
    private bool canJump = true;

    public bool CanJump { get { return canJump; } set { canJump = value; } }

    private bool readyToJump = true;

    public bool ReadyToJump { get { return readyToJump; } }

    [Tooltip("Interval between jumping")][Min(.25f), SerializeField] private float jumpCooldown = .25f;

    [Tooltip("The higher this value is, the higher you will get to jump."), SerializeField]
    private float jumpForce = 550f;

    [Tooltip("How much control you own while you are not grounded. Being 0 = no control of it, 1 = Full control.")]
    [Range(0, 1), SerializeField]
    private float controlAirborne = .5f;

    [Tooltip("Turn this on to allow the player to crouch while jumping")]
    public bool allowCrouchWhileJumping;

    //Aim assist
    [Tooltip("Determine wether to apply aim assist or not.")]
    public bool applyAimAssist;

    [Min(.1f), SerializeField]
    private float maximumDistanceToAssistAim;

    [Tooltip("Snapping speed."), SerializeField]
    private float aimAssistSpeed;

    [Tooltip("size of the aim assist range."), SerializeField]
    private float aimAssistSensitivity = 3f;

    private RaycastHit hit;

    private Transform target;

    //Stamina
    [Tooltip("You will lose stamina on performing actions when true.")]
    public bool usesStamina;

    [SerializeField] private float stamina;

    [Tooltip("Minimum stamina required to being able to run again."), SerializeField]
    private float minStaminaRequiredToRun;

    [Tooltip("Max amount of stamina."), SerializeField]
    private float maxStamina;

    [SerializeField] private bool LoseStaminaWalking;

    [Tooltip("Amount of stamina lost on jumping."), SerializeField]
    private float staminaLossOnJump;

    [Tooltip("Amount of stamina lost on sliding."), SerializeField]
    private float staminaLossOnSlide;

    [SerializeField] private bool canRun;

    [Tooltip("Our Slider UI Object. Stamina will be shown here."), SerializeField]
    private Slider staminaSlider;

    //Others
    [HideInInspector] public bool isCrouching;

    [SerializeField] private FootStepsAudio footsteps;

    // Audio
    [Header("Audio")]
    private AudioSource _audio;

    [Tooltip("Volume of the AudioSource."), SerializeField]
    private float footstepVolume;

    private float stepTimer;

    public Events events;

    [Tooltip("Default field of view of your camera"), Range(1, 179), SerializeField] public float normalFOV;

    #endregion variables

    private void Awake() => GetAllReferences();

    private void Start() {
        playerScale = transform.localScale;
        canRun = true;
        canJump = true;

        ResetStamina();

        events.OnSpawn.Invoke();
    }

    private void Update() => Stamina();

    public void HandleVelocities() {
        if (isCrouching) return;
        if (weapon.weapon != null && weapon.isAiming && weapon.weapon.setMovementSpeedWhileAiming) {
            currentSpeed = weapon.weapon.movementSpeedWhileAiming;
            return;
        }
        if (InputManager.sprinting && canRun) {
            if (!canRunBackwards && InputManager.y < 0 || !canRunWhileShooting && InputManager.shooting && weapon.weapon != null)
                currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, Time.deltaTime * loseSpeedDeceleration);
            if (canRunBackwards || !canRunBackwards && Vector3.Dot(orientation.forward, rb.velocity) > 0) {
                if (!canRunWhileShooting && !InputManager.shooting || canRunWhileShooting) {
                    currentSpeed = runSpeed;
                }
            }
        } else {
            currentSpeed = walkSpeed;
        }

        if (rb.velocity.magnitude < .01f) currentSpeed = walkSpeed;
    }

    public void StartCrouch() {
        isCrouching = true;
        currentSpeed = crouchSpeed;
        transform.localScale = crouchScale;

        if (rb.velocity.magnitude > walkSpeed && grounded && allowSliding) { // Handle sliding
            events.OnSlide.Invoke(); // Invoke your own method on the moment you slid NOT WHILE YOU ARE SLIDING
                                     // Add the force on slide
            rb.AddForce(orientation.transform.forward * slideForce);
            //staminaLoss
            if (usesStamina) stamina -= staminaLossOnSlide;
        }
    }

    public void StopCrouch() {
        isCrouching = false;
        transform.localScale = Vector3.MoveTowards(transform.localScale, playerScale, Time.deltaTime * crouchTransitionSpeed);
    }

    private void Stamina() {
        // Check if we def wanna use stamina
        if (!usesStamina || stats.isDead || !PlayerStats.Controllable) return;

        float oldStamina = stamina; // Store stamina before we change its value

        // We ran out of stamina
        if (stamina <= 0) {
            canRun = false;
            canJump = false;
        }

        // Wait for stamina to regenerate up to the min value allowed to start running and jumping again
        if (stamina >= minStaminaRequiredToRun) {
            canRun = true; canJump = true;
        }

        // Regen stamina
        if (stamina < maxStamina) {
            if (currentSpeed <= walkSpeed && !LoseStaminaWalking
                || currentSpeed < runSpeed && (!LoseStaminaWalking || LoseStaminaWalking && InputManager.x == 0 && InputManager.y == 0))
                stamina += Time.deltaTime;
        }

        // Lose stamina
        if (currentSpeed == runSpeed && canRun) stamina -= Time.deltaTime;
        if (currentSpeed < runSpeed && LoseStaminaWalking && (InputManager.x != 0 || InputManager.y != 0)) stamina -= Time.deltaTime * (walkSpeed / runSpeed);

        // Stamina UI not found might be a problem, it won´t be shown but you will get notified
        if (staminaSlider == null) {
            Debug.LogWarning("REMEMBER: You forgot to attach your StaminaSlider UI Component, so it won´t be shown.");
            return;
        }

        // Handle stamina UI
        if (oldStamina != stamina)
            staminaSlider.gameObject.SetActive(true);
        else
            staminaSlider.gameObject.SetActive(false);

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = stamina;
    }

    /// <summary>
    /// Handle all the basics related to the movement of the player.
    /// </summary>
    public void Movement(bool move) {
        //Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        FrictionForce(InputManager.x, InputManager.y, mag);

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (InputManager.x > 0 && xMag > currentSpeed || InputManager.x < 0 && xMag < -currentSpeed) InputManager.x = 0;
        if (InputManager.y > 0 && yMag > currentSpeed || InputManager.y < 0 && yMag < -currentSpeed) InputManager.y = 0;

        float multiplier = (!grounded) ? controlAirborne : 1;
        float multiplierV = (!grounded) ? controlAirborne : 1;

        float multiplier2 = (weapon.weapon != null) ? weapon.weapon.weightMultiplier : 1;

        if (!move) return;

        rb.AddForce(orientation.transform.forward * InputManager.y * acceleration * Time.deltaTime * multiplier * multiplierV / multiplier2);
        rb.AddForce(orientation.transform.right * InputManager.x * acceleration * Time.deltaTime * multiplier / multiplier2);
    }

    /// <summary>
    /// Manage the footstep audio playing
    /// </summary>
    public void FootSteps() {
        // Reset timer if conditions are met + dont play the footsteps
        if (!grounded || rb.velocity.magnitude <= .1f) {
            stepTimer = .7f;
            return;
        }

        // Wait for the next time to play a sound
        stepTimer -= Time.deltaTime * rb.velocity.magnitude / 15;

        // Play the sound and reset
        if (stepTimer <= 0) {
            stepTimer = .5f;
            _audio.pitch = UnityEngine.Random.Range(.7f, 1.3f); // Add variety to avoid boring and repetitive sounds while walking
            // Remember that you can also add a few more sounds to each of the layers to add even more variety to your sfx.
            if (Physics.Raycast(playerCam.position, Vector3.down, out RaycastHit hit, playerCam.position.y + .01f, whatIsGround)) {
                int i = 0;
                switch (hit.transform.gameObject.layer) {
                    case 3: // Ground
                        i = UnityEngine.Random.Range(0, footsteps.defaultStep.Length);
                        _audio.PlayOneShot(footsteps.defaultStep[i], footstepVolume);
                        break;

                    case 10: // Grass
                        i = UnityEngine.Random.Range(0, footsteps.grassStep.Length);
                        _audio.PlayOneShot(footsteps.grassStep[i], footstepVolume);
                        break;

                    case 11: // Metal
                        i = UnityEngine.Random.Range(0, footsteps.metalStep.Length);
                        _audio.PlayOneShot(footsteps.metalStep[i], footstepVolume);
                        break;

                    case 12: // Mud
                        i = UnityEngine.Random.Range(0, footsteps.mudStep.Length);
                        _audio.PlayOneShot(footsteps.mudStep[i], footstepVolume);
                        break;

                    case 13: // Wood
                        i = UnityEngine.Random.Range(0, footsteps.woodStep.Length);
                        _audio.PlayOneShot(footsteps.woodStep[i], footstepVolume);
                        break;
                }
            }
        }
    }

    public void Jump() {
        readyToJump = false;
        //staminaLoss
        if (usesStamina) stamina -= staminaLossOnJump;

        //Add jump forces
        rb.AddForce(Vector2.up * jumpForce * 1.5f);
        rb.AddForce(normalVector * jumpForce * 0.5f);

        //If jumping while falling, reset y velocity.
        if (rb.velocity.y < 0.5f)
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        else if (rb.velocity.y > 0)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump() => readyToJump = true;

    /// <summary>
    /// Handle all the basics related to camera movement
    /// </summary>
    public void Look() {
        int inverted = (InputManager.invertedAxis) ? -1 : 1;

        float sensM = (weapon.isAiming) ? InputManager.aimingSensitivityMultiplier : 1;

        //Handle the camera movement and look based on the inputs received by the user
        float mouseX = (InputManager.mousex * InputManager.sensitivity * Time.fixedDeltaTime + InputManager.controllerx * InputManager.controllerSensitivityX * Time.fixedDeltaTime) * inverted * sensM;
        float mouseY = (InputManager.mousey * InputManager.sensitivity * Time.fixedDeltaTime * inverted + InputManager.controllery * InputManager.controllerSensitivityY * Time.fixedDeltaTime * -inverted) * sensM;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations on:
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0); // the camera parent
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0); // the orientatio

        // Decide wether to use aim assist or not
        if (AimAssistHit() == null || !applyAimAssist || target == null || Vector3.Distance(target.position, transform.position) > maximumDistanceToAssistAim) return;
        // Get the direction to look at
        Vector3 direction = (AimAssistHit().position - transform.position).normalized;
        Quaternion targetRotation = transform.rotation * Quaternion.FromToRotation(transform.forward, direction);
        // Smoothly override our current camera rotation towards the selected enemy
        playerCam.transform.localRotation = Quaternion.Lerp(playerCam.transform.localRotation, targetRotation, Time.deltaTime * aimAssistSpeed);
    }

    /// <summary>
    /// Add friction force to the player when it´s not airborne
    /// Please note that it counters movement, since it goes in the opposite direction to velocity
    /// </summary>
    private void FrictionForce(float x, float y, Vector2 mag) {
        // Prevent from adding friction on an airborne body
        if (!grounded || InputManager.jumping) return;

        //Slow down sliding + prevent from infinite sliding
        if (InputManager.crouching) {
            rb.AddForce(acceleration * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        // Counter movement ( Friction while moving )
        // Prevent from sliding not on purpose
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) {
            rb.AddForce(acceleration * orientation.transform.right * Time.deltaTime * -mag.x * frictionForceAmount);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) {
            rb.AddForce(acceleration * orientation.transform.forward * Time.deltaTime * -mag.y * frictionForceAmount);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > currentSpeed) {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * currentSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook() {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    /// <summary>
    /// Determine wether this is determined as floor or not
    /// </summary>
    private bool IsFloor(Vector3 v) {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    /// <summary>
    /// Basically find everything the script needs to work
    /// </summary>
    private void GetAllReferences() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Instantiate(Resources.Load("InputManager"));
        InputManager.PlayerMovement = this.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        weapon = GetComponent<WeaponController>();
        stats = GetComponent<PlayerStats>();
    }

    private bool cancellingGrounded;

    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other) {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++) {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal)) {
                if (!grounded) events.OnLand.Invoke(); // We have just landed
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        float delay = 3f;
        if (!cancellingGrounded) {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    /// <summary>
    /// Returns the transform you want your camera to be sticked to
    /// </summary>
    private Transform AimAssistHit() {
        // Aim assist will work on enemies only, since we dont wanna snap our camera on any object around the environment
        // max range to snap
        float range = 40;
        // Max range depends on the weapon range if you are holding a weapon
        if (weapon.weapon != null) range = weapon.weapon.bulletRange;

        // Detect our potential transform
        RaycastHit hit;
        if (Physics.SphereCast(playerCam.transform.GetChild(0).position, aimAssistSensitivity, playerCam.transform.GetChild(0).transform.forward, out hit, range) && hit.transform.tag == "Enemy") {
            target = hit.collider.transform;
        } else target = null;
        // Return our target
        return target;
    }

    private void StopGrounded() => grounded = false;

    private void ResetStamina() => stamina = maxStamina;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Weapons") {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}