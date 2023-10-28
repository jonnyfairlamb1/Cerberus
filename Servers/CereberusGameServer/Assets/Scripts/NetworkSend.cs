

using System;
using System.Collections.Generic;
using System.Diagnostics;
using NovaCore;
using NovaCore.Utils;
using Packets;
using UnityEditor.PackageManager;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts {
    public static class NetworkSend {

        public static void SendToAllInLobby(Message message)
        {
            foreach (var player in GameManager.Instance.PlayerList)
            {
                NetworkManager.Instance.Server.Send(message, player.Value.PlayerId);
            }
        }

        public static void SendToAllInLobbyExcept(ushort exception, Message message)
        {
            foreach (var player in GameManager.Instance.PlayerList) {
                if(player.Value.PlayerId != exception)
                    NetworkManager.Instance.Server.Send(message, player.Value.PlayerId);
            }
        }

        public static void SendClientId(ushort clientId, int currentGameState, float countDownTimerRemaining) {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_ClientId);
            message.Add(clientId);
            message.Add(currentGameState);
            message.Add(countDownTimerRemaining);
            NetworkManager.Instance.Server.Send(message, clientId);
        }

        public static void PlayerJoinedLobby(Dictionary<ushort, Player> players)
        {
            //Tell everyone else someone has joined
            var message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerJoinedLobby);
            message.Add(players.Count);

            foreach (var player in players)
            {
                message.Add(player.Value.SteamName);
                message.Add(player.Value.SteamId);
            }
            SendToAllInLobby(message);
        }

        public static void PlayerJoinedGame(ushort joiningPlayer, Dictionary<ushort, Player> players)
        {
            //Message to send to everyone but the joining player
            var message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_ForeignPlayerJoinedGame);
            message.AddString(players[joiningPlayer].SteamName);
            message.AddString(players[joiningPlayer].SteamId);
            message.AddInt(players[joiningPlayer].TeamId);

            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.x);
            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.y);
            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.z);

            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.x);
            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.y);
            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.z);
            message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.w);

            SendToAllInLobbyExcept(joiningPlayer, message);

            //Send only to the joining player.
            message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_LocalPlayerJoinedGame);

            message.AddInt(players.Count);
            foreach (var player in players.Values)
            {
                message.AddString(player.SteamName);
                message.AddString(player.SteamId);
                message.AddInt(player.TeamId);

                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.x);
                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.y);
                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.position.z);

                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.x);
                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.y);
                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.z);
                message.AddFloat(players[joiningPlayer].PlayerGameObject.transform.rotation.w);

            }

            NetworkManager.Instance.Server.Send(message, joiningPlayer);
        }

        public static void GameStarted(string mapName)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_GameStarted);
            message.Add(mapName);
            SendToAllInLobby(message);
        }

        public static void SendPlayerData(Dictionary<ushort, Player> playerData)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerGameData);
            message.Add(playerData.Count);

            foreach (var player in playerData.Values) {
                message.Add(player.SteamId);
                message.Add(player.SteamName);
                message.Add(player.TeamId);
                //position
                message.Add(player.SpawnPosition.x);
                message.Add(player.SpawnPosition.y);
                message.Add(player.SpawnPosition.z);
                //rotation
                message.Add(0f);
                message.Add(0f);
                message.Add(0f);
                message.Add(0f);
            }

            SendToAllInLobby(message);
        }

        public static void GameEnded()
        {

        }

        public static void SendPlayerTransforms(Dictionary<ushort, Player> players)
        {
            var message = Message.Create(MessageSendMode.Unreliable, (ushort)GameServerPackets.GS_PlayerTransformUpdate);
            message.Add(players.Count);

            foreach (var player in players.Values) {

                if(!player.CompletedLoadIn || player.PlayerGameObject == null)
                    continue;

                message.Add(player.SteamId);
                //position
                message.Add(player.PlayerGameObject.transform.position.x);
                message.Add(player.PlayerGameObject.transform.position.y);
                message.Add(player.PlayerGameObject.transform.position.z);
                //rotation
                message.Add(player.PlayerGameObject.transform.rotation.x);
                message.Add(player.PlayerGameObject.transform.rotation.y);
                message.Add(player.PlayerGameObject.transform.rotation.z);
                message.Add(player.PlayerGameObject.transform.rotation.w);
            }
            SendToAllInLobby(message);
            Debug.Log("Sent transform update");
        }
    }
}
