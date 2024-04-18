using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Data
{
    public class BannerSettings
    {
        [Key]
        public int BannerId { get; set; }

        public bool Visible { get; set; }

        public string Message { get; set; }
    }
}
