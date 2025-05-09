START TRANSACTION;

CREATE TABLE "DomainEventOutboxMessages" (
    "Id" uuid NOT NULL,
    "EventType" text COLLATE case_insensitive NOT NULL,
    "Payload" text COLLATE case_insensitive NOT NULL,
    "OccurredOnUtc" timestamp with time zone NOT NULL,
    "ProcessedOnUtc" timestamp with time zone,
    "RetryCount" integer NOT NULL,
    "Error" text COLLATE case_insensitive,
    CONSTRAINT "PK_DomainEventOutboxMessages" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250509105823_AddDomainEventOutboxMessages', '8.0.6');

COMMIT;

