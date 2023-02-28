using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    public PlayerData _localPlayerData = new();

    public Dictionary<int, BaseWeapon> _baseWeaponDict = new();
    public Dictionary<int, BaseCharacter> _characters = new();

    public bool isInGame = false;

    public GameObject _basePlayerPrefab;
    public GameObject _basePlayerControllerPrefab;
    public GameObject _playerUiPrefab;
    public GameObject _characterSelectionUI;

    public bool RunWithoutLoginServer = false;
    public bool RunWithoutGameServer = false;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    private void Start() {
        if (SteamManager.Initialized) {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
            _localPlayerData.steamID = GenerateRandomString(10);
            _localPlayerData.steamName = GenerateRandomString(10);
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

    private string GenerateRandomString(int length) {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++) {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        return finalString;
    }
}