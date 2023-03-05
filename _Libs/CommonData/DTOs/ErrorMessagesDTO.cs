using CommonData.ServerData;
using System.Collections.Generic;

namespace CommonData.DTOs {
    public record ErrorMessagesDTO {
        public Dictionary<int, ErrorMessage> ErrorMessages { get; set; } = new();
    }
}