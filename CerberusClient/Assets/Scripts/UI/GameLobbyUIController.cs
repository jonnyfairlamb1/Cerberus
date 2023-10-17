using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Assets.Scripts;
using TMPro;
using UnityEngine;
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

    private Timer _countdownTimer;
    private int _countdownTimerValue;
    private List<TMP_Text> _buttonVoteText;
    [SerializeField]private GameObject _lobbyPanel;
    private List<GameObject> _lobbyList = new();

    void Awake()
    {
        Instance = this;
    }

    public void AddPlayerName(List<string> playerNames)
    {
        for (int i = 0; i < _lobbyList.Count; i++)
        {
            DestroyImmediate(_lobbyList[i]);
        }

        _lobbyList.Clear();

        foreach (var playerName in playerNames)
        {
            NameTextPrefab.GetComponent<TMP_Text>().text = playerName;
            NameTextPrefab.name = playerName;
            Instantiate(NameTextPrefab, PlayerNamePanel.transform);
        }
    }

    public void SetupLobby(GameState gameState, int countdownTimerValue)
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

    private void IsInLobby(int countdownTimerValue)
    {
        _countdownTimer = new Timer(countdownTimerValue);
        _countdownTimerValue = countdownTimerValue;
        _countdownTimer.Start();
    }
}
