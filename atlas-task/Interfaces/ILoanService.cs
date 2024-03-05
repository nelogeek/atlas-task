using atlas_task.Models;

namespace atlas_task.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Payment> CalculateLoanSchedule(Loan loan);
        IEnumerable<Payment> CalculateAdditionalLoanSchedule(LoanDop loanDop);
    }
}
