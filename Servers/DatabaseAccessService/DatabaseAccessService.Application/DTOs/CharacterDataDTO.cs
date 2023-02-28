using CommonData.PlayerSendData;

namespace DatabaseAccessService.Application.DTOs {
    public record CharacterDataDTO {
        public Dictionary<int, BaseCharacter> characterData { get; set; } = new();
    }
}