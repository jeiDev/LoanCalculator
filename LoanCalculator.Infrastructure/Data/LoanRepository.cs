using Microsoft.Data.SqlClient;
using System.Data;

namespace LoanCalculator.Infrastructure.Data
{
    public class LoanRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public LoanRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public (int Age, decimal Rate, decimal MonthlyPayment) CalculateLoan(int age, decimal amount, int months)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_CalculateLoan", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Age", age);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@Months", months);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return (
                    reader.GetInt32(0),
                    reader.GetDecimal(1),
                    reader.GetDecimal(2)
                );
            }

            throw new Exception("Loan calculation failed");
        }

        public void SaveLoanLog(int age, decimal amount, int months, decimal monthlyPayment, string ipAddress)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_SaveLoanLog", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Age", age);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@Months", months);
            command.Parameters.AddWithValue("@MonthlyPayment", monthlyPayment);
            command.Parameters.AddWithValue("@IpAddress", ipAddress);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}