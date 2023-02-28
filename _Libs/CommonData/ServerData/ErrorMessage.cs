using System.Text.Json.Serialization;

namespace CommonData.ServerData {

    public class ErrorMessage {

        [JsonPropertyName("errorId")]
        public int errorId { get; set; }

        [JsonPropertyName("playerErrorMessage")]
        public string playerErrorMessage { get; set; }

        [JsonPropertyName("internalErrorMessage")]
        public string internalErrorMessage { get; set; }

        [JsonPropertyName("errorLevel")]
        public int errorLevel { get; set; }

        public ErrorMessage(int errorId, string playerErrorMessage, string internalErrorMessage, int errorLevel) {
            this.errorId = errorId;
            this.playerErrorMessage = playerErrorMessage;
            this.internalErrorMessage = internalErrorMessage;
            this.errorLevel = errorLevel;
        }

        public ErrorMessage() {
        }
    }
}