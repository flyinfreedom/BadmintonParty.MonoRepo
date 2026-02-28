-- Seed data for Tenates and TenateAccounts
-- TenateStatus: Active = '1'
-- AccountStatus: Active = '1'

-- 1. Insert a default Tenate
INSERT INTO "Tenates" ("Id", "Name", "Description", "Status", "PublicId")
VALUES (1, 'Eden Badminton Club', 'The default badminton club for Eden.', 'Active', '550e8400-e29b-41d4-a716-446655440000')
ON CONFLICT ("Id") DO NOTHING;

-- 2. Insert the Eden TenateAccount
-- Note: TenateId must match the Tenate Id above. 
-- Password is stored as plain text here for demonstration, you might need to hash it later.
INSERT INTO "TenateAccounts" ("Id", "TenateId", "Account", "Password", "Name", "Status")
VALUES (1, 1, 'Eden', 'Aa123456', 'Eden Manager', 'Active')
ON CONFLICT ("Account") DO NOTHING;

-- Resetting sequence for Postgres to avoid auto-increment collisions after manual IDs
SELECT setval(pg_get_serial_sequence('"Tenates"', 'Id'), coalesce(max("Id"), 1)) FROM "Tenates";
SELECT setval(pg_get_serial_sequence('"TenateAccounts"', 'Id'), coalesce(max("Id"), 1)) FROM "TenateAccounts";
