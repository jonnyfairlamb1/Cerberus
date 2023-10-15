using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace CommonData.Extensions {

    public static class PlayerDataConversions {

        public static PlayerData ConvertDBToPlayerData(this DBPlayer dbPlayer) {
            PlayerData playerData = new PlayerData(dbPlayer.SteamName, dbPlayer.SteamID, dbPlayer.PlayerLevel);
            return playerData;
        }
    }
}