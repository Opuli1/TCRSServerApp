using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCRSServerApp.Data.Entities
{
    public class ContentPost
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Slug { get; set; }

        public int CategoryId { get; set; } = 0;

        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedOn { get; set; }

        public DateTime? EditedOn { get; set;}

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual User User { get; set; }

        [NotMapped]
        public string CategoryName => Category.Name;

        public List<FileMetaData> Files { get; set; } = new List<FileMetaData>();
    }
}
