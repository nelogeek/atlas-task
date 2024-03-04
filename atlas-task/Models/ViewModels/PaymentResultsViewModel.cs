namespace atlas_task.Models.ViewModels
{
    public class PaymentResultsViewModel
    {
        public IEnumerable<Payment> Payments { get; set; }
        public decimal TotalOverpayment { get; set; }
    }
}
