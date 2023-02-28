using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace CommonData.Extensions {

    public static class PlayerDataConversions {

        public static PlayerData ConvertDBToPlayerData(this DBPlayer dbPlayer) {
            PlayerData playerData = new PlayerData(dbPlayer.SteamName, dbPlayer.SteamID, dbPlayer.PlayerLevel, dbPlayer.BattlepassLevel, dbPlayer.Currency);
            return playerData;
        }

        public static PlayerLobbyData ConvertLobbyToPlayerLobbyData(this Lobby lobby) {
            if (lobby == null) return null;
            PlayerLobbyData playerLobbyData = new(lobby.LobbyID, lobby.ServerIp, lobby.ServerPort, lobby.GameMap.MapName, lobby.GameMode.Abbreviation, lobby.GameMode.GameModeName);
            return playerLobbyData;
        }
    }
}