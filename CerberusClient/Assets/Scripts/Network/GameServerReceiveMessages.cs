using Assets.Scripts.Entities;
using NovaCoreNetworking;
using Packets;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Network {

    public class GameServerReceiveMessages {

        [MessageHandler((ushort)GameServerPackets.GS_ClientId)]
        private static void Packete_GameServerClientId(Message message) {
            ushort clientId = message.GetUShort();
            GameManager.instance._localPlayerData.clientId = clientId;
        }

        [MessageHandler((ushort)GameServerPackets.GS_ErrorMessage)]
        private static void Packet_GameServerErrorMessage(Message message) {
            string error = message.GetString();
            Debug.LogError(error);
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerJoinedLobby)]
        private static void Packet_PlayerJoinedLobby(Message message) {
            string steamName = message.GetString();
            string steamId = message.GetString();
            int playerId = message.GetInt();
            int teamId = message.GetInt();
            float posX = message.GetFloat();
            float posY = message.GetFloat();
            float posZ = message.GetFloat();
            float rotX = message.GetFloat();
            float rotY = message.GetFloat();
            float rotZ = message.GetFloat();
            float rotW = message.GetFloat();

            GamePlayerData gamePlayerData = new(steamName, steamId, playerId, teamId, new(posX, posY, posZ), new(rotX, rotY, rotZ, rotW));

            NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData.Add(gamePlayerData);
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerLeftLobby)]
        private static void Packet_PlayerLeftLobby(Message message) {
            //TODO: add player left message to game chat.
            string steamName = message.GetString();
            string steamId = message.GetString();
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerTransformUpdate)]
        private static void Packet_PlayerTransformUpdate(Message message) {
        }

        [MessageHandler((ushort)GameServerPackets.GS_PlayerChoseCharacter)]
        private static void Packet_PlayerChoseCharacter(Message message) {
            string steamId = message.GetString();
            int chosenCharacterId = message.GetInt();
            int chosenCharacterSkinId = message.GetInt();

            BaseCharacter chosenCharacter = GameManager.instance._characters[chosenCharacterId];
            CharacterSkin chosenCharacterSkin = GameManager.instance._characters[chosenCharacterId].characterSkins.Single(x => x.SkinId == chosenCharacterSkinId);

            for (int i = 0; i < NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData.Count; i++) {
                if (NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData[i].steamId == steamId) {
                    NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData[i].currentCharacter = chosenCharacter;
                    NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData[i].currentCharacterSkin = chosenCharacterSkin;

                    NetworkManager.instance.InstantiatePlayerCharacter(NetworkManager.instance._currentlyConnectedGameServer.gamePlayerData[i]);
                }
            }
        }

        [MessageHandler((ushort)GameServerPackets.GS_GameStarted)]
        private static void Packet_GameStarted(Message message) {
        }

        [MessageHandler((ushort)GameServerPackets.GS_GameEnded)]
        private static void Packet_GameEneded(Message message) {
        }
    }
}