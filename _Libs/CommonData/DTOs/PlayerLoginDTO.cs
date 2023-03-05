using CommonData.ServerData;

namespace CommonData.DTOs {
    public record PlayerLoginDTO {
        public DBPlayer Player { get; set; }
    }
}