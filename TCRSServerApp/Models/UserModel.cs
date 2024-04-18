using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Models
{
    public class UserModel
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email {  get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
