using LoanCalculator.Domain.Entities;
using LoanCalculator.Infrastructure.Interfaces;

namespace LoanCalculator.Application.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public LoanResult CalculateLoan(LoanRequest request)
        {
            int age = CalculateAge(request.BirthDate);
            var ageRange = _loanRepository.GetAgeRange();

            if (request.Amount <= 0)
                throw new Exception("El monto debe de ser mayor a 0");

            if (age < ageRange.MinAge)
                throw new Exception("Lo Sentimos aun no cuenta con la edad para solicitar esta producto.");

            if (age > ageRange.MaxAge)
                throw new Exception("Favor pasar por una de nuestras sucursales para evaluar su caso.");

            var result = _loanRepository.CalculateLoan(age, request.Amount, request.Months);

            _loanRepository.SaveLoanLog(age, request.Amount, request.Months, result.MonthlyPayment, request.IpAddress);

            return result;
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        public PagedResult<LoanLog> GetLoanLogs(int pageNumber, int pageSize)
        {
            return _loanRepository.GetLoanLogs(pageNumber, pageSize);
        }
    }
}