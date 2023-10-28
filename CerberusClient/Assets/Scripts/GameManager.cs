using Steamworks;
using System;
using System.Collections.Generic;
using Assets.Scripts.Network.GameServer;
using NovaCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using LogType = NovaCore.Utils.LogType;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance {
        get => _instance;
        private set {
            if (_instance == null) {
                _instance = value;
            } else if (_instance != value) {
                Debug.Log($"{nameof(GameManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    public bool RunWithoutLoginServer = false;
    public bool RunWithoutGameServer = false;

    public Dictionary<string, PlayerData> PlayerData = new();
    public GameObject NetworkPlayerPrefab;
    public GameObject LocalPlayerPrefab;

    public string LocalPlayerSteamId = "SteamId1234";
    public string LocalPlayerSteamName = "SteamName";

    private void Awake() {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start() {
        if (SteamManager.Initialized) {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
            //_localPlayerSteamName = SteamFriends.GetPersonaName();
            //_localPlayerSteamId = SteamUser.GetSteamID().ToString();
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback) {
        if (pCallback.m_bActive != 0) {
            Debug.Log("Steam Overlay has been activated");
        } else {
            Debug.Log("Steam Overlay has been closed");
        }
    }

}