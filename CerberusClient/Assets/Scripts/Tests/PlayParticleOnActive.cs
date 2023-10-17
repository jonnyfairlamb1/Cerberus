using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnActive : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    void OnAwake()
    {
        _particleSystem.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
