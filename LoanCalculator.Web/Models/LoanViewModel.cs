namespace LoanCalculator.Web.Models
{
    public class LoanViewModel
    {
        public DateTime BirthDate { get; set; } = new DateTime(1990, 1, 1);
        public decimal Amount { get; set; }
        public int Months { get; set; }

        public int Age { get; set; }
        public decimal Rate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int MinAllowedAge { get; set; }
        public int MaxAllowedAge { get; set; }
        public string? AgeStatusMessage { get; set; }

        public List<int> Terms { get; set; } = new List<int> { 3, 6, 9, 12 };
    }
}