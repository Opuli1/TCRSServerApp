using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Models
{
    public class SignInModel
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required, MinLength(5)]
        public string Password { get; set; }
    }
}
