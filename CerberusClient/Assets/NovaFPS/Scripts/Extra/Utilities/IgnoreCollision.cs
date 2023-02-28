using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ignore collisions with player
/// Use it if you need to, but you might find it handy regarding to bullet shells on the ground.
/// They will interact with the world but not the player, hence we won�t suffer from extra unnecessary friction when walking on top of them.
/// </summary>
public class IgnoreCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}