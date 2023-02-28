using UnityEngine;

/// <summary>
/// Keep camera in place
/// </summary>
public class MoveCamera : MonoBehaviour {

    [Tooltip("Reference to our head = height of the camera"), SerializeField]
    private Transform head;

    private void Update() => transform.position = head.transform.position;
}