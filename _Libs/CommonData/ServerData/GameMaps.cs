namespace CommonData.ServerData {

    public class GameMaps {
        public string MapName { get; set; } = string.Empty;
        public string SceneName { get; set; } = string.Empty;
        public int GameMode { get; set; }
        public bool IsActive { get; set; }

        public GameMaps(string mapName, string sceneName, int gameMode, bool isActive) {
            MapName = mapName;
            SceneName = sceneName;
            GameMode = gameMode;
            IsActive = isActive;
        }
    }
}