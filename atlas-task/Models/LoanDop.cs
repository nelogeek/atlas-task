using System.ComponentModel.DataAnnotations;

namespace atlas_task.Models
{
    public class LoanDop
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное число")]
        [Display(Name = "Сумма займа")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Введите положительное число")]
        [Display(Name = "Срок займа (в днях)")]
        public int LoanTermInDays { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Введите положительное число")]
        [Display(Name = "Срок займа (в днях)")]
        public decimal InterestRatePerDay { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Введите положительное число")]
        [Display(Name = "Шаг платежа (в днях)")]
        public int PaymentStepInDays { get; set; }
    }
}
