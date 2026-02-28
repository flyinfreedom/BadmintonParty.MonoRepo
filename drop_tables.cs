using Npgsql;

var connectionString = "Host=db.fryovhbnlbgvsnlzuary.supabase.co;Database=postgres;Username=postgres;Password=bH6LnPtpAehSuvRK;SSL Mode=Require;Trust Server Certificate=true";

using var conn = new NpgsqlConnection(connectionString);
conn.Open();

var sql = @"
DROP TABLE IF EXISTS ""AuditLogs"" CASCADE;
DROP TABLE IF EXISTS ""TenateAccounts"" CASCADE;
DROP TABLE IF EXISTS ""TenateHalls"" CASCADE;
DROP TABLE IF EXISTS ""Tenates"" CASCADE;
DROP TABLE IF EXISTS ""Members"" CASCADE;
DROP TABLE IF EXISTS ""MemberRecentOpenings"" CASCADE;
DROP TABLE IF EXISTS ""Courts"" CASCADE;
DROP TABLE IF EXISTS ""CourtHalls"" CASCADE;
DROP TABLE IF EXISTS ""__EFMigrationsHistory"" CASCADE;
";

using var cmd = new NpgsqlCommand(sql, conn);
cmd.ExecuteNonQuery();

Console.WriteLine("Tables dropped successfully.");
