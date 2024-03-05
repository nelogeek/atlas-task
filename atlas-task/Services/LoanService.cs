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

        public IEnumerable<Payment> CalculateAdditionalLoanSchedule(LoanDop loanDop)
        {
            List<Payment> schedule = new List<Payment>();

            decimal remainingPrincipal = loanDop.LoanAmount; // Сумма займа
            int loanTermInDays = loanDop.LoanTermInDays; // Срок займа (в днях)
            decimal dailyInterestRate = loanDop.InterestRatePerDay / 100m; // Ставка
            int paymentStepInDays = loanDop.PaymentStepInDays; // Шаг платежа


            decimal annuityFactor = (dailyInterestRate * (decimal)Math.Pow(1 + (double)dailyInterestRate, loanTermInDays)) / ((decimal)Math.Pow(1 + (double)dailyInterestRate, loanTermInDays) - 1);


            DateTime paymentDate = DateTime.Today;

            for (int currentDay = 1, paymentNumber = 1; currentDay <= loanTermInDays; currentDay++)
            {
                if (currentDay % paymentStepInDays == 0 || currentDay == loanTermInDays)
                {
                    decimal interestPayment = remainingPrincipal * dailyInterestRate;
                    decimal principalPayment = annuityFactor * remainingPrincipal - interestPayment;
                    decimal paymentAmount = principalPayment + interestPayment;

                    remainingPrincipal -= principalPayment;

                    schedule.Add(new Payment
                    {
                        PaymentNumber = paymentNumber++,
                        PaymentDate = paymentDate,
                        PaymentAmount = Math.Round(paymentAmount, 2),
                        PrincipalPayment = Math.Round(principalPayment, 2),
                        InterestPayment = Math.Round(interestPayment, 2),
                        RemainingBalance = Math.Round(remainingPrincipal, 2)
                    });

                    paymentDate = paymentDate.AddDays(paymentStepInDays);
                }
            }

            return schedule;
        }


    }
}
