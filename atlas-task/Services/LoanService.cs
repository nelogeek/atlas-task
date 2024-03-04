using atlas_task.Interfaces;
using atlas_task.Models;

namespace atlas_task.Services
{
    public class LoanService : ILoanService
    {
        public IEnumerable<Payment> CalculateLoanSchedule(Loan loan)
        {
            List<Payment> schedule = new List<Payment>();

            decimal remainingPrincipal = loan.LoanAmount;
            decimal monthlyInterestRate = (decimal)loan.InterestRate / 12m / 100m;
            decimal monthlyPayment = loan.LoanAmount * monthlyInterestRate /
                                     (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), -loan.LoanTerm));

            for (int i = 1; i <= loan.LoanTerm; i++)
            {
                decimal interestPayment = remainingPrincipal * monthlyInterestRate;
                decimal principalPayment = monthlyPayment - interestPayment;

                           schedule.Add(new Payment
                {
                    PaymentNumber = i,
                    PaymentDate = DateTime.Now.AddMonths(i),
                    PaymentAmount = Math.Round(principalPayment + interestPayment, 2),
                    PrincipalPayment = Math.Round(principalPayment, 2),
                    InterestPayment = Math.Round(interestPayment, 2),
                    RemainingBalance = Math.Round(remainingPrincipal - principalPayment , 2)
                });

                remainingPrincipal -= principalPayment;
            }

            return schedule;
        }
    }
}
