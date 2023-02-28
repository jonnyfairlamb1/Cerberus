using CommonData.Entities;
using CommonData.ServerData;
using System.Collections.Generic;

namespace CommonData.PlayerSendData {

    public class BaseCharacter {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int Health { get; set; }
        public int RunSpeed { get; set; }
        public float FootstepVolume { get; set; }

        public CharacterEnum CharacterEnum { get; set; }

        public List<BaseCharacterAbility> characterAbilities { get; set; } = new();
        public List<CharacterSkins> characterSkins { get; set; } = new();

        public BaseCharacter(int characterId, string characterName, int health, int runSpeed, float footstepVolume, CharacterEnum characterEnum) {
            CharacterId = characterId;
            CharacterName = characterName;
            Health = health;
            RunSpeed = runSpeed;
            FootstepVolume = footstepVolume;
            CharacterEnum = characterEnum;
        }

        public BaseCharacter() {
        }
    }
}