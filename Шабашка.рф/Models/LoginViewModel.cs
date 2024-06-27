using System.ComponentModel.DataAnnotations;

namespace Шабашка.рф.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(20,ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3,ErrorMessage ="Имя должно иметь длину больше 3 символов")]

        public string Name { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "пароль")]

        public string Password { get; set; }


    }
}
