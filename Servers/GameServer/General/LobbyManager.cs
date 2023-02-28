using CommonData.GameServer;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using GameServer.Networking;

namespace GameServer.General {

    public class LobbyManager {
        public Lobby owningLobby;

        public LobbyManager(Lobby lobby) {
            this.owningLobby = lobby;
        }

        public GamePlayerData? NewPlayerJoinedLobby(ushort clientId, DBPlayer dBPlayer) {
            owningLobby.Players.Add(dBPlayer);

            for (int i = 0; i < owningLobby.LobbyTeams.Count; i++) {
                for (int x = 0; x < owningLobby.LobbyTeams.ElementAt(i).Value.Length; x++) {
                    if (owningLobby.LobbyTeams.ElementAt(i).Value[x] == null) {
                        //TODO: get spawn position and rotation
                        GamePlayerData gamePlayerData = new(clientId, dBPlayer, i, new(0, 0, 0), new(0, 0, 0, 0), PlayerRole.Player);
                        owningLobby.LobbyTeams.ElementAt(i).Value[x] = dBPlayer;

                        owningLobby.PlayersGameData.Add(gamePlayerData);
                        Console.WriteLine($"Player: {dBPlayer.SteamID} Joined team {i}");
                        return gamePlayerData;
                    }
                }
            }

            return null;
        }

        public void ShouldGameStart() {
        }

        public void ShouldGameStop() {
            NetworkSend.SendGameStarted(owningLobby);
        }

        public void GameEnded(int winningTeam) {
            NetworkSend.SendGameEnded(owningLobby);
        }

        public void UpdatePlayerTransforms() {
            NetworkSend.SendPlayerTransformUpdate(owningLobby);
        }

        public void PlayerChoseCharacter(string steamId, int characterId) {
            for (int i = 0; i < owningLobby.Players.Count; i++) {
                if (owningLobby.Players[i].SteamID == steamId) {
                    int equippedSkin = owningLobby.Players[i].GetEquippedSkinForCharacterId(characterId);

                    //TODO: Error check this
                    if (equippedSkin != -1) NetworkSend.SendPlayerChoseCharacter(owningLobby, steamId, characterId, equippedSkin);
                    return;
                }
            }
        }

        public void PlayerTookHealing(GamePlayerData healedPlayer, GamePlayerData playerDidHealing, BaseCharacterAbility characterAbility) {
            healedPlayer.currentHealth += characterAbility.Healing;
            if (healedPlayer.currentHealth > healedPlayer.maxHealth) healedPlayer.currentHealth = healedPlayer.maxHealth;

            playerDidHealing.healingDone += characterAbility.Healing;
            healedPlayer.healingTaken += characterAbility.Healing;

            NetworkSend.SendPlayerTookHealing(owningLobby, healedPlayer);
        }

        public void PlayerTookDamage(GamePlayerData playerDamageTaken, GamePlayerData playerDamageDealer,
            BaseCharacterAbility characterAbility = null, BaseWeapon weapon = null) {
            if (characterAbility != null) {
                playerDamageTaken.currentHealth -= characterAbility.Damage;
                playerDamageDealer.damageDone += characterAbility.Damage;

                if (playerDamageTaken.currentHealth <= 0) PlayerDied(playerDamageTaken, playerDamageDealer);

                playerDamageTaken.damageTakenByPlayer.Add(DateTime.Now, playerDamageDealer.dbPlayer.SteamID);

                return;
            }
            if (weapon != null) {
                playerDamageTaken.currentHealth -= weapon.DamagePerBullet;
                playerDamageDealer.damageDone += weapon.DamagePerBullet;

                if (playerDamageTaken.currentHealth <= 0) PlayerDied(playerDamageTaken, playerDamageDealer);

                playerDamageTaken.damageTakenByPlayer.Add(DateTime.Now, playerDamageDealer.dbPlayer.SteamID);

                return;
            }
        }

        public void PlayerDied(GamePlayerData deadPlayer, GamePlayerData playerDamageDealer) {
            deadPlayer.deaths++;
            NetworkSend.SendPlayerDied(owningLobby, deadPlayer, playerDamageDealer, CalculateAssistsOnPlayerDeath(deadPlayer, playerDamageDealer));
        }

        private List<string> CalculateAssistsOnPlayerDeath(GamePlayerData deadPlayer, GamePlayerData killingPlayer) {
            List<string> assistSteamIds = new();
            DateTime dateTime = DateTime.Now;

            for (int x = 1; x <= 5; x++) {
                //assists for damage done
                for (int i = 0; i < deadPlayer.damageTakenByPlayer.Count; i++) {
                    if (deadPlayer.damageTakenByPlayer.ContainsKey(dateTime) && !assistSteamIds.Contains(killingPlayer.dbPlayer.SteamID)) {
                        assistSteamIds.Add(deadPlayer.damageTakenByPlayer.ElementAt(i).Value);
                    }
                }

                //Assists for healing done
                for (int i = 0; i < killingPlayer.healingTakenByPlayer.Count; i++) {
                    if (killingPlayer.healingTakenByPlayer.ContainsKey(dateTime) && !assistSteamIds.Contains(killingPlayer.healingTakenByPlayer.ElementAt(i).Value)) {
                        assistSteamIds.Add(killingPlayer.healingTakenByPlayer.ElementAt(i).Value);
                    }
                }

                dateTime = dateTime.AddMinutes(-1);
            }

            return assistSteamIds;
        }
    }
}