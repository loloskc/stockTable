using System.ComponentModel.DataAnnotations;

namespace stockTable.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}
