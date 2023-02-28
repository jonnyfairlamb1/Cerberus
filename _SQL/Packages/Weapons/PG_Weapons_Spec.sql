CREATE OR REPLACE PACKAGE PG_WEAPONS AS 

PROCEDURE Get_Player_Loadouts(SteamID in varchar2, results out sys_refcursor);
PROCEDURE Get_Custom_Weapons(SteamID in varchar2, results out sys_refcursor);

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 

END PG_WEAPONS;