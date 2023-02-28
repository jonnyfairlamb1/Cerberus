using CommonData.ServerData;

namespace DatabaseAccessService.Application.DTOs {
    public record PlayerLoginDTO {
        public DBPlayer Player { get; set; }
    }
}