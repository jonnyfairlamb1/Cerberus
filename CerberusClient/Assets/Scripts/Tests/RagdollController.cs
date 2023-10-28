using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    // colliders that needs to be enabled when not using ragdoll
    public Collider[] colliderToEnable;

    // rigidbody that is  activated when not using ragdoll
    Rigidbody rb;

    // all colliders that are activated when using ragdoll
    Collider[] allCollider;

    // all the rigidbodies used by ragdoll
    List<Rigidbody> ragdollRigidBodies = new();

    // animator used to controll different animation state of the character
    public Animator anim;



    void Awake()
    {
        Init();
        EnableRagdoll(false);
    }

    /// <summary>
    /// this stores reference of all the collider and attached rigidbodies used by ragdoll
    /// </summary>
    private void Init() {
        anim = GetComponent<Animator>();

        var allRigidbodies = GetComponentsInChildren<Rigidbody>();
        var allColliders = GetComponentsInChildren<Collider>();

        foreach (var rb in allRigidbodies) {
            ragdollRigidBodies.Add(rb); // add to list
        }

        foreach (var collider in allColliders)
        {
            collider.enabled = false;
        }

        allCollider = allColliders.ToArray();
    }

    public void EnableRagdoll(bool enableRagdoll) {
        anim.enabled = !enableRagdoll;

        foreach (var collider in allCollider)
        {
            collider.enabled = enableRagdoll;
        }

        foreach (var ragdollRigidBody in ragdollRigidBodies) {
            ragdollRigidBody.useGravity = enableRagdoll; // make rigidbody use gravity if ragdoll is active
            ragdollRigidBody.isKinematic = !enableRagdoll; // enable or disable kinematic accordig to enableRagdoll variable
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) {
            EnableRagdoll(true);
        }
    }
}
