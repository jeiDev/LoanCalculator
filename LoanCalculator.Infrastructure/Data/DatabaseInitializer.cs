using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LoanCalculator.Infrastructure.Data
{
    public class DatabaseInitializer
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            var dir = new DirectoryInfo(currentDir);

            while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "database")))
            {
                dir = dir.Parent;
            }

            if (dir == null)
                throw new Exception("Database folder not found");

            var scriptsFolder = Path.Combine(dir.FullName, "database");

            ExecuteScript(connectionString, Path.Combine(scriptsFolder, "01_create_database.sql"));
            ExecuteScript(connectionString, Path.Combine(scriptsFolder, "02_tables.sql"));
            ExecuteScript(connectionString, Path.Combine(scriptsFolder, "03_seed_data.sql"));
            ExecuteScript(connectionString, Path.Combine(scriptsFolder, "04_stored_procedures.sql"));
        }

        private void ExecuteScript(string connectionString, string scriptPath)
        {
            if (!File.Exists(scriptPath))
                return;

            var script = File.ReadAllText(scriptPath);

            var commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            foreach (var commandText in commands)
            {
                using var command = new SqlCommand(commandText, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}