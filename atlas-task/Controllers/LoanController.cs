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

                // Создание экземпляра модели LoanInputViewModel
                var loanInputViewModel = new LoanInputViewModel
                {
                    loan = loan, // Здесь передаем объект loan
                    loanDop = new LoanDop() // Здесь можно передать объект LoanDop, если необходимо
                };

                var viewModel = new PaymentResultsViewModel { Payments = schedule, TotalOverpayment = totalOverpayment };

                return View("Schedule", viewModel);
            }

            // Возвращаем представление "Index" с моделью типа LoanInputViewModel
            return View("Index", new LoanInputViewModel());
        }



        [HttpPost]
        public IActionResult CalculateAdditionalTask(LoanDop loanDop)
        {
            if (ModelState.IsValid)
            {
                var schedule = _loanService.CalculateAdditionalLoanSchedule(loanDop);
                var viewModel = new PaymentResultsViewModel
                {
                    Payments = schedule,
                    TotalOverpayment = schedule.Sum(payment => payment.InterestPayment)
                };
                return View("Schedule", viewModel);
            }
            return View("Index", loanDop);
        }



    }
}
