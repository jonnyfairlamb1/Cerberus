using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inheriting from Interactable, this means you can interact with the door
/// Keep in mind that this is highly subject to change on future updates
/// </summary>
[RequireComponent(typeof(BoxCollider))] // Require a trigger collider to detect side
public class DoorInteractable : Interactable {

    [Tooltip("The parent of this object"), SerializeField]
    private Transform doorContainer;

    [Tooltip("How much you want to rotate the door"), SerializeField]
    private float openedDoorRotation;

    [Tooltip("rotation speed"), SerializeField]
    private float speed;

    private bool isOpened;

    private Quaternion closedRot;

    private int side = 1;

    private void Start() {
        // Initial settings
        isOpened = false;
        closedRot = doorContainer.rotation;
    }

    private void Update() {
        // if we opened it, open it
        if (isOpened) doorContainer.localRotation = Quaternion.Lerp(doorContainer.localRotation,
            Quaternion.Euler(new Vector3(doorContainer.localRotation.x, openedDoorRotation * side, doorContainer.localRotation.z)),
                Time.deltaTime * speed);
        // If we closed it, close it
        if (!isOpened) doorContainer.rotation = Quaternion.Lerp(doorContainer.rotation, closedRot, Time.deltaTime * speed);
    }

    /// <summary>
    /// Check for interaction. Overriding from Interactable.cs
    /// </summary>
    public override void Interact() {
        if (!isOpened) isOpened = true;
        else isOpened = false;
    }

    // Checking side, this is pretty much subject to change to a more solid system
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !isOpened) {
            side = -1;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && !isOpened) {
            side = 1;
        }
    }
}