using CommonData.PlayerSendData;
using CommonData.ServerData;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CommonData.GameServer {

    public class GamePlayerData {
        public ushort clientId;

        public DBPlayer dbPlayer;

        public int teamId;

        public int maxHealth;
        public float currentHealth;
        public int maxAmmo;
        public int currentAmmo;
        public int reserveAmmo;

        public Dictionary<DateTime, string> damageTakenByPlayer = new();
        public Dictionary<DateTime, string> healingTakenByPlayer = new();

        public float damageDone;
        public float healingDone;
        public int assists;
        public int deaths;
        public float healingTaken;

        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public PlayerRole role;

        public GamePlayerData(ushort clientId, DBPlayer dbPlayer, int teamId, Vector3 currentPosition, Quaternion currentRotation,
            PlayerRole playerRole) {
            this.clientId = clientId;
            this.dbPlayer = dbPlayer;
            this.teamId = teamId;
            this.currentPosition = currentPosition;
            this.currentRotation = currentRotation;
            this.role = playerRole;
            this.damageDone = 0;
            this.healingDone = 0;
            this.assists = 0;
            this.deaths = 0;
            this.healingTaken = 0;
        }
    }
}