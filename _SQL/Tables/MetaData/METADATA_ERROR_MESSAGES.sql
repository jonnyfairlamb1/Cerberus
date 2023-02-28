CREATE TABLE CERBERUS.METADATA_ERROR_MESSAGES(
"ERROR_ID" NUMBER NOT NULL ENABLE, 
"PLAYER_ERROR_MESSAGE" VARCHAR2(4000 BYTE) NOT NULL ENABLE, 
INTERNAL_ERROR_MESSAGE VARCHAR2(4000 BYTE) NOT NULL ENABLE,
ERROR_LEVEL NUMBER NOT NULL ENABLE,
CONSTRAINT "ERROR_MESSAGES_PK" PRIMARY KEY ("ERROR_ID")
);