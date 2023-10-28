using Assets.Scripts;
using System;using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using NovaCore.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using LogType = NovaCore.Utils.LogType;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
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

    public const int MaxPlayersInGame = 12;
    public const int MinPlayersToStartGame = 1;//4;
    public Dictionary<ushort, Player> PlayerList = new(MaxPlayersInGame);
    public float TimerCountdownValueSeconds = 10.0f; // Set the duration of the timer in seconds
    public GameObject PlayerPrefab;

    private GameState _gameState;
    private int _totalPlayersLoadedScene = 0;
    private LevelManager _levelManager;
    private string _levelName = "TestLevel";

    private int _team1Members = 0;
    private int _team2Members = 0;

    public GameState GameState
    {
        get { return _gameState; }
        set {
            if (_gameState != value)
            {
                _gameState = value;
            }
        }
    }

    void Start()
    {
        Instance = this;
        GameState = GameState.PreLobby;
        DontDestroyOnLoad(this);
    }

    public void NewPlayerJoined(ushort clientId)
    {
        NetworkSend.SendClientId(clientId, (int)_gameState, TimerCountdownValueSeconds);
        PlayerList.Add(clientId, new() {
            PlayerId = clientId
        });

        if (GameState == GameState.Lobby) //return out, dont need to do anything else for the lobby
            return;

        if (GameState == GameState.PreLobby)
        {
            if (PlayerList.Count > MinPlayersToStartGame - 1)//account for the list size
            {
                GoToLobby();
            }
        }

        if (GameState == GameState.PrepPhase || GameState == GameState.RoundStarted)
        {
            NetworkSend.GameStarted(_levelName);
            //find a team for that player
            PlayerList[clientId].TeamId = AssignPlayerTeam();
        }

    }

    void Update()
    {
        if (GameState == GameState.Lobby)
        {
            if (TimerCountdownValueSeconds >= 0) {
                TimerCountdownValueSeconds -= Time.deltaTime;
                Debug.Log("Timer: " + TimerCountdownValueSeconds.ToString("F2")); // Display timer with 2 decimal places
            } else {
                Debug.Log("Timer finished!");
                GameState = GameState.PrepPhase;
                StartGame();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameState == GameState.RoundStarted || GameState == GameState.PrepPhase) {
            NetworkSend.SendPlayerTransforms(PlayerList);
        }
    }

    private void GoToLobby()
    {
        GameState = GameState.Lobby;
        //Send everyone the timer
    }

    private void StartGame() {
        //Really basic split into teams
        for (ushort i = 0; i < PlayerList.Count; i++) {
            if (i % 2 == 0) {
                PlayerList.ElementAt(i).Value.TeamId = AssignPlayerTeam();
            } else {
                PlayerList.ElementAt(i).Value.TeamId = AssignPlayerTeam();
            }
        }

        NovaCoreLogger.Log(LogType.Debug, "Started game!");
        //Show loading screen
        //Load the level
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadSceneAsync(_levelName, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode arg1) {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.SetActiveScene(loadedScene);
        SceneManager.UnloadSceneAsync("GameLobby");

        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        foreach (var player in PlayerList.Values)
        {
            SpawnPlayer(player);
        }
        NetworkSend.GameStarted(_levelName);
    }

    public void SpawnPlayer(Player player)
    {
        _levelManager.GetSpawnPoint(player);
        var playerGo = Instantiate(PlayerPrefab, player.SpawnPosition, new(0, 0, 0, 0));
        playerGo.name = $"Player: {player.SteamName}";
        player.PlayerGameObject = playerGo;
    }

    private void EndGame()
    {
        GameState = GameState.RoundEnded;
        NetworkSend.GameEnded();
    }

    public void PlayerLoadedGameScene()
    {
        _totalPlayersLoadedScene++;
        if (_totalPlayersLoadedScene == PlayerList.Count)
        {
            NetworkSend.SendPlayerData(PlayerList);
        }
    }

    public void PlayerDisconnected(ushort clientId)
    {
        if (GameState == GameState.PreLobby || GameState == GameState.Lobby) {
            //Tell everyone to remove the player from the lobby screen
        }

        //Find the player and destroy them.
        //Broadcast to all players they left.
        //check if the game is now empty and return to lobby if it is.
    }

    private int AssignPlayerTeam()
    {
        int teamNumber = 0;
        if (_team1Members == _team2Members) //if teams are equal just assign to random team
        {
            Random rand = new();
            teamNumber = rand.Next(1, 2);
        }
        else if (_team1Members < _team2Members)
        {
            teamNumber = 1;
            
        }else if (_team1Members > _team2Members)
        {
            teamNumber = 2;
        }

        if(teamNumber == 1)
            _team1Members++;
        if (teamNumber == 2)
            _team2Members++;

        return teamNumber;
    }

}
