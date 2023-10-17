using System.Collections.Generic;
using NovaCore;
using Packets;
using System.Linq;
using Assets.Scripts.Network.GameServer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

namespace Assets.Scripts.Network {

    public class GameServerReceiveMessages {

        [MessageHandler((ushort)GameServerPackets.GS_ClientId)]
        private static void Packet_LoginConfirmed(Message message)
        {
            ushort playerId = message.GetUShort();
            GameState gameState = (GameState)message.GetInt();
            int timerRemaining = message.GetInt();

            GameServerSend.SendLogin("SteamId", "SteamName");

            if (gameState == GameState.PrepPhase || gameState == GameState.RoundStarted)
            {
                //go to map name given
            }
            else
            {
                //go to lobby
                GameLobbyUIController.Instance.SetupLobby(gameState, timerRemaining);
            }
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerJoinedLobby)]
        private static void Packet_PlayerJoinedLobby(Message message)
        {
            int numberOfPlayers = message.GetInt();
            List<string> playerNames = new();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerNames.Add(message.GetString());
            }

            GameLobbyUIController.Instance.AddPlayerName(playerNames);
        }

        [MessageHandler((ushort)GameServerPackets.GS_GameStarted)]
        private static void Packet_GameStarted(Message message) {
            string mapName = message.GetString();

            GameManager.Instance.ShowLoadingScreen(true);
            SceneManager.sceneLoaded += GameManager.Instance.OnSceneLoaded;

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

                GameManager.Instance.SpawnPlayer(steamId, steamName, teamId, spawnPosition, spawnRotation);
            }

            GameManager.Instance.ShowLoadingScreen(false);
        }
    }
}