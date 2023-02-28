using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    private Transform Player;

    private void Start() => Player = GameObject.FindGameObjectWithTag("Player").transform;

    private void Update() => transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
}