using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Data.Entities
{
    public class FileMetaData
    {
        [Key]
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int PostId { get; set; }

        public virtual ContentPost ContentPost { get; set; }
    }
}
