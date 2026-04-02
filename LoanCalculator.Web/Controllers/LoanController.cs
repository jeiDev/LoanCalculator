using Microsoft.AspNetCore.Mvc;
using LoanCalculator.Web.Models;
using LoanCalculator.Application.Services;
using LoanCalculator.Domain.Entities;

namespace LoanCalculator.Web.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _loanService;

        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult Index()
        {
            return View(new LoanViewModel());
        }

        [HttpPost]
        public IActionResult Calculate(LoanViewModel model)
        {
            try
            {
                var request = new LoanRequest
                {
                    BirthDate = model.BirthDate,
                    Amount = model.Amount,
                    Months = model.Months,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty
                };

                var result = _loanService.CalculateLoan(request);

                model.Age = result.Age;
                model.Rate = result.Rate;
                model.MonthlyPayment = result.MonthlyPayment;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.AgeStatusMessage = ex.Message;
            }

            return View("Index", model);
        }
    }
}