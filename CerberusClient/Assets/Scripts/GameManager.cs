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

    public List<GameObject> Players;
    public GameObject PlayerPrefab;

    public GameObject LoadingScreen;
    public TMP_Text LoadingText;

    private void Awake() {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start() {
        if (SteamManager.Initialized) {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
            //_localPlayerData.steamName = SteamFriends.GetPersonaName();
            //_localPlayerData.steamID = SteamUser.GetSteamID().ToString();
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback) {
        if (pCallback.m_bActive != 0) {
            Debug.Log("Steam Overlay has been activated");
        } else {
            Debug.Log("Steam Overlay has been closed");
        }
    }


    public void ShowLoadingScreen(bool bActive)
    {
        LoadingScreen.SetActive(bActive);
    }

    public void SpawnPlayer(string steamId,string steamName, int teamId, Vector3 spawnPos, Quaternion spawnRot)
    {
        var player = Instantiate(PlayerPrefab, spawnPos, spawnRot);
        player.name = steamId;
        var playerData = player.GetComponent<Player>();
        playerData.teamId = teamId;
        playerData.steamId = steamId;
        playerData.steamName = steamName;
        
        Players.Add(player);

        //TODO: Disable control over other players that arent your own

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameServerSend.SendSceneLoaded();
        LoadingText.text = "Waiting on other players...";
    }
}