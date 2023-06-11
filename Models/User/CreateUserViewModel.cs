using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.User
{
    public class CreateUserViewModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
