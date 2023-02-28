create or replace PACKAGE PG_SERVER AS 

PROCEDURE GetErrorMessages(results out sys_refcursor);
PROCEDURE GetMapDetails(results out sys_refcursor);
PROCEDURE GetGameModes(results out sys_refcursor);

END PG_SERVER;