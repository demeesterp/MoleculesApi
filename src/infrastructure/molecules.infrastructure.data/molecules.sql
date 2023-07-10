﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'moleculesapp') THEN
        CREATE SCHEMA moleculesapp;
    END IF;
END $EF$;

CREATE TABLE moleculesapp."CalcOrder" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" character varying(250) NOT NULL,
    "Description" text NOT NULL,
    "CustomerName" text NOT NULL,
    CONSTRAINT "PK_CalcOrder" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230710164845_InitialCreate', '7.0.8');

COMMIT;

