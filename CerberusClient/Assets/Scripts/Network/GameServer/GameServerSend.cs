
using NovaCore;
using NovaCore.Utils;
using Packets;
using Steamworks;
using System;
using UnityEngine;

namespace Assets.Scripts.Network.GameServer {
    public static class GameServerSend {

        public static void SendLogin(string steamId, string steamName)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_Login);
            message.AddString(steamId);
            message.AddString(steamName);
            NetworkManager.instance.Client.Send(message);
        }

        public static void SendSceneLoaded()
        {
            NetworkManager.instance.Client.Send(Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_GameSceneLoaded));
        }

        public static void SendPlayerMove(GameObject gameObject)
        {
            var message = Message.Create(MessageSendMode.Unreliable, (ushort)ClientPackets.C_PlayerTransformUpdate);

            message.AddFloat(gameObject.transform.position.x);
            message.AddFloat(gameObject.transform.position.y);
            message.AddFloat(gameObject.transform.position.z);

            message.AddFloat(gameObject.transform.rotation.x);
            message.AddFloat(gameObject.transform.rotation.y);
            message.AddFloat(gameObject.transform.rotation.z);
            message.AddFloat(gameObject.transform.rotation.w);

            NovaCoreLogger.Log(NovaCore.Utils.LogType.Debug, "Sending transform update");

            NetworkManager.instance.Client.Send(message);
        }
    }
}
