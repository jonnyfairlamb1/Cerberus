using CommonData.ServerData;
using System.Collections.Generic;

namespace CommonData.DTOs {
    public record GameServerDataDTO {
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public int GameServerId { get; set; }

        public Dictionary<int, Lobby> Lobbies { get; set; }
    }
}