CREATE OR REPLACE PACKAGE BODY PG_WEAPONS AS

PROCEDURE Get_Player_Loadouts(SteamID in varchar2, results out sys_refcursor) IS
BEGIN
    Open results for Select * from player_loadouts where OWNING_PLAYER_STEAM_ID = SteamID;
END Get_Player_Loadouts;
 
PROCEDURE Get_Custom_Weapons(SteamID in varchar2, results out sys_refcursor) IS
BEGIN
    Open results for Select * from custom_weapons where OWNING_PLAYER_STEAM_ID = SteamID;
END Get_Custom_Weapons;

END PG_WEAPONS;