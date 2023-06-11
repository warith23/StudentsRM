using System.ComponentModel.DataAnnotations;

namespace StudentsRM.Models.User
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
 