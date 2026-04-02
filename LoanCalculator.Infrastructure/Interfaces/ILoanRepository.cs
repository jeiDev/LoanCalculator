using LoanCalculator.Domain.Entities;

namespace LoanCalculator.Infrastructure.Interfaces
{
    public interface ILoanRepository
    {
        LoanResult CalculateLoan(int age, decimal amount, int months);
        LoanAgeRange GetAgeRange();
        void SaveLoanLog(int age, decimal amount, int months, decimal monthlyPayment, string ipAddress);

        PagedResult<LoanLog> GetLoanLogs(int pageNumber, int pageSize);
    }
}