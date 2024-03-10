using System.ComponentModel.DataAnnotations;

namespace Food.ViewModels
{
    public class SignInVM
    {
       
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        public string Username { get; set; }

        

        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
       
        public bool IsRemember { get; set; }
    }
}
