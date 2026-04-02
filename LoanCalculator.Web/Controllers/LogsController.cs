using Microsoft.AspNetCore.Mvc;
using LoanCalculator.Application.Services;
using LoanCalculator.Domain.Entities;

namespace LoanCalculator.Web.Controllers
{
    public class LogsController : Controller
    {
        private readonly LoanService _loanService;
        private const int PageSize = 10;

        public LogsController(LoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var result = _loanService.GetLoanLogs(page, pageSize);


            return View(result);
        }
    }
}