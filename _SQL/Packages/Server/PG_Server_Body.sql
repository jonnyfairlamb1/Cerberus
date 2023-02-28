create or replace PACKAGE BODY PG_SERVER AS

PROCEDURE GetErrorMessages(results out sys_refcursor) is
BEGIN
Open results for Select * from Error_Messages;
END GetErrorMessages;

PROCEDURE GetMapDetails(results out sys_refcursor) is
BEGIN
Open results for Select * from gamemaps;
END GetMapDetails;


PROCEDURE GetGameModes(results out sys_refcursor) is
BEGIN
Open results for Select * from gamemodes;
END GetGameModes;

END PG_SERVER;