using CommonData.ServerData;

namespace DatabaseAccessService.Application.DTOs {
    public record ErrorMessagesDTO {
        public Dictionary<int, ErrorMessage> ErrorMessages { get; set; } = new();
    }
}