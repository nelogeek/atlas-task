using System.ComponentModel.DataAnnotations;

namespace atlas_task.Models
{
    public class Loan
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Сумма займа должен быть больше нуля")]
        [Display(Name = "Сумма займа")]
        public decimal LoanAmount { get; set; }

        
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Срок займа должен быть больше нуля")]
        [Display(Name = "Срок займа (в месяцах)")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Ставка должна быть неотрицательной")]
        [Display(Name = "Ставка (в год)")]
        public double InterestRate { get; set; }
    }
}
