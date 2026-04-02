namespace LoanCalculator.Domain.Entities
{
    public class LoanLog
    {
        public DateTime QueryDate { get; set; }
        public int Age { get; set; }
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public decimal MonthlyPayment { get; set; }
        public string IpAddress { get; set; }
    }
}