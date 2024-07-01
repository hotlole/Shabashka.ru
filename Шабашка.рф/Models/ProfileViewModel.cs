using System.ComponentModel.DataAnnotations; 
    
namespace Шабашка.рф.Models
{
    public class ProfileViewModel
    {
        public long id { get; set; }

        [Required (ErrorMessage = "Укажите возраст")]
        [Range (0, 150,ErrorMessage = "Диапазон возраста должен быть от 0 до 150")]

        public short Age { get; set; }
        [Required(ErrorMessage = "Укажите электронный адрес")]
        [MinLength (5,ErrorMessage = "Минимальная длина должна быть больше 5 символов")]
        [MaxLength(100,ErrorMessage ="Максимальная длина должна быть меньше 100 символов")]

        public string Email {  get; set; }
    }
}
