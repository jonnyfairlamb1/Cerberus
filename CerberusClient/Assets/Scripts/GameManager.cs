using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    public bool RunWithoutLoginServer = false;
    public bool RunWithoutGameServer = false;

    private void Awake() {
        if (instance == null)
            instance = this;
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
}