using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using Steamworks;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance;
    public static InGameManager Instance {
        get => _instance;
        private set {
            if (_instance == null) {
                _instance = value;
            } else if (_instance != value) {
                Debug.Log($"{nameof(InGameManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }
    private void Awake() {
        Instance = this;

    }

    public Dictionary<string, GameObject> Players = new Dictionary<string, GameObject>();



    public void SpawnPlayer(string steamId, string steamName, int teamId, Vector3 spawnPos, Quaternion spawnRot)
    {
        GameObject player = null;

        if (steamId == GameManager.Instance.LocalPlayerSteamId) {
            //Enable control of player
            player = Instantiate(GameManager.Instance.LocalPlayerPrefab, spawnPos, spawnRot);
        } else {
            //Disable control
            player = Instantiate(GameManager.Instance.NetworkPlayerPrefab, spawnPos, spawnRot);
        }
        Players.Add(steamId, player);

        player.name = $"Player: {steamName}";

        var playerData = new PlayerData();
        playerData.TeamId = teamId;
        playerData.SteamId = steamId;
        playerData.SteamName = steamName;

        var playerComponent = player.GetComponentInChildren<Player>();
        playerComponent.PlayerData = playerData;
    }

}
