using CerberusLoginServer.General;
using CommonData.Entities;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using LoginServer.Proxy;
using NovaCore;
using Packets;

namespace CerberusLoginServer.Networking;

public static class NetworkSend {

    public static void Send_ErrorMessage(ushort clientId, ErrorMessage msg) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_ErrorMessage);
        message.Add(msg.errorId);
        message.Add(msg.playerErrorMessage);
        NetworkConfig.server.Send(message, clientId);
    }

    public static void Send_LoginConfirmed(ushort clientId, string playerObject) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_LoginConfirmed);
        message.AddString(playerObject);
        NetworkConfig.server.Send(message, clientId);
    }

    public static void Send_WeaponLoadouts(ushort clientId, List<WeaponLoadouts> weaponLoadouts) {
        for (int i = 0; i < weaponLoadouts.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_LoadoutData);
            message.AddString(Helpers.SerializeObject(weaponLoadouts[i]));
            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_PlayerOwnedSkins(ushort clientId, List<OwnedSkinsTuple> ownedSkins) {
        for (int i = 0; i < ownedSkins.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_OwnedCharacterSkins);
            message.Add(ownedSkins[i].CharacterId);
            message.Add(ownedSkins[i].SkinId);
            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_PlayerEquippedSkins(ushort clientId, List<EquippedSkinsTuple> equippedSkins) {
        for (int i = 0; i < equippedSkins.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_EquippedSkins);
            message.Add(equippedSkins[i].CharacterId);
            message.Add(equippedSkins[i].SkinId);
            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_BaseWeaponData(ushort clientId) {
        for (int i = 0; i < DataProxy._baseWeaponsData.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_BaseWeaponData);
            message.AddString(Helpers.SerializeObject(DataProxy._baseWeaponsData[i]));
            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_AllCharacterData(ushort clientId) {
        for (int i = 0; i < DataProxy._characterData.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_CharacterData);

            message.Add(DataProxy._characterData.ElementAt(i).Value.CharacterId);
            message.Add(DataProxy._characterData.ElementAt(i).Value.CharacterName);
            message.Add(DataProxy._characterData.ElementAt(i).Value.Health);
            message.Add(DataProxy._characterData.ElementAt(i).Value.RunSpeed);
            message.Add(DataProxy._characterData.ElementAt(i).Value.FootstepVolume);

            NetworkConfig.server.Send(message, clientId);
            Send_CharacterAbilities(clientId, DataProxy._characterData.ElementAt(i).Value);
            Send_CharacterSkins(clientId, DataProxy._characterData.ElementAt(i).Value);
        }
    }

    public static void Send_CharacterAbilities(ushort clientId, BaseCharacter baseCharacter) {
        for (int i = 0; i < baseCharacter.characterAbilities.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_CharacterAbilityData);

            message.Add(baseCharacter.characterAbilities[i].AbilityId);
            message.Add(baseCharacter.characterAbilities[i].CharacterId);
            message.Add(baseCharacter.characterAbilities[i].AbilityName);
            message.Add(baseCharacter.characterAbilities[i].Healing);
            message.Add(baseCharacter.characterAbilities[i].Damage);
            message.Add(baseCharacter.characterAbilities[i].Cooldown);
            message.Add(baseCharacter.characterAbilities[i].AbiltyBind);
            message.Add(baseCharacter.characterAbilities[i].AbiltyType);
            message.Add(baseCharacter.characterAbilities[i].UltimateChargeModifier);

            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_CharacterSkins(ushort clientId, BaseCharacter baseCharacter) {
        for (int i = 0; i < baseCharacter.characterSkins.Count; i++) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_CharacterSkinData);

            message.Add(baseCharacter.characterSkins[i].SkinId);
            message.Add(baseCharacter.characterSkins[i].CharacterId);
            message.Add(baseCharacter.characterSkins[i].CharacterName);
            message.Add(baseCharacter.characterSkins[i].SkinName);
            message.Add(baseCharacter.characterSkins[i].Cost);

            NetworkConfig.server.Send(message, clientId);
        }
    }

    public static void Send_EndOfPlayerData(ushort clientId) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_EndOfPlayerData);
        NetworkConfig.server.Send(message, clientId);
    }

    public static void Send_JoinedGame(ushort clientId, string gameServerObject) {
        if (string.IsNullOrEmpty(gameServerObject)) {
            Send_ErrorMessage(clientId, DataProxy._errorMessages[10003]);
            return;
        }

        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_JoinedGame);
        message.AddString(gameServerObject);
        NetworkConfig.server.Send(message, clientId);
    }
}