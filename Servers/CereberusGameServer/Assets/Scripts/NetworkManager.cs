using Assets.Scripts;
using NovaCore;
using NovaCore.Utils;
using Packets;
#if !UNITY_EDITOR
using System;
#endif
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    public static NetworkManager Instance {
        get => _instance;
        private set {
            if (_instance == null) {
                _instance = value;
            }else if (_instance != value) {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;
    [SerializeField] private GameObject playerPrefab;

    public GameObject PlayerPrefab => playerPrefab;

    public Server Server { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

#if UNITY_EDITOR
        NovaCoreLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#else
        Console.Title = "GameServer";
        Console.Clear();
        Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
        NovaCoreLogger.Initialize(Debug.Log, true);
#endif

        Server = new Server();
        Server.ClientConnected += NewPlayerConnected;
        Server.ClientDisconnected += PlayerDisconnected;

        Server.Start(port, maxClientCount);
    }

    private void FixedUpdate() {
        Server.Update();
    }

    private void OnApplicationQuit() {
        Server.Stop();

        Server.ClientConnected -= NewPlayerConnected;
        Server.ClientDisconnected -= PlayerDisconnected;
    }

    private void NewPlayerConnected(object sender, ServerConnectedEventArgs e) {
        GameManager.Instance.NewPlayerJoined(e.Client.Id);
    }

    private void PlayerDisconnected(object sender, ServerDisconnectedEventArgs e) {
        //Destroy(Player.List[e.Client.Id].gameObject);
    }
}
