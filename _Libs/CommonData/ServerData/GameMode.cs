namespace CommonData.ServerData {

    public class GameMode {
        public int GameModeId { get; set; }
        public string Abbreviation { get; set; }
        public string GameModeName { get; set; }
        public string Description { get; set; }
        public int AmountOfTeams { get; set; }
        public int TeamSize { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxSpectators { get; set; }
        public bool CanJoinWithFriends { get; set; }

        public GameMode(int gameModeId, string abbreviation, string gameModeName, string description, int amountOfTeams,
            int teamSize, int maxPlayers, int maxSpectators, bool canJoinWithFriends) {
            GameModeId = gameModeId;
            Abbreviation = abbreviation;
            GameModeName = gameModeName;
            Description = description;
            AmountOfTeams = amountOfTeams;
            TeamSize = teamSize;
            MaxPlayers = maxPlayers;
            MaxSpectators = maxSpectators;
            CanJoinWithFriends = canJoinWithFriends;
        }
    }
}