using NovaCore;
using Packets;
using System.Linq;
using UnityEngine;

public static class AccountRecieveMessages {

    [MessageHandler((ushort)LoginServerPackets.LS_LoginConfirmed)]
    private static void Packet_LoginConfirmed(Message message) {
        string playerDataString = message.GetString();
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataString);
        GameManager.instance._localPlayerData = playerData;
        //MenuManager.instance.UpdatePlayerSteamData();
    }

    [MessageHandler((ushort)LoginServerPackets.LS_CharacterData)]
    private static void Packet_BaseCharacterData(Message message) {
        int characterId = message.GetInt();
        string characterName = message.GetString();
        int health = message.GetInt();
        int runSpeed = message.GetInt();
        float footstepVolume = message.GetFloat();

        BaseCharacter character = new(characterId, characterName, health, runSpeed, footstepVolume);
        GameManager.instance._characters.Add(character.CharacterId, character);
    }

    [MessageHandler((ushort)LoginServerPackets.LS_CharacterAbilityData)]
    private static void Packet_CharacterAbilityData(Message message) {
        int abilityId = message.GetInt();
        int characterId = message.GetInt();
        string abilityName = message.GetString();
        int healing = message.GetInt();
        int damage = message.GetInt();
        int cooldown = message.GetInt();
        string abiltyBind = message.GetString();
        string abiltyType = message.GetString();
        int ultimateChargeModifier = message.GetInt();

        BaseCharacterAbility ability = new(abilityId, characterId, abilityName, healing, damage, cooldown, abiltyBind, abiltyType, ultimateChargeModifier);

        GameManager.instance._characters[characterId].characterAbilities.Add(ability);
    }

    [MessageHandler((ushort)LoginServerPackets.LS_CharacterSkinData)]
    private static void Packet_CharacterSkinData(Message message) {
        int skinId = message.GetInt();
        int characterId = message.GetInt();
        string characterName = message.GetString();
        string skinName = message.GetString();
        int cost = message.GetInt();

        CharacterSkin skin = new(skinId, characterId, characterName, skinName, cost);
        GameManager.instance._characters[characterId].characterSkins.Add(skin);
    }

    [MessageHandler((ushort)LoginServerPackets.LS_OwnedCharacterSkins)]
    private static void Packet_OwnedCharacterSkins(Message message) {
        int characterId = message.GetInt();
        int skinId = message.GetInt();

        for (int x = 0; x < GameManager.instance._characters[characterId].characterSkins.Count; x++) {
            if (GameManager.instance._characters[characterId].characterSkins[x].SkinId == skinId) {
                GameManager.instance._characters[characterId].characterSkins[x].IsOwned = true;
                return;
            }
        }
    }

    [MessageHandler((ushort)LoginServerPackets.LS_EquippedSkins)]
    private static void Packet_EquippedCharacterSkins(Message message) {
        int characterId = message.GetInt();
        int skinId = message.GetInt();

        for (int x = 0; x < GameManager.instance._characters[characterId].characterSkins.Count; x++) {
            if (GameManager.instance._characters[characterId].characterSkins[x].SkinId == skinId) {
                GameManager.instance._characters[characterId].characterSkins[x].IsEquipped = true;
                return;
            }
        }
    }

    [MessageHandler((ushort)LoginServerPackets.LS_BaseWeaponData)]
    private static void Packet_BaseWeaponData(Message message) {
        string baseWeaponString = message.GetString();
        BaseWeapon baseWeapon = JsonUtility.FromJson<BaseWeapon>(baseWeaponString);

        GameManager.instance._baseWeaponDict.Add(baseWeapon.WeaponId, baseWeapon);
    }

    [MessageHandler((ushort)LoginServerPackets.LS_LoadoutData)]
    private static void Packet_LoadoutData(Message message) {
        string loadout = message.GetString();
        WeaponLoadouts weaponData = JsonUtility.FromJson<WeaponLoadouts>(loadout);
        GameManager.instance._localPlayerData.weaponLoadouts.Add(weaponData);
    }

    [MessageHandler((ushort)LoginServerPackets.LS_EndOfPlayerData)]
    private static void Packet_EndOfPlayerData(Message message) {
        for (int i = 0; i < GameManager.instance._baseWeaponDict.Count; i++) {
            WeaponEditorManager.instance.GenerateButton(GameManager.instance._baseWeaponDict.ElementAt(i).Value);
        }

        for (int i = 0; i < GameManager.instance._characters.Count; i++) {
            CharacterEditorManager.instance.GenerateButton(GameManager.instance._characters.ElementAt(i).Value);
        }

        MenuManager.instance.GoToMainMenu();
    }
}