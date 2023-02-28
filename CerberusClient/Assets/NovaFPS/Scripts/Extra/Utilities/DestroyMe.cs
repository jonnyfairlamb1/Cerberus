using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {
    public float timeToDestroy;

    private void Start() {
        Invoke("DestroyMeObj", timeToDestroy);
    }

    // Update is called once per frame
    private void DestroyMeObj() {
        Destroy(this.gameObject);
    }
}