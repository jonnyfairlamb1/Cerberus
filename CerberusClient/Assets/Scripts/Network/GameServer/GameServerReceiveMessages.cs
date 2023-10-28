using System;
using System.Collections.Generic;
using NovaCore;
using Packets;
using System.Linq;
using Assets.Scripts.Network.GameServer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

namespace Assets.Scripts.Network {

    public class GameServerReceiveMessages {

        [MessageHandler((ushort)GameServerPackets.GS_ClientId)]
        private static void Packet_LoginConfirmed(Message message)
        {
            ushort playerId = message.GetUShort();
            GameState gameState = (GameState)message.GetInt();
            float timerRemaining = message.GetFloat();
            GameServerSend.SendLogin(GameManager.Instance.LocalPlayerSteamId, GameManager.Instance.LocalPlayerSteamName);


            if (gameState == GameState.PreLobby || gameState == GameState.Lobby)
            {
                //go to lobby
                GameLobbyUIController.Instance.SetupLobby(gameState, timerRemaining);
            }

        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerJoinedLobby)]
        private static void Packet_PlayerJoinedLobby(Message message)
        {
            int numberOfPlayers = message.GetInt();

            //We get all the data back for all the players that are here.
            GameManager.Instance.PlayerData.Clear();
            for (int i = 0; i < numberOfPlayers; i++) {
                var playerName = message.GetString();
                var playerId = message.GetString();

                GameManager.Instance.PlayerData.Add(playerId, new() {
                    SteamName = playerName,
                    SteamId = playerId
                });
            }

            GameLobbyUIController.Instance.UpdateLobbyScreen();
        }

        /// <summary>
        /// Network player joined the game.
        /// </summary>
        /// <param name="message"></param>
        [MessageHandler((ushort)GameServerPackets.GS_ForeignPlayerJoinedGame)]
        private static void Packet_ForeignPlayerJoinedGame(Message message)
        {
            string steamName = message.GetString();
            string steamId = message.GetString();
            int teamId = message.GetInt();

            Vector3 currentPosition = new(message.GetFloat(), message.GetFloat(), message.GetFloat());
            Quaternion currentRotation = new(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());

            InGameManager.Instance.SpawnPlayer(steamId, steamName, teamId, currentPosition, currentRotation);
        }

        /// <summary>
        /// Local player joined the game so will receive more data.
        /// </summary>
        /// <param name="message"></param>
        [MessageHandler((ushort)GameServerPackets.GS_LocalPlayerJoinedGame)]
        private static void Packet_LocalPlayerJoinedGame(Message message)
        {
            var numberOfPlayers = message.GetInt();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                var steamName = message.GetString();
                var steamId = message.GetString();
                var teamId = message.GetInt();

                Vector3 currentPosition = new(message.GetFloat(), message.GetFloat(), message.GetFloat());
                Quaternion currentRotation = new(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());

                InGameManager.Instance.SpawnPlayer(steamId, steamName, teamId, currentPosition, currentRotation);
            }
        }

        [MessageHandler((ushort)GameServerPackets.GS_GameStarted)]
        private static void Packet_GameStarted(Message message) {
            string mapName = message.GetString();
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == mapName)
                return;

            GameLobbyUIController.Instance.ShowLoadingScreen(true);
            SceneManager.sceneLoaded += GameLobbyUIController.Instance.OnSceneLoaded;

            SceneManager.LoadSceneAsync(mapName, LoadSceneMode.Additive);
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerGameData)]
        private static void Packet_PlayerGameData(Message message) {
            SceneManager.UnloadSceneAsync("MainMenu");
            int numberOfPlayers = message.GetInt();

            for (int i = 0; i < numberOfPlayers; i++) {
                string steamId = message.GetString();
                string steamName = message.GetString();
                int teamId = message.GetInt();

                Vector3 spawnPosition = new(message.GetFloat(), message.GetFloat(), message.GetFloat());
                Quaternion spawnRotation = new(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());

                InGameManager.Instance.SpawnPlayer(steamId, steamName, teamId, spawnPosition, spawnRotation);
            }

            GameLobbyUIController.Instance.ShowLoadingScreen(false);
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerTransformUpdate)]
        private static void Packet_PlayerTransform(Message message) {

            Debug.Log("Received transform update");
            int numberOfPlayers = message.GetInt();

            try
            {
                for (int i = 0; i < numberOfPlayers; i++) {
                    string steamId = message.GetString();

                    Vector3 currentPosition = new(message.GetFloat(), message.GetFloat(), message.GetFloat());
                    Quaternion currentRotation = new(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());

                    if (steamId == GameManager.Instance.LocalPlayerSteamId) //cant check before reading position data or steamId value gets corrupted.
                        continue;


                    InGameManager.Instance.Players[steamId].transform.position = currentPosition;
                    InGameManager.Instance.Players[steamId].transform.rotation = currentRotation;
                }
            }
            catch (Exception e)
            {
                //Player hasnt loaded in yet
            }
            
        }
    }
}