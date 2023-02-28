namespace DatabaseAccessService.Application.DTOs {
    public record JoinRandomGameDTO {
        public int LobbyID { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
        public string MapName { get; set; }
        public string Abbreviation { get; set; }
        public string GameMode { get; set; }
    }
}