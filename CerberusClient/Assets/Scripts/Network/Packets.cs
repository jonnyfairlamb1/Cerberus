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
        C_Weapon_Data,
        C_JoinRandomGame,
        C_LobbyId,
        C_PlayerChoseCharacter,
        C_PlayerChoseLoadout,
    }

    enum GameServerPackets {
        GS_ErrorMessage = 1000,
        GS_ClientId,
        GS_PlayerJoinedLobby,
        GS_PlayerLeftLobby,
        GS_PlayerTransformUpdate,
        GS_PlayerChoseCharacter,
        GS_GameStarted,
        GS_GameEnded,
        GS_PlayerTookHealing,
        GS_PlayerTookDamage,
        GS_PlayerDied
    }
}