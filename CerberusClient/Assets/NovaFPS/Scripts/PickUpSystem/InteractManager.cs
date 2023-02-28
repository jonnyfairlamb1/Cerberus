using NovaFPS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractManager : MonoBehaviour {

    [System.Serializable]
    public class Events { public UnityEvent OnInteract; }

    [Tooltip("Attach your main camera"), SerializeField] private Camera mainCamera; // Attach your main camera

    private Transform orientation;

    [Tooltip("Object with the same height as your camera, used to orientate the player."), SerializeField] private LayerMask mask;

    private GameObject lookingAt;

    [Tooltip("Attach the generic pickeable object here"), SerializeField] private Pickeable weaponGenericPickeable;

    [Tooltip("Distance from the player where the pickeable will be instantiated"), SerializeField] private float droppingDistance;

    [SerializeField, Tooltip("How much time player has to hold the interact button in order to successfully interact")] private float progressRequiredToInteract;

    [HideInInspector] public float progressElapsed;

    [HideInInspector] public bool alreadyInteracted;

    [Tooltip("Adjust the interaction interval, the lower, the faster you will be able to interact"), Range(.2f, .7f), SerializeField] private float interactInterval = .4f;

    [Tooltip("Attach the UI you want to use as your interaction UI"), SerializeField] public GameObject interactUI;

    [Tooltip("Inside the interact UI, this is the text that will display the object you want to interact with " +
        "or any custom method you would like."), SerializeField]
    private TextMeshProUGUI interactText;

    public Events events;

    private void Start() {
        alreadyInteracted = false;
        interactUI.SetActive(false);
        orientation = GetComponent<PlayerMovement>().orientation;
        mainCamera = GetComponent<WeaponController>().mainCamera;
    }

    private void Update() {
        if (alreadyInteracted || PlayerStates.instance.CurrentState == PlayerStates.instance._States.Die()) return;
        DetectPickeable();
        DetectInput();
    }

    private void DetectPickeable() {
        RaycastHit hit;
        //Detect an interactable via raycast
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 4f, mask)) {
            // Able to interact with it
            hit.collider.GetComponent<Interactable>().interactable = true;
            lookingAt = hit.collider.gameObject;
            interactUI.SetActive(true);
            interactText.text = lookingAt.GetComponent<Interactable>().interactText;
        } else {
            if (lookingAt != null) {
                lookingAt.GetComponent<Interactable>().interactable = false;
                lookingAt = null;
                interactUI.SetActive(false);
            }
        }
    }

    private void DetectInput() {
        WeaponController wcon = GetComponent<WeaponController>();

        // Handle dropping
        if (InputManager.dropping && wcon.weapon != null && !wcon.Reloading) {
            WeaponPickeable pick = Instantiate(weaponGenericPickeable, orientation.position + orientation.forward * droppingDistance, orientation.rotation) as WeaponPickeable;
            pick.dropped = true;
            pick.GetComponent<Rigidbody>().AddForce(orientation.forward * 4, ForceMode.Impulse);
            pick.GetComponent<Rigidbody>().AddForce(orientation.up * -2, ForceMode.Impulse);
            float random = Random.Range(-1f, 1f);
            pick.GetComponent<Rigidbody>().AddTorque(new Vector3(random, random, random) * 10);

            pick.currentBullets = wcon.id.bulletsLeftInMagazine;
            pick.totalBullets = wcon.id.totalBullets;
            pick.weapon = wcon.weapon;
            pick.GetVisuals();

            wcon.weapon = null;
            wcon.slots[wcon.currentWeapon].weapon = null;
            Destroy(wcon.inventory[wcon.currentWeapon]);
        }

        if (lookingAt == null) {
            progressElapsed = -.01f;
            return;
        } // If we dont detect an interactable then dont continue
        // However if we detected an interactable + we pressing the interact button, then:
        if (InputManager.interacting) {
            progressElapsed += Time.deltaTime;
            if (progressRequiredToInteract > 0) {
                interactUI.transform.Find("Progress").gameObject.SetActive(true);
                interactUI.transform.Find("Progress").GetComponent<UnityEngine.UI.Image>().fillAmount = progressElapsed / progressRequiredToInteract;
            }
        }
        if (!InputManager.interacting) {
            progressElapsed = -.01f;
            interactUI.transform.Find("Progress").gameObject.SetActive(false);
        }
        // Interact
        if (progressElapsed >= progressRequiredToInteract) {
            interactUI.transform.Find("Progress").gameObject.SetActive(false);
            progressElapsed = -.01f;
            events.OnInteract.Invoke(); // Call our event
            // prevent from spamming
            alreadyInteracted = true;
            // Perform any interaction you may like Please note that classes that inherit from
            // interactable can override the virtual void Interact()
            lookingAt.GetComponent<Interactable>().Interact();
            // Prevent from spamming but let the user interact again
            Invoke("ResetInteractTimer", interactInterval);
            // Manage UI
            interactUI.SetActive(false);
            lookingAt = null;
        }
    }

    private void ResetInteractTimer() => alreadyInteracted = false;
}