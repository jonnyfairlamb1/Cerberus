namespace Packets {
    enum LoginServerPackets {
        LS_WelcomeMsg = 1,
        LS_LoginConfirmed,
        LS_ErrorMessage,
        LS_LoadoutData,
        LS_OwnedCharacterSkins,
        LS_BaseWeaponData,
        LS_CharacterData,
        LS_CharacterSkinData,
        LS_EndOfPlayerData,
        LS_JoinedGame,
        LS_RejectedConnection_ServerNotReady,
        LS_EquippedSkins,
    }
    enum ClientPackets {
        C_Login = 100,
        C_GameSceneLoaded,
        C_PlayerTransformUpdate
    }

    enum GameServerPackets {
        GS_ErrorMessage = 1000,
        GS_ClientId,
        GS_PlayerGameData,
        GS_PlayerJoinedLobby,
        GS_PlayerLeftLobby,
        GS_ForeignPlayerJoinedGame,
        GS_LocalPlayerJoinedGame,
        GS_PlayerLeftGame,
        GS_PlayerTransformUpdate,
        GS_PlayerChoseCharacter,
        GS_GameStarted,
        GS_GameEnded,
        GS_PlayerTookHealing,
        GS_PlayerTookDamage,
        GS_PlayerDied
    }
}