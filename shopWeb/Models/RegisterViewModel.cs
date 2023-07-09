using System.ComponentModel.DataAnnotations;

namespace shopWeb.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string fullName { get; set; }


        [Required]
        public string NatinalCode { get; set; }


        [Required]
        public string MyAddresss { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password and confirmation dont match")]
        public string ConfirmPassword { get; set; }
    }
}
