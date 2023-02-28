using Assets.Scripts.Entities;
using CerberusClient.Network.Data;
using NovaCoreNetworking;
using NovaCoreNetworking.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
    public static NetworkManager instance;

    public Client Client { get; private set; }

    public GameServerData _currentlyConnectedGameServer;

    private ushort _serverTick;

    public ushort ServerTick {
        get => _serverTick;
        private set {
            _serverTick = value;
            InterpolationTick = (ushort)(value - TicksBetweenPositionUpdates);
        }
    }

    private bool isConnectingToGameServer = false;

    public ushort InterpolationTick { get; private set; }

    private ushort _ticksBetweenPositionUpdates = 2;

    public ushort TicksBetweenPositionUpdates {
        get => _ticksBetweenPositionUpdates;
        private set {
            _ticksBetweenPositionUpdates = value;
            InterpolationTick = (ushort)(ServerTick - value);
        }
    }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    [Space(10)]
    [SerializeField] private ushort tickDivergenceTolerance = 1;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    private void Start() {
        NovaCoreLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;

        ServerTick = TicksBetweenPositionUpdates;
    }

    private void FixedUpdate() {
        Client.Update();
        ServerTick++;
    }

    private void OnApplicationQuit() {
        Client.Disconnect();
    }

    public void Connect() {
        Client.Connect($"{ip}:{port}");
        MenuManager.instance._loginText.text = "....Receiving player data from server.";
    }

    private void DidConnect(object sender, EventArgs e) {
        if (!isConnectingToGameServer) {
            NetworkSend.SendLogin(GameManager.instance._localPlayerData.steamID, GameManager.instance._localPlayerData.steamName);
        } else {
            NetworkSend.SendLobbyIdToGameServer(_currentlyConnectedGameServer);
        }
    }

    private void FailedToConnect(object sender, EventArgs e) {
        Debug.Log("Failed to connect and show error message");
        //UIManager.Singleton.BackToMain();
    }

    public void ConnectGameServer(GameServerData gameServerData) {
        isConnectingToGameServer = true;
        NovaCoreLogger.Log(NovaCoreNetworking.Utils.LogType.Debug, "Attemping connection to game server");
        Client.Connect($"{gameServerData.serverIp}:{gameServerData.serverPort}");
        _currentlyConnectedGameServer = gameServerData;
    }

    public void InstantiatePlayerCharacter(GamePlayerData gamePlayerData) {
        //TODO: create the gameobject using a skinDB class
        var playerCharacter = Instantiate(GameManager.instance._basePlayerPrefab, gamePlayerData.currentPosition, gamePlayerData.currentRotation);
        var playerData = playerCharacter.AddComponent<Player>();

        TMP_Text[] playerCharacterTextFields = playerCharacter.GetComponentsInChildren<TMP_Text>();

        if (gamePlayerData.playerId == GameManager.instance._localPlayerData.clientId) {
            var playerController = Instantiate(GameManager.instance._basePlayerControllerPrefab, playerCharacter.transform);
            var playerUi = Instantiate(GameManager.instance._playerUiPrefab, playerCharacter.transform);

            //Setup the ui elements
            var crosshair = playerUi.GetComponentInChildren<Crosshair>();
            var compass = playerUi.GetComponentInChildren<Compass>();
            var weaponController = playerController.GetComponentInChildren<WeaponController>();
            var interactManager = playerController.GetComponentInChildren<InteractManager>();
            var jumpMotionController = playerController.GetComponentInChildren<JumpMotion>();
            var jumpMotionUI = playerUi.GetComponentInChildren<JumpMotion>();
            var playerStats = playerController.GetComponentInChildren<PlayerStats>();

            var playerMovement = playerController.GetComponentInChildren<PlayerMovement>();
            crosshair.player = playerMovement;
            jumpMotionController.player = playerMovement;
            jumpMotionUI.player = playerMovement;

            compass.player = playerCharacter.transform;
            weaponController.inventoryContainer = GameObject.Find("InventoryContainer").GetComponent<CanvasGroup>();
            weaponController.bulletsUI = GameObject.Find("BulletsUI").GetComponent<TextMeshProUGUI>();
            weaponController.magazineUI = GameObject.Find("MagazineUI").GetComponent<TextMeshProUGUI>();
            weaponController.reloadUI = GameObject.Find("ReloadText").GetComponent<TextMeshProUGUI>();
            weaponController.lowAmmoUI = GameObject.Find("LowAmmoText").GetComponent<TextMeshProUGUI>();
            weaponController.currentWeaponDisplay = GameObject.Find("CurrentWeapon").GetComponent<Image>();
            interactManager.interactUI = GameObject.Find("InteractUI");
            playerStats.healthSlider = GameObject.Find("healthSlider").GetComponentInChildren<Slider>();
            playerStats.shieldSlider = GameObject.Find("shieldSlider").GetComponentInChildren<Slider>();
            playerStats.healthStatesEffect = GameObject.Find("HealthStatesEffect").GetComponentInChildren<Image>();

            var mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");
            for (int i = 0; i < mainCamera.Length; i++) {
                Destroy(mainCamera[i]);
            }

            GameManager.instance._characterSelectionUI.SetActive(false);
        }

        playerData.playerId = gamePlayerData.playerId;
        playerData.steamName = gamePlayerData.steamName;
        playerData.steamId = gamePlayerData.steamId;
        playerData.teamId = gamePlayerData.teamId;
        playerData.currentCharacter = gamePlayerData.currentCharacter;

        playerCharacter.name = $"{gamePlayerData.steamName}: {gamePlayerData.playerId}";
        playerData.UpdateTextFields(playerCharacterTextFields);
    }
}