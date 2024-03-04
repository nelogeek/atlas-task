using System.ComponentModel.DataAnnotations;

namespace atlas_task.Models
{
    public class Loan
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Сумма займа должен быть больше нуля")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Срок займа должен быть больше нуля")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Ставка должна быть неотрицательной")]
        public double InterestRate { get; set; }
    }
}
