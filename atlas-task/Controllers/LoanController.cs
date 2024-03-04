using atlas_task.Interfaces;
using atlas_task.Models;
using atlas_task.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace atlas_task.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(Loan loan)
        {
            if (ModelState.IsValid)
            {
                var schedule = _loanService.CalculateLoanSchedule(loan);
                var sumInterestPayments = schedule.Sum(payment => payment.InterestPayment);
                var sumPrincipalPayments = schedule.Sum(payment => payment.PrincipalPayment);
                var totalOverpayment = sumInterestPayments + sumPrincipalPayments;


                var viewModel = new PaymentResultsViewModel { Payments = schedule, TotalOverpayment = totalOverpayment };
                // Вернуть представление с расписанием платежей и моделью представления
                return View("Schedule", viewModel);

            }

            return View("Index", loan);
        }



    }
}
