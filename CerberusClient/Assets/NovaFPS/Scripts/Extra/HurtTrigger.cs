using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTrigger : MonoBehaviour
{
    [SerializeField] private float damage; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<PlayerStats>().Damage(damage); 
    }
}
