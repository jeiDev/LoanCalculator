using System.Data;
using Microsoft.Data.SqlClient;
using LoanCalculator.Domain.Entities;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Infrastructure.Data;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public LoanRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public LoanResult CalculateLoan(int age, decimal amount, int months)
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
                return new LoanResult
                {
                    Age = reader.GetInt32(0),
                    Rate = reader.GetDecimal(1),
                    MonthlyPayment = reader.GetDecimal(2)
                };
            }

            throw new Exception("Loan calculation failed");
        }

        public LoanAgeRange GetAgeRange()
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SELECT MIN(Age), MAX(Age) FROM AgeRates", connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new LoanAgeRange
                {
                    MinAge = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    MaxAge = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                };
            }

            throw new Exception("Age range query failed");
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

        public PagedResult<LoanLog> GetLoanLogs(int pageNumber, int pageSize)
        {
            var result = new PagedResult<LoanLog>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_GetLoanLogs", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Data.Add(new LoanLog
                {
                    QueryDate = reader.GetDateTime(0),
                    Age = reader.GetInt32(1),
                    Amount = reader.GetDecimal(2),
                    Months = reader.GetInt32(3),
                    MonthlyPayment = reader.GetDecimal(4),
                    IpAddress = reader.GetString(5)
                });
            }

            if (reader.NextResult())
            {
                if (reader.Read())
                {
                    result.TotalRecords = reader.GetInt32(0);
                }
            }

            result.PageNumber = pageNumber;
            result.PageSize = pageSize;

            return result;
        }
    }
}