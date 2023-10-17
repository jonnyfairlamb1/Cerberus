using Assets.Scripts;
using System;using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using NovaCore.Utils;
using UnityEngine;
using LogType = NovaCore.Utils.LogType;


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
    public int TimerCountdownValueSeconds = 5;//60;

    private Timer _lobbyCountDownTimer;

    private GameState _gameState;
    private int _totalPlayersLoadedScene = 0;

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
    }

    public void NewPlayerJoined(ushort clientId)
    {
        NetworkSend.SendClientId(clientId, (int)_gameState, TimerCountdownValueSeconds);
        PlayerList.Add(clientId, new()
        {
            PlayerId = clientId
        });

        if (PlayerList.Count > MinPlayersToStartGame-1)//account for the list size
        {
            GoToLobby();
        }
    }

    private void GoToLobby()
    {
        GameState = GameState.Lobby;
        _lobbyCountDownTimer = new Timer(TimerCountdownValueSeconds);
        _lobbyCountDownTimer.AutoReset = true;
        _lobbyCountDownTimer.Elapsed += StartGame;
        _lobbyCountDownTimer.Start();
        //Send everyone the timer
    }

    private void StartGame(object sender, ElapsedEventArgs e)
    {
        if (TimerCountdownValueSeconds > 0)
        {
            TimerCountdownValueSeconds--;
            return;
        }

        _lobbyCountDownTimer.Stop();
        _lobbyCountDownTimer.AutoReset = false;
        GameState = GameState.PrepPhase;

        //Really basic split into teams

        for (ushort i = 0; i < PlayerList.Count; i++) {
            if (i % 2 == 0)
            {
                PlayerList.ElementAt(i).Value.TeamId = 1;
            } else {
                PlayerList.ElementAt(i).Value.TeamId = 2;
            }
        }

        NovaCoreLogger.Log(LogType.Debug, "Started game!");
        NetworkSend.GameStarted(PlayerList, "TestLevel");
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
}
