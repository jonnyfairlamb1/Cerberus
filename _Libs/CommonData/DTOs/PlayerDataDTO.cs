using CommonData.ServerData;

namespace CommonData.DTOs {
    public record PlayerDataDTO {
        public DBPlayer Player { get; set; }
    }
}