using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LoanCalculator.Infrastructure.Data
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString = string.Empty;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}