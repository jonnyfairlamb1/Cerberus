using NovaCore;
using NovaCore.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
    public static NetworkManager instance;

    public Client Client { get; private set; }

    private ushort _serverTick;

    public ushort ServerTick {
        get => _serverTick;
        private set {
            _serverTick = value;
            InterpolationTick = (ushort)(value - TicksBetweenPositionUpdates);
        }
    }

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
        Debug.Log("Connected to server");
        NetworkSend.SendLogin("Test","Test");
    }

    private void FailedToConnect(object sender, EventArgs e) {
        Debug.Log("Failed to connect and show error message");
    }

    public void ConnectToGameServer()
    {
        //Client.Disconnect();
        Client.Connect($"{ip}:{5101}");
    }
}