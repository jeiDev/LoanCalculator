namespace LoanCalculator.Domain.Entities
{
    public class LoanRequest
    {
        public DateTime BirthDate { get; set; }
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public string IpAddress { get; set; }
    }
}