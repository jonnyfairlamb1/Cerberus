using CommonData.PlayerSendData;
using System.Collections.Generic;

namespace CommonData.DTOs {
    public record CharacterDataDTO {
        public Dictionary<int, BaseCharacter> characterData { get; set; } = new();
    }
}