using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TCRSServerApp.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(25), Unicode(false)]
        public string FirstName { get; set; }

        [MaxLength(25), Unicode(false)]
        public string LastName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(30)]
        public string Salt { get; set; }

        [Required, MaxLength(100)]
        public string Hash { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? EditedOn { get; set; }
    }
}
