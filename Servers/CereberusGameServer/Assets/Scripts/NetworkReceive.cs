using System.Collections.Generic;
using NovaCore;
using Packets;
using NovaCore.Utils;
using UnityEngine;
using LogType = NovaCore.Utils.LogType;
using UnityEditor.PackageManager;

namespace Assets.Scripts {
    public class NetworkReceive {

        [MessageHandler((ushort)ClientPackets.C_Login)]
        private static void Packet_LoginConfirmed(ushort fromClientId, Message message)
        {
            string playerSteamId = message.GetString();
            string playerSteamName = message.GetString();
            GameManager.Instance.PlayerList[fromClientId].SteamId = playerSteamId;
            GameManager.Instance.PlayerList[fromClientId].SteamName = playerSteamName;
            GameManager.Instance.PlayerList[fromClientId].CompletedLoadIn = true;

            if (GameManager.Instance.GameState == GameState.PreLobby ||
                GameManager.Instance.GameState == GameState.Lobby)
                NetworkSend.PlayerJoinedLobby(GameManager.Instance.PlayerList);
            else
            {
                GameManager.Instance.SpawnPlayer(GameManager.Instance.PlayerList[fromClientId]);
                NetworkSend.PlayerJoinedGame(fromClientId, GameManager.Instance.PlayerList);
            }

        }

        [MessageHandler((ushort)ClientPackets.C_GameSceneLoaded)]
        private static void Packet_PlayerLoadedScene(ushort fromClientId, Message message)
        {
            GameManager.Instance.PlayerLoadedGameScene();
        }

        [MessageHandler((ushort)ClientPackets.C_PlayerTransformUpdate)]
        private static void Packet_PlayerTransformUpdate(ushort fromClientId, Message message) {

            var xPos = message.GetFloat();
            var yPos = message.GetFloat();
            var zPos = message.GetFloat();

            var xRot = message.GetFloat();
            var yRot = message.GetFloat();
            var zRot = message.GetFloat();
            var wRot = message.GetFloat();

            Vector3 position = new(xPos, yPos, zPos);
            Quaternion rotation = new(xRot, yRot, zRot, wRot);

            var movingCharacterTransform = GameManager.Instance.PlayerList[fromClientId].PlayerGameObject.transform;

            if (position == movingCharacterTransform.position)
                return;

            movingCharacterTransform.position = position;
            movingCharacterTransform.rotation = rotation;
        }
    }
}
