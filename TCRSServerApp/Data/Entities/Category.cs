using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), Unicode(false)]
        public string Name { get; set; }

        [Required, MaxLength(100), Unicode(false)]
        public string Slug { get; set; }

        public virtual ICollection<ContentPost> ContentPosts { get; set; } = new List<ContentPost>();

        public Category Clone() => 
            (Category)this.MemberwiseClone();
    }
}
