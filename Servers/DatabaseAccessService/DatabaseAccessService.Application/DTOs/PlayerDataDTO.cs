using CommonData.ServerData;

namespace DatabaseAccessService.Application.DTOs {
    public record PlayerDataDTO {
        public DBPlayer Player { get; set; }
    }
}