CREATE TABLE CERBERUS.PLAYER_CUSTOM_WEAPONS(
CUSTOM_WEAPON_ID NUMBER NOT NULL,
LOADOUT_SLOT VARCHAR2(100) NOT NULL, 
WEAPON_ID NUMBER NOT NULL, 
OWNING_PLAYER_STEAM_ID VARCHAR2(100) NOT NULL,
BARREL_ATTACHMENT_ID NUMBER,
GRIP_ATTACHMENT_ID NUMBER,
SCOPE_ATTACHMENT_ID NUMBER,
RAIL_ATTACHMENT_ID NUMBER,
MID_BARREL_ATTACHMENT_ID NUMBER
);