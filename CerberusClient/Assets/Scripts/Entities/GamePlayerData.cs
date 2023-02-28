using UnityEngine;

namespace Assets.Scripts.Entities {

    public class GamePlayerData : MonoBehaviour {
        public string steamName;
        public string steamId;
        public int playerId;
        public int teamId;
        public Vector3 currentPosition;
        public Quaternion currentRotation;

        public BaseCharacter currentCharacter;
        public CharacterSkin currentCharacterSkin;

        public GamePlayerData(string steamName, string steamId, int playerId, int teamId, Vector3 currentPosition, Quaternion currentRotation) {
            this.steamName = steamName;
            this.steamId = steamId;
            this.playerId = playerId;
            this.teamId = teamId;
            this.currentPosition = currentPosition;
            this.currentRotation = currentRotation;
        }
    }
}