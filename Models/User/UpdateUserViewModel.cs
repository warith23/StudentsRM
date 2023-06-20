using System.ComponentModel.DataAnnotations;

namespace StudentsRM.Models.User
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
 