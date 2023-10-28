using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Assets.Scripts;
using Assets.Scripts.Network.GameServer;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLobbyUIController : MonoBehaviour
{

    private static GameLobbyUIController _instance;
    public static GameLobbyUIController Instance {
        get => _instance;
        private set {
            if (_instance == null) {
                _instance = value;
            } else if (_instance != value) {
                Debug.Log($"{nameof(GameLobbyUIController)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    public List<Button> VotingButtons;
    public GameObject NameTextPrefab;
    public TMP_Text CountdownTimerText;
    public GameObject PlayerNamePanel;

    public GameObject LoadingScreen;
    public TMP_Text LoadingText;

    private Timer _countdownTimer;
    private float _countdownTimerValue;
    private List<TMP_Text> _buttonVoteText;
    [SerializeField]private GameObject _lobbyPanel;
    private List<GameObject> _lobbyList = new();

    void Awake()
    {
        Instance = this;
    }

    public void ShowLoadingScreen(bool bActive) {
        LoadingScreen.SetActive(bActive);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.SetActiveScene(scene);

        GameServerSend.SendSceneLoaded();
        LoadingText.text = "Waiting on other players...";
    }

    public void UpdateLobbyScreen()
    {
        for (int i = 0; i < _lobbyList.Count; i++)
        {
            DestroyImmediate(_lobbyList[i]);
        }

        _lobbyList.Clear();

        foreach (var playerData in GameManager.Instance.PlayerData.Values)
        {
            var nameTextObj = Instantiate(NameTextPrefab, PlayerNamePanel.transform);
            nameTextObj.GetComponent<TMP_Text>().text = playerData.SteamName;
            _lobbyList.Add(nameTextObj);
        }
    }

    public void SetupLobby(GameState gameState, float countdownTimerValue)
    {
        _lobbyPanel.SetActive(true);
        if (gameState == GameState.PreLobby)
            IsInPreLobby();
        else
            IsInLobby(countdownTimerValue);
    }

    private void IsInPreLobby()
    {
        CountdownTimerText.text = "Waiting for more players to join...";
    }

    private void IsInLobby(float countdownTimerValue)
    {
        _countdownTimer = new Timer(countdownTimerValue);
        _countdownTimerValue = countdownTimerValue;
        _countdownTimer.Start();
    }
}
