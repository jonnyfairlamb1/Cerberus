create or replace PACKAGE BODY PG_ACCOUNTS AS

PROCEDURE Player_Login(SteamID in varchar2,SteamName in varchar2, IPAddress in varchar2, results out sys_refcursor) is
accountStanding varchar2(1000);
BEGIN
    --Check if that account already exists
    BEGIN
        Select accountStanding into accountStanding from player_accounts where Steam_ID = SteamID;
    EXCEPTION
    WHEN no_data_found THEN
        CREATE_NEW_PLAYER(SteamID,SteamName,IPAddress);
    END;
        --Return the account
    Update player_accounts set is_logged_in = 1 where steam_id = SteamID;
    Open results for Select PLAYER_LEVEL, BATTLEPASS_LEVEL, CURRENCY, ACCOUNT_STANDING from player_accounts where steam_id = SteamID;
END Player_Login;

PROCEDURE CREATE_NEW_PLAYER(SteamID in varchar2, SteamName in varchar2, IPAddress in varchar2) is
LoadoutCounter number;
LoadoutNumber number;

BEGIN
LoadoutCounter := 0;
LoadoutNumber := 0;

    --Create account
    Insert into player_accounts VALUES(SteamName, SteamID,1,1,0,1,IPAddress,'Good',null,null);
    
    --Create base weapons for player
    FOR record IN (Select * from metadata_start_weapons)
    LOOP
        LoadoutCounter := LoadoutCounter +1;
        Insert into player_custom_weapons VALUES(CUSTOM_WEP_ID.nextval,record.Loadout_Slot, record.Weapon_ID, SteamID, null,null,null,null,null);
        
        IF LoadoutCounter = 2 THEN
            loadoutnumber := loadoutnumber +1;
            Insert into player_loadouts 
                VALUES(LOADOUT_ID.nextval, SteamID, 'Custom Loadout '||LoadoutNumber, CUSTOM_WEP_ID.currval -1,CUSTOM_WEP_ID.currval);
            LoadoutCounter := 0;
        END IF;
        
    END LOOP;
    SetupNewAccountSkins(SteamID);
END CREATE_NEW_PLAYER;

PROCEDURE SetupNewAccountSkins(SteamId IN VARCHAR2) is
CURSOR CharacterSkinValuesCur IS SELECT skin_id, character_id from metadata_character_skins where baseline = 'Y';
SkinId number;
CharacterId number;
TargetSkinTable varchar2(100);
SQLStatement varchar2(1000);
BEGIN
OPEN CharacterSkinValuesCur;
LOOP
    FETCH CharacterSkinValuesCur INTO skinid, CharacterId;
    EXIT WHEN CharacterSkinValuesCur%NOTFOUND;
    INSERT INTO player_skins VALUES(PLAYER_SKIN_ID.nextval, SteamId, SkinId);
    INSERT INTO player_equipped_skins values(SteamId, SkinId, CharacterId);
END LOOP;
END SetupNewAccountSkins;

PROCEDURE Player_Logout(SteamID in varchar2, results out sys_refcursor) is
BEGIN
    Update player_accounts set IS_LOGGED_IN = 0 where STEAM_ID = SteamID;
    Open results for select is_logged_in from player_accounts where steam_id = SteamID;
END Player_Logout;

PROCEDURE GetPlayerCharacterSkins(SteamId IN VARCHAR2,results out sys_refcursor) IS
BEGIN
OPEN results FOR
SELECT skin_id 
FROM player_skins
WHERE owning_steam_id = SteamId;
END GetPlayerCharacterSkins;

PROCEDURE Clear_Database IS
BEGIN
DELETE FROM player_accounts;
DELETE FROM player_custom_weapons;
DELETE FROM player_loadouts;
DELETE FROM player_skins;
DELETE FROM player_equipped_skins;
DBMS_OUTPUT.PUT_LINE('Completed');
END Clear_Database;


END PG_ACCOUNTS;