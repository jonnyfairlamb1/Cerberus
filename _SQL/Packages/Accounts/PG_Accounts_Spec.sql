create or replace PACKAGE PG_ACCOUNTS AS 
PROCEDURE Player_Login(SteamID in varchar2,SteamName in varchar2, IPAddress in varchar2, results out sys_refcursor);
PROCEDURE CREATE_NEW_PLAYER(SteamID in varchar2, SteamName in varchar2, IPAddress in varchar2);
PROCEDURE Player_Logout(SteamID in varchar2, results out sys_refcursor);

PROCEDURE ResetDB;

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 

END PG_ACCOUNTS;