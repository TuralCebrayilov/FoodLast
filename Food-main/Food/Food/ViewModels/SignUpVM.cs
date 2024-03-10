using System.ComponentModel.DataAnnotations;

namespace Food.ViewModels
{
    public class SignUpVM
    {
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Buxana boş ola bilməz!!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string CheckPassword { get; set; }
        public bool IsRemember { get; set; }
    }
}
