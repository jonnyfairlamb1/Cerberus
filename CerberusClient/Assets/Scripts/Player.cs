using System;
using System.Timers;
using Assets.Scripts.Network.GameServer;
using NovaCore.Utils;
using TMPro;
using UnityEngine;
using LogType = NovaCore.Utils.LogType;

public class Player : MonoBehaviour {

    [SerializeField] private float _transformUpdateTimer = 1000f;
    private float _transformTimer;

    public GameObject _characterHead;
    public GameObject _characterHeadPrefab;
    public GameObject _characterLeftHand;
    public GameObject _characterLeftLeg;
    public GameObject _characterBody;
    public GameObject _characterRightHand;
    public GameObject _characterRightLeg;

    private bool isDead = false;

    public bool IsLocalControlled = false;
    public PlayerData PlayerData;

    void Start()
    {
        _transformTimer = _transformUpdateTimer;
    }

    void FixedUpdate()
    {
        GameServerSend.SendPlayerMove(this.gameObject);

        if (_transformTimer >= 0 && IsLocalControlled)
        {
            _transformTimer -= Time.deltaTime;
            
            _transformTimer = _transformUpdateTimer;
        }
    }

    public void Die(float power)
    {
        var ragdollController = GetComponent<RagdollController>();
        if (isDead)
            return;

        isDead = true;
        
        var head = Instantiate(_characterHeadPrefab, transform.position, transform.rotation);
        head.SetActive(true);
        _characterHead.GetComponent<SkinnedMeshRenderer>().enabled = false;


        //set rigidbody as active and give force
        var rb = _characterHeadPrefab.GetComponent<Rigidbody>();
        //ragdollController.EnableRagdoll(true);
        //rb.AddExplosionForce(power, _characterHeadPrefab.transform.position, 2f);
    }
}